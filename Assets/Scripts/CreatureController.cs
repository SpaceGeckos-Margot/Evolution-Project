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
    public float s;
    public Vector3 size;

    // energy timer
    public bool energyTimer;
    public float energyUsed;
    
    // other variables
    public int state = 0;
    public bool alive = true;
    public int foodEaten = 0;
    public bool creatureEaten = false;
    GameObject nearObject; //declared here so all functions can access it
    
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        
        state = 0;
        foodEaten = 0;
        energyUsed = 0;

        
        if (Timer.dayCount == 0){
            sense = Random.Range(15f, 30f);
            speed = Random.Range(3f, 5f);
            agent.speed = speed;
            s = Random.Range(2f, 3.5f);
            size = new Vector3(s, s, s);    
            this.transform.localScale = size;
            Counter.totalSpeed += speed;
            Counter.totalCreatures++;
            
        }
        
        

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
            energyUsed = energyUsed += (Time.deltaTime * speed * (size.x/2));
            this.name = energyUsed.ToString();

        }
        if (energyUsed >= 80) {
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
        else if (state == 5) {
            findCreature();
           
        }
        if (alive == false){
            Destroy(gameObject);
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
            
            
            
        } 
        if( other.gameObject.CompareTag("Creature")) 
        {
            if (Timer.snackTime == true && creatureEaten == false)
            {
                if (other.gameObject.GetComponent<CreatureController>().s <= this.s) 
                {      
                    Destroy(other.gameObject);
                    other.gameObject.GetComponent<CreatureController>().alive = false;
                    foodEaten = foodEaten += 2;
                    energyTimer = false;
                    creatureEaten = true;
                    
                }
            }
        }
        if (foodEaten >= 2)
            {
                state = 3;
                //return to edge
            } else
            {
                state = 0;
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
            
            
        }
        if (state != 1)
        {
            if (Timer. snackTime == true && creatureEaten == false) {
                state = 5;
            } else {
                state = 2;
            }
            

        }
                
        
        
    }
    void findCreature() 
    {   
        Collider[] nearObjects = Physics.OverlapSphere(agent.transform.position, sense * 1.3f);
        for (int i = 0; i < nearObjects.Length; i++) // loops over objects, not foreach because nearobject has already been defined
        {
            nearObject = nearObjects[i].gameObject; // nearobjects[] is a collider array, this grabs the gameobject from said collider
            if (nearObject.CompareTag("Creature"))
            {   
                    // creature size check
                if (nearObject.GetComponent<CreatureController>().s <= this.s) 
                {      
                    agent.SetDestination(nearObject.transform.position); 
                    state = 1;
                }
                
                break;
            }
            
            
        }
        if (state != 1)
        {
            state = 2;
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
        newCreature.GetComponent<CreatureController>().sense = this.sense * Random.Range(.95f, 1.05f);

        newCreature.GetComponent<CreatureController>().speed = this.speed * Random.Range(.95f, 1.05f);
        newCreature.GetComponent<NavMeshAgent>().speed = newCreature.GetComponent<CreatureController>().speed;
        Counter.totalSpeed += newCreature.GetComponent<CreatureController>().speed;
        Counter.totalCreatures++;


        newCreature.GetComponent<CreatureController>().s = this.s * Random.Range(.95f, 1.05f);
        newCreature.GetComponent<CreatureController>().size = new Vector3(s, s, s);
        newCreature.GetComponent<CreatureController>().transform.localScale = size;
        
        
    }
    
}

