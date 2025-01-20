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
    public float waterFallRate = 0.5f;
    public float destroyWaterFall = 10f;
    private Vector3 waterPosition;
    public Vector3 customGravity = new Vector3(0,-3,0);
    public float x_Anpassung = 3f;
    public float y_Anpassung = -5f;
    public float z_Anpassung = 0f;


    void Start()
    {
        // Wasserfall läuft durchgehend
        InvokeRepeating("WaterfallActive", 0f, waterFallRate);

        // Position anpassen
        waterPosition = rockLocation.position;
        waterPosition.x += x_Anpassung;
        waterPosition.y += y_Anpassung;
        waterPosition.z += z_Anpassung;

    }


    void WaterfallActive()
    {
        // WasserFallImage instantiaten
        GameObject waterFall = Instantiate(waterFallImage, waterPosition, Quaternion.Euler(0, 180, 0));
        
        Rigidbody rb = waterFall.GetComponent<Rigidbody>();
        rb.useGravity = false;

        StartCoroutine(ApplyCustomGravity(rb));
        StartCoroutine(WaterFallDestruction(waterFall));
    }

    private IEnumerator ApplyCustomGravity(Rigidbody rb)
    {
        while (rb != null)
        {
            rb.velocity += customGravity * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator WaterFallDestruction(GameObject obj)
    {
        // WasserFallImage nach Zyklus einfach zerstören
        yield return new WaitForSeconds(destroyWaterFall);
        Destroy(obj);
    }
}
