using UnityEngine;
using System.Collections;

public class GenericObjectPoolScript : MonoBehaviour
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

    private GenericObjectPool<WaterFallObject> waterFallPool;

    void Start()
    {
    waterFallPool = new GenericObjectPool<WaterFallObject>();

    for (int i = 0; i < poolSize; i++)
    {
        // create waterFallObject, init and set inactive
        var obj = new WaterFallObject();
        obj.Init(waterFallImage, rockLocation, destroyWaterFall, this, customGravity, x_Anpassung, y_Anpassung, z_Anpassung, waterFallPool);
        obj.New(); 
        waterFallPool.ReturnToPool(obj);
    }

    InvokeRepeating(nameof(ActivateWaterFall), 0f, waterFallRate);
    }


    void ActivateWaterFall()
    {
        // get object from pool and activate
        var obj = waterFallPool.Spawn();
    }
}
