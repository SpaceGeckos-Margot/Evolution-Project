using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Mathf;
public class CreatureController : MonoBehaviour
{
    NavMeshAgent agent;
    // creature stats 
    public float sense;
    public float speed;

    // energy timer
    public bool energyTimer;
    public float energyUsed;
    
    // other variables
    public int state = 0;
    public bool alive = true;
    public int foodEaten = 0;
    GameObject nearObject; //declared here so all functions can access it
    
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        
        state = 0;
        foodEaten = 0;
        energyUsed = 0;
        
        
        sense = Random.Range(15, 30);
        speed = Random.Range(3, 5);
        agent.speed = speed;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("space")) 
        {
            if (foodEaten >= 2)
            {
                Mitosis();
            }
            state = 0;
            foodEaten = 0;
            energyUsed = 0;
            
            
            // energy timer stopwatch variable back to zero
        }
        
        if (Timer.timerOn == false && Timer.endOfDay == true && state != 4) 
        {
            state = 3;
            
        }
        if (energyTimer == true) 
        {
            energyUsed = energyUsed += (Time.deltaTime * speed);
            this.name = energyUsed.ToString();
            //Debug.Log(energyUsed);
            //multiply by speed variable later

        }
        if (energyUsed >= 44) {
            alive = false;            

        }
        

        if (state == 0)
        {
            if (Timer.timerOn == true) {
            findFood();
            }    
        }
        else if (state == 1) 
        {
            energyTimer = true;
            //Debug.Log(energyTimer);
            // if destination no longer active --> set state back to 0
            if (nearObject == null) {
                state = 0;
            }
        }
        else if (state == 2)    
        {
            
            Wander();
            energyTimer = true;
            
            
        }
        else if (state == 3)
        {
                        
            returnToEdge();
            energyTimer = true;
            if (agent.transform.position.x >= 24 || agent.transform.position.x <= -24 || agent.transform.position.z >= 24 || agent.transform.position.z <= -24) {
                state = 4;
            }

            if (Timer.endOfDay)
            {
                if (foodEaten == 0)
                {
                    alive = false;
                }
                       
            }
            
        }
        else if (state == 4) {
            // flip energy timer switch off
           energyTimer = false;
           //Debug.Log(energyTimer);
           
        }
        if (alive == false){
            Destroy(gameObject);
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {   
    
        if(other.gameObject.CompareTag("Food")) 
        {
            Destroy(other.gameObject);
            foodEaten++;
            energyTimer = false;
            
            if (foodEaten >= 2)
            {
                state = 3;
                //return to edge
            } else
            {
                state = 0;
            }
            
        }

    }

    void findFood()
    {
        
        
        Collider[] nearObjects = Physics.OverlapSphere(agent.transform.position, sense);

        for (int i = 0; i < nearObjects.Length; i++) // loops over objects, not foreach because nearobject has already been defined
        {
            nearObject = nearObjects[i].gameObject; // nearobjects[] is a collider array, this grabs the gameobject from said collider
            if (nearObject.CompareTag("Food"))
            {        
                agent.SetDestination(nearObject.transform.position); 
                state = 1;
                
                
                break;
            }
            if (state != 1)
            {
                state = 2;
            }
            
        }
        
    }
    

    void Wander()
    {
        var i = Random.Range(-10, 10);
        var j = Random.Range(-10, 10);

        var newX = agent.transform.position.x + i;
        var newY = agent.transform.position.y;
        var newZ = agent.transform.position.z + j;

        Vector3 wanderHere = new Vector3(newX, newY, newZ);
        agent.SetDestination(wanderHere);
        if (agent.transform.position == wanderHere)
        {
            state = 0;
        }    
    }

   void returnToEdge()
    {
        var X = agent.transform.position.x;
        var Y = agent.transform.position.y;
        var Z = agent.transform.position.z;

        if (Abs(X) >= Abs(Z))
        {
            if (X >= 0) {
                Vector3 homeSpot = new Vector3(25, 2, Z);
                agent.SetDestination(homeSpot);
            } else {
                Vector3 homeSpot = new Vector3(-25, 2, Z);
                agent.SetDestination(homeSpot);
            }
        } else {
            if (Z >= 0) {
                Vector3 homeSpot = new Vector3(X, 2, 25);
                agent.SetDestination(homeSpot);
            } else {
                Vector3 homeSpot = new Vector3(X, 2, -25);
                agent.SetDestination(homeSpot);
            }
        }
    }

    void Mitosis() 
    {    
        var newCreature = Instantiate(this.gameObject);
        newCreature.GetComponent<CreatureController>().sense = sense * Random.Range(.95f, 1.05f);
        newCreature.GetComponent<CreatureController>().speed = speed * Random.Range(.95f, 1.05f);
    }
    
}

