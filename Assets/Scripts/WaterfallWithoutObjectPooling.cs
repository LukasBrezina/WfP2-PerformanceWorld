using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterfallWithoutObjectPooling : MonoBehaviour
{

    /*
    Funktion:
    Es werden jede "waterFallRate"-Sekunde ein WaterFallImage instantiated und nach dem Lebenszyklus
    "destroyWaterFall"-Sekunden einfach zerstört.
    */

    public GameObject waterFallImage;
    public Transform rockLocation;
    public float waterFallRate = 0.1f;
    public float destroyWaterFall = 2f;
    private Vector3 waterPosition;



    void Start()
    {
        // Wasserfall läuft durchgehend
        InvokeRepeating("WaterfallActive", 0f, waterFallRate);

        // Position anpassen
        waterPosition = rockLocation.position;
        waterPosition.x += 5f;

    }


    void WaterfallActive()
    {
        // WasserFallImage instantiaten
        GameObject waterFall = Instantiate(waterFallImage, waterPosition, Quaternion.Euler(0, 180, 0));
        StartCoroutine(WaterFallDestruction(waterFall));
    }

    private IEnumerator WaterFallDestruction(GameObject obj)
    {
        // WasserFallImage nach Zyklus einfach zerstören
        yield return new WaitForSeconds(destroyWaterFall);
        Destroy(obj);
    }
}
