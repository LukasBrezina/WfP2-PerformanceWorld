using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class WaterFallObject : IPoolableObject
{
    private GameObject instance;
    private GameObject waterFallImage;
    private Transform stoneLocation;
    private float destroyTime;
    private MonoBehaviour script;
    private Vector3 customGravity;
    private float x_Anpassung;
    private float y_Anpassung;
    private float z_Anpassung;
    private GenericObjectPool<WaterFallObject> pool;

    // create object function
    public void Init(GameObject waterFallImage, Transform stoneLocation, float destroyTime, MonoBehaviour script, Vector3 gravity, float x_Anpassung, float y_Anpassung, float z_Anpassung, GenericObjectPool<WaterFallObject> pool)
    {
        this.waterFallImage = waterFallImage;
        this.stoneLocation = stoneLocation;
        this.destroyTime = destroyTime;
        this.script = script;
        this.customGravity = gravity;
        this.x_Anpassung = x_Anpassung;
        this.y_Anpassung = y_Anpassung;
        this.z_Anpassung = z_Anpassung;
        this.pool = pool;
    }

    // instantiate object and deactivate it
    public void New()
    {
        instance = Object.Instantiate(waterFallImage);
        instance.SetActive(false);
    }

    public void Respawn()
    {
        // set position and activate
        instance.transform.position = stoneLocation.position + new Vector3(x_Anpassung, y_Anpassung, z_Anpassung);
        instance.transform.rotation = Quaternion.Euler(0, 180, 0);
        instance.SetActive(true);

        // custom gravity 
        Rigidbody rb = instance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            script.StartCoroutine(ApplyCustomGravity(rb));
        }

        // after specified time set inactive
        script.StartCoroutine(AutoDeactivate());
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

        // deactivate object and return to pool
        instance.SetActive(false);
        pool.ReturnToPool(this);
    }
}
