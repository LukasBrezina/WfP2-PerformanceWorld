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
    // fixed pool size, changed via inspector to 100 and 200
    public int poolSize = 20;

    public float x_Anpassung = 3f;
    public float y_Anpassung = -5f;
    public float z_Anpassung = 0f;

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

        // adjust position
        waterPosition = rockLocation.position;
        waterPosition.x += x_Anpassung;
        waterPosition.y += y_Anpassung;
        waterPosition.z += z_Anpassung;
    }

    void WaterFallActive()
    {
        if (waterFallPool.Count > 0)
        {
            // get object from queue, adjust position and rotation
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

        Rigidbody rb = waterFall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // deactivate object and return to pool
        waterFall.SetActive(false);
        waterFallPool.Enqueue(waterFall);
    }
}
