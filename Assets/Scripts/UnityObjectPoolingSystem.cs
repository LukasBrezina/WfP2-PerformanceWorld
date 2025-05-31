using System.Collections;
using UnityEngine;
// Unity Object Pooling System
using UnityEngine.Pool;

public class UnityObjectPoolingSystem : MonoBehaviour
{
    public GameObject waterFallImage;
    public Transform rockLocation;
    public float waterFallRate = 0.5f;
    public float destroyWaterFall = 10f;
    public int poolSize = 20;

    public float x_Anpassung = 3f;
    public float y_Anpassung = -5f;
    public float z_Anpassung = 0f;

    public Vector3 customGravity = new Vector3(0, -3, 0);

    private IObjectPool<GameObject> objectPool;
    private Vector3 waterPosition;

    void Awake()
    {
        // object pool from unity
        objectPool = new ObjectPool<GameObject>(
            CreateWaterFall,
            OnTakeFromPool,
            OnReturnedToPool,
            OnDestroyWaterFall,
            true,
            poolSize,
            poolSize
        );
    }

    void Start()
    {
        // change position
        waterPosition = rockLocation.position + new Vector3(x_Anpassung, y_Anpassung, z_Anpassung);
        InvokeRepeating(nameof(WaterFallActive), 0f, waterFallRate);
    }

    // get object, adjust position and gravity
    void WaterFallActive()
    {
        GameObject waterFall = objectPool.Get();

        waterFall.transform.position = waterPosition;
        waterFall.transform.rotation = Quaternion.Euler(0, 180, 0);

        Rigidbody rb = waterFall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            StartCoroutine(ApplyCustomGravity(rb));
        }

        StartCoroutine(WaterFallDeactivating(waterFall));
    }

    // create object
    private GameObject CreateWaterFall()
    {
        GameObject waterFall = Instantiate(waterFallImage);
        waterFall.SetActive(false);
        return waterFall;
    }

    // activate object
    private void OnTakeFromPool(GameObject waterFall)
    {
        waterFall.SetActive(true);
    }

    // deactivate object
    private void OnReturnedToPool(GameObject waterFall)
    {
        waterFall.SetActive(false);
    }

    // destroy object
    private void OnDestroyWaterFall(GameObject waterFall)
    {
        Destroy(waterFall);
    }

    // set custom gravity
    private IEnumerator ApplyCustomGravity(Rigidbody rb)
    {
        while (rb != null && rb.gameObject.activeInHierarchy)
        {
            rb.velocity += customGravity * Time.deltaTime;
            yield return null;
        }
    }

    // return object to pool after a specified time interval
    private IEnumerator WaterFallDeactivating(GameObject waterFall)
    {
        yield return new WaitForSeconds(destroyWaterFall);

        Rigidbody rb = waterFall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        objectPool.Release(waterFall);
    }
}
