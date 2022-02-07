using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    //private bool foodWait;
    private float foodTimer;
    // Start is called before the first frame update
    void Start()
    {
        //foodWait = false;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (Timer.timerOn == false)
        {
            Destroy(gameObject);
        }
    }
    
    
}
