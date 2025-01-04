using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallWithObjectPooling : MonoBehaviour
{
    public GameObject waterFallImage;
    public Transform rockLocation;
    public float waterFallRate = 0.5f;
    public float destroyWaterFall = 10f;
    private Vector3 waterPosition;
    // 22 da 2 WaterFallImage pro Sekunde * Destroy nach 10 Sekunden = 20 -> +2 für Puffer
    public int poolSize = 20;
    public Vector3 customGravity = new Vector3(0, -3, 0);

    private Queue<GameObject> waterFallPool;

    void Start()
    {
        waterFallPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject waterFall = Instantiate(waterFallImage);
            waterFall.SetActive(false);
            waterFallPool.Enqueue(waterFall);
        }

        InvokeRepeating("WaterFallActive", 0f, waterFallRate);

        // Position anpassen
        waterPosition = rockLocation.position;
        waterPosition.x += 3f;
        waterPosition.y -= 5f;
    }

    void WaterFallActive()
    {
        if (waterFallPool.Count > 0)
        {
            // WasserFallImage aus der Queue holen, Position und Zustand anpassen
            GameObject waterFall = waterFallPool.Dequeue();
            waterFall.transform.position = waterPosition;
            waterFall.transform.rotation = Quaternion.Euler(0, 180, 0);
            waterFall.SetActive(true);

            Rigidbody rb = waterFall.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            StartCoroutine(ApplyCustomGravity(rb));
            StartCoroutine(WaterFallDeactivating(waterFall));
        }
    }

    private IEnumerator ApplyCustomGravity(Rigidbody rb)
    {
        while (rb != null && rb.gameObject.activeInHierarchy)
        {
            rb.velocity += customGravity * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator WaterFallDeactivating(GameObject waterFall)
    {
        yield return new WaitForSeconds(destroyWaterFall);

        // Rigidbody-Zustand zurücksetzen
        Rigidbody rb = waterFall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Objekt deaktivieren und zurück in den Pool legen
        waterFall.SetActive(false);
        waterFallPool.Enqueue(waterFall);
    }
}
