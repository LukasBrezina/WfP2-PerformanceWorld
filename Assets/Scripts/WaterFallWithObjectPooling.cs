using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.UI;

public class WaterFallWithObjectPooling : MonoBehaviour
{

    /* 
    Selbe Logik wie WaterfallWithoutObjectPooling, einziger Unterschied
    liegt darin, dass die WasserFallImages nicht Instantiated und am Ende ihres Lebenszyklus destroyed werden,
    sondern:
    Es werden "poolSize"-viele Objekte sofort zu Beginn erzeugt, deaktiviert und in den Pool (Queue) gelegt.
    Dort liegen sie dann inaktiv herum bis sie gebraucht werden. Wenn sie gebraucht werden, dann werden sie einfach nur
    aktiviert. Nach ihrem Lebenszyklus werden sie deaktiviert und zurück in den Pool gelegt.

    */

    public GameObject waterFallImage;
    public Transform rockLocation;
    public float waterFallRate = 0.1f;
    public float destroyWaterFall = 2f;
    private Vector3 waterPosition;
    public int poolSize = 20;

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
        waterPosition.x += 5f;

    }

    void WaterFallActive()
    {
        if (waterFallPool.Count > 0)
        {
            // WasserFallImage aus der Queue holen, Position nach vorne anpassen (sonst direkt im Stein), rotieren und aktivieren
            GameObject waterFall = waterFallPool.Dequeue();
            waterFall.transform.position = waterPosition;
            waterFall.transform.rotation = Quaternion.Euler(0, 180, 0);
            waterFall.SetActive(true);

            StartCoroutine(WaterFallDeactivating(waterFall));
        }
    }

    private IEnumerator WaterFallDeactivating(GameObject waterFall)
    {
        yield return new WaitForSeconds(destroyWaterFall);
        // am Ende des Zyklus deaktivieren statt zerstören
        waterFall.SetActive(false);
        waterFallPool.Enqueue(waterFall);
    }

}