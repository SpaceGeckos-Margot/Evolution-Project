using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    Light mainLight;
    // Start is called before the first frame update
    void Start()
    {
        mainLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer.endOfDay == true){
        mainLight.intensity = 0.3f;
        }
        else {
            mainLight.intensity = 1f;
        }
    }
}
