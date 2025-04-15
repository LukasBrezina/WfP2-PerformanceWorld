using UnityEngine;
using System.Collections;

public class GenericObjectPoolScript : MonoBehaviour
{
    public GameObject waterFallImage;       // Dein Prefab
    public Transform rockLocation;          // Wo das Wasser spawnen soll
    public float waterFallRate = 0.5f;      // Spawn-Rate
    public float destroyWaterFall = 10f;    // Lebenszeit jedes Objekts
    public int poolSize = 20;               // Pool-Größe

    public float x_Anpassung = 3f;          // X-Anpassung für Position
    public float y_Anpassung = -5f;         // Y-Anpassung für Position
    public float z_Anpassung = 0f;          // Z-Anpassung für Position

    public Vector3 customGravity = new Vector3(0, -3, 0);  // Benutzerdefinierte Schwerkraft

    private GenericObjectPool<WaterFallObject> waterFallPool;

    void Start()
    {
    waterFallPool = new GenericObjectPool<WaterFallObject>();

    for (int i = 0; i < poolSize; i++)
    {
        var obj = new WaterFallObject();
        obj.Init(waterFallImage, rockLocation, destroyWaterFall, this, customGravity, x_Anpassung, y_Anpassung, z_Anpassung, waterFallPool);
        obj.New(); // Jetzt korrekt, da Prefab gesetzt wurde
        waterFallPool.ReturnToPool(obj);
    }

    InvokeRepeating(nameof(ActivateWaterFall), 0f, waterFallRate);
    }


    void ActivateWaterFall()
    {
        // Objekt aus dem Pool holen und aktivieren
        var obj = waterFallPool.Spawn();
    }
}
