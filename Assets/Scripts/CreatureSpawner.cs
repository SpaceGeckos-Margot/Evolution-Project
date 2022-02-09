using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureSpawner : MonoBehaviour
{   
    public int startNum = 100;
    public Transform Creature;
    
    // Start is called before the first frame update
    void Start()
    {
         for (int i = 0; i < startNum; i++) {
            var myCreature = Instantiate(Creature);
            var axis = Random.Range(1, 100);
            if (axis >= 50){
                var j = Random.Range(1, 100);
                var x1 = 0;
                if (j >= 50) {
                    x1 = 25;
                } else {
                    x1 = -25;

                }
                var z1 = Random.Range(-24, 24);
                Vector3 spawnHere = new Vector3(x1, 2, z1);
                myCreature.position = spawnHere;
                
            } else {
                var k = Random.Range(1, 100);
                var z2 = 0;
                if (k >= 50) {
                    z2 = 25;
                } else {
                    z2 = -25;
                }
                var x2 = Random.Range(-24, 24);
                Vector3 spawnHere = new Vector3(x2, 2, z2);
                myCreature.position = spawnHere;
              
            }
                
        }

        
    }
}
