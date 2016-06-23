//=========================================================
// Author: Bobby Lewis
// Date: 3-19-16
// Version: 1.0
// Description: This code runs the Alien Defender AI
//==========================================================
using UnityEngine;
using System.Collections;
using System;

public class AlienDefender : MonoBehaviour
{
    [SerializeField]
    float range = 2; //Used for drop offset

    //Enums for the FSM
    public enum AIFSM { Wander, Attack, Defend, InShip, ObAv };
    public AIFSM AIState = AIFSM.Wander;
    //Target  variables
    private Transform target;
    public GameObject targetObj;
    public float targetDis, targetDisA;//distance from target
    bool spotted = false;// determines if the enemy has spotted the player before or not


    //AI stat variables
    float speed = 0.7f;// movement speed variable

 

    //variables for Wander 
    Vector3 wanderPoint;
    float wanderRange = 2.0f;//range for how far the enemy will walk during the wander state
    float wanderSpeed = 0.3f;//the speed the enemy will walk when wandering

    //variables for Attack
   // float attackRange = 20.0f;//range before shooting
    Vector3 targetV, enemyV, NLinear;//new vectors for attacking
    public float fireTimer;//cooldown timer for shooting
    private float shotTimer = 1.3f;//cooldown time required for shooting

    //variables for Defend
    private Rigidbody2D rb;

    //variables for ship boardning and deboarding
    public ShipInteraction ShipI;

    //Weapon Invetory Variables 
    public Inventory alienInventory;
    public Guid weaponId;

    //Pathfinding Variables 
    public PathFindDefender path;
    public ArrayList ObList;
    public Node2 startNode { get; set; }
    public Node2 goalNode { get; set; }
    public Vector3 point;
    public  int desIndex = 1;
    public int PDesIndex = 0;

    // Use this for initialization
    void Start()
    {   //invetory for AI
        alienInventory = this.GetComponent<Inventory>();
		Weapon tempWeapon = ScriptableObject.CreateInstance("Weapon") as Weapon;
        tempWeapon.Initialize(weaponLayers.Enemy);
        alienInventory.items.Add(tempWeapon.itemId, tempWeapon);
        weaponId = tempWeapon.itemId;
        //Rigid Body
        rb = GetComponent<Rigidbody2D>();
        fireTimer = 0;//start timer at 0;
        targetObj = GameObject.FindGameObjectWithTag("Player");//target the player
        target = targetObj.transform;//transform of target
        Wander();//start off in wander state
        ShipI = gameObject.GetComponent<ShipInteraction>();//using ShipInteraction.CS
        path = gameObject.GetComponent<PathFindDefender>();//Using TestCode.cs
        ObList = new ArrayList();//new array list to hold all nodes for A* algorithm
    }

    // Update is called once per frame
    void Update()
    {
        targetDis = Vector3.Distance(transform.position, target.position);//magnitude/distane of enemy and target

        if (ShipI.seat != ShipInteraction.ShipSeat.passenger) { State( targetDis); }
        else
        {   AIState = AIFSM.InShip;}

        if (fireTimer <= 2)
        {  fireTimer += Time.deltaTime;}

        switch (AIState)//FSM 
        {
            case AIFSM.Wander:
                if ((transform.position - wanderPoint).magnitude < 2.0f)// if the distance is less than 2.0f
                {
                    Wander();
                }//endif
                else
                {
                    NLinear = Norm(transform.position, wanderPoint);
                    transform.position += (NLinear * wanderSpeed * Time.deltaTime);
                    rotateForward(wanderPoint);
                }
                if (targetDis < 2.5f)
                { spotted = true; }
                break;

            case AIFSM.Attack:
             //   RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward,0.0f);
             //   if (hit)
             ////   {
                //    print(hit.collider.name);
             //   }
                Attack(target);
                break;

            case AIFSM.Defend:
                Defend();
                break;

            case AIFSM.InShip:
                break;

            case AIFSM.ObAv:
                point = asl();
                targetDisA = Vector2.Distance(transform.position, point);
                MovetoPoint(point);
                if (targetDis > 2.8f && targetDis < 3.5f)
                {
                    AIState = AIFSM.Defend;
                    resetAstar();
                }
                if (targetDisA < 0.7f)
                { desIndex++; }
                break;
        }
    }

    //////////////////////////////////////////
    ////////STATE MACHINE FUNCTIONS //////////
    //////////////////////////////////////////

    //determines which state the enemy should be in
    private void State( float distance)
    {
        if (0.8f <= distance && distance <= 2.8f && spotted == true)
        {
            AIState = AIFSM.Attack;
        }//end if
        else if (distance > 2.8f && spotted == true && distance < 3.5f)
        {
            AIState = AIFSM.Defend;
        }
        else if (distance >= 3.5f && distance <= 5.0f && spotted == true)
        {
            AIState = AIFSM.ObAv;
        }
        else if (distance > 5.0f && spotted == true)
        {
            AIState = AIFSM.ObAv;
            resetAstar();
              
        }
    }//State()

    private void Wander()
    {
        wanderPoint = new Vector3(UnityEngine.Random.Range(transform.position.x - wanderRange, transform.position.x + wanderRange), UnityEngine.Random.Range(transform.position.y - wanderRange, transform.position.y + wanderRange), 1.0f);
        wanderPoint.z = 1.0f;
        NLinear = Norm(transform.position, wanderPoint);
        transform.position += (NLinear * wanderSpeed * Time.deltaTime);
    }//Wander()

    private void Attack(Transform target)
    {
        targetV = new Vector3(target.position.x, target.position.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        NLinear = Norm(enemyV, targetV);
        transform.position += (NLinear * speed * Time.deltaTime);
        rotateForward(target.position);
        if (fireTimer > shotTimer)
        {
            var tempWeapon = alienInventory.items[weaponId] as Weapon;
            if (tempWeapon)
            {
                StartCoroutine(tempWeapon.Fire(this.transform));
                fireTimer = 0.0f;
            }
        }
    }//Attack()

    private void MovetoPoint(Vector3 point)
    {

        rotateForward(point);
        targetV = new Vector3(point.x, point.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        NLinear = Norm(enemyV, targetV);
        transform.position += (NLinear * speed * Time.deltaTime);
        
       
    }//Attack2()


    private void Defend()
    {
        if (targetDis <= 5.0f)
        {

            rotateForward(target.position);
            if (targetDis <= 4.0f)
            {
                if (fireTimer > shotTimer)
                {
                    var tempWeapon = alienInventory.items[weaponId] as Weapon;
                    if (tempWeapon)
                    {
                        StartCoroutine(tempWeapon.Fire(this.transform));
                        fireTimer = 0.0f;
                    }
                }
            }
        }
    }//Defend()

    /////////////////////////////////////////////
    //////////extra functions///////////////////
    ////////////////////////////////////////////

    //normalize vectors
    private Vector3 Norm(Vector3 position, Vector3 target)
    {
        Vector3 linear = target - position;
        linear = linear.normalized;
        return linear;
    }//Norm()

    //Ensure the enemy is facing direction it is moving in
    private void rotateForward(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }//rotateforward

    
    /// /////////////////////////////////////////////////////
    /// ///////////////////////A* Functions/////////////////
    /// ///////////////////////////////////////////////////
  
    private Vector3 asl()
	{
		Vector3 pos;
		path = gameObject.GetComponent<PathFindDefender>();
		if (path)
		{
			if (PDesIndex == 0 && path.pathArray.Count > 0)
			{
				ObList = path.pathArray;
				desIndex = 0;
				PDesIndex++;
			}
		}
		drawpath();
        pos = getDestination(ObList);
        return pos;
    }

    private Vector3 getDestination(ArrayList path)
    {
        Vector3 pos = new Vector3();
            if (desIndex < path.Count)
            {
            Node2 nextNode = (Node2)path[desIndex];
            pos = nextNode.position;
            }

        if (desIndex >= path.Count) {//AIState = AIFSM.Defend;
            PDesIndex = 0; }
        return pos;
    }//getdestination;

    void resetAstar() {
        PDesIndex = 0;
        desIndex = 0;
    }



    void drawpath()
    {
        if (ObList == null)
            return;
        if (ObList.Count > 0)
        {
            int index = 1;
            foreach (Node2 node in ObList)
            {
                if (index < ObList.Count)
                {
                    Node2 nextNode = (Node2)ObList[index];
                    Debug.DrawLine(node.position, nextNode.position,
                    Color.green);

                    index++;
                }
            }
        }

    }//drawpath()

    //Adam - Creating a drop when enemy is destroyed based on their inventory, and a random exp drop amount.
    void Drops()
    {
        var expDrop = Resources.Load("Prefabs/ExpPickup");
        var exp = UnityEngine.Random.Range(1.0f, 3.0f);
        for (int i = 0; i < exp; i++)
        {
            Vector3 offset = new Vector3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-2, 2), 0);
            Instantiate(expDrop, transform.position + offset, transform.rotation);
        }

        var worldDrop = Resources.Load("Prefabs/WorldPickup");
        var temp = (GameObject)Instantiate(worldDrop, transform.position, transform.rotation);
        temp.GetComponent<WorldDrop>().Merge(alienInventory.items);

    }

  
        



    }
