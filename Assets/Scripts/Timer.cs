using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static bool timerOn;
    public static float time;
    public static bool endOfDay;
    public static float dayCount = 0;
    public static bool snackTime;
    // Start is called before the first frame update
    void Start()
    {
        timerOn = false;
        snackTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {    
            time = 0;
            timerOn = true;
            endOfDay = false;
            //Debug.Log("space");
            dayCount += 1;
        }
        if (timerOn == true) 
        {

            time = time += Time.deltaTime;
           // Debug.Log(time);
        }

        if (time >= 3 && time <= 9) 
        {
            snackTime = true;
        } else 
        {
            snackTime = false;
        }

        if (time >= 20)
        {
            timerOn = false;
            endOfDay = true;
        }
      
    }
}
