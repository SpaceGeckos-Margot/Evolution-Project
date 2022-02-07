using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public int totalFood = 75;
    public Transform Food;
    private bool foodDrop;

    // Start is called before the first frame update
    void Update()
    {   
        if (Input.GetKeyDown("space"))
        {
            for (int i = 0; i < totalFood; i++) 
            {   
                var food = Instantiate(Food);
                var x1 = Random.Range(-22, 22);
                var z1 = Random.Range(-22, 22);
                Vector3 spawnHere = new Vector3(x1, 0, z1);
                food.position = spawnHere;
            }
        }
    }
    
   
}
