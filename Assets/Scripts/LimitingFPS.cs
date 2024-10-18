using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LimitingFPS : MonoBehaviour
{

    void Start()
    {
        // Limiting FPS to 200 so measuring is easier
        Application.targetFrameRate = 200;
    }

    void Update()
    {
      
    }
}
