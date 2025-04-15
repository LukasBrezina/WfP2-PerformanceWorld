using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class WaterFallObject : IPoolableObject
{
    private GameObject instance;
    private GameObject prefab;
    private Transform parent;
    private float destroyTime;
    private MonoBehaviour runner;
    private Vector3 customGravity;
    private float x_Anpassung;
    private float y_Anpassung;
    private float z_Anpassung;
    private GenericObjectPool<WaterFallObject> pool;

    public void Init(GameObject prefab, Transform parent, float destroyTime, MonoBehaviour runner, Vector3 gravity, float x_Anpassung, float y_Anpassung, float z_Anpassung, GenericObjectPool<WaterFallObject> pool)
    {
        this.prefab = prefab;
        this.parent = parent;
        this.destroyTime = destroyTime;
        this.runner = runner;
        this.customGravity = gravity;
        this.x_Anpassung = x_Anpassung;
        this.y_Anpassung = y_Anpassung;
        this.z_Anpassung = z_Anpassung;
        this.pool = pool;
    }

    public void New()
    {
        // Prefab instanziieren und deaktivieren
        instance = Object.Instantiate(prefab);
        instance.SetActive(false);
    }

    public void Respawn()
    {
        // Position mit Anpassungen setzen
        instance.transform.position = parent.position + new Vector3(x_Anpassung, y_Anpassung, z_Anpassung);
        instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        instance.SetActive(true);

        // Rigidbody setzen, um Custom Gravity zu aktivieren
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            runner.StartCoroutine(ApplyCustomGravity(rb));
        }

        // Auto-Deaktivierung des Objekts nach einer gewissen Zeit
        runner.StartCoroutine(AutoDeactivate());
    }

    private IEnumerator ApplyCustomGravity(Rigidbody rb)
    {
        while (rb != null && instance.activeInHierarchy)
        {
            rb.velocity += customGravity * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator AutoDeactivate()
    {
        yield return new WaitForSeconds(destroyTime);

        // Objekt deaktivieren und zurück in den Pool legen
        instance.SetActive(false);
        pool.ReturnToPool(this);
    }
}
