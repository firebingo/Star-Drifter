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

    //Enums for the FSM
    public enum AIFSM { Wander, Attack, Defend, InShip, ObAv };
    public AIFSM AIState = AIFSM.Wander;

    //Target  variables
    private Transform target;
    public GameObject targetObj;
    public float targetDis;//distance from target
    bool spotted = false;// determines if the enemy has spotted the player before or not


    //AI stat variables
    float speed = 0.7f;// movement speed variable

    //max variables for AI
    float maxRotation;// max rotation of the enemy
    float maxSpeed;//max speed of the enemy

    //variables for Wander 
    Vector3 wanderPoint;
    float wanderBreak = 0.0f;//break time for the wander state
    float wanderRange = 2.0f;//range for how far the enemy will walk during the wander state
    float wanderSpeed = 0.3f;//the speed the enemy will walk when wandering

    //variables for Attack
    float attackRange = 20.0f;//range before shooting
    Vector3 targetV, enemyV, NLinear;//new vectors for attacking
    public float fireTimer;//cooldown timer for shooting
    private float shotTimer = 1.3f;//cooldown time required for shooting

    //variables for Defend
    private Rigidbody2D rb;

    //variables for ship boardning and deboarding
    public ShipInteraction ShipI;

    public Inventory alienInventory;
    public Guid weaponId;

    public TestCode path;

    //
    public ArrayList ObList;
    public Node2 startNode { get; set; }
    public Node2 goalNode { get; set; }
   public  Vector3 point;

   public  int desIndex = 1;
    int PDesIndex = 0;

    private float elapsedTime = 0.0f;
    //Interval time between pathfinding
    private float intervalTime = 1.0f;

    // Use this for initialization
    void Start()
    {
        alienInventory = this.GetComponent<Inventory>();
        Weapon tempWeapon = new Weapon();
        tempWeapon.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 5f, 5f, shotTimer, 3f, weaponTypes.Pistol, weaponLayers.Enemy);
        alienInventory.items.Add(tempWeapon.itemId, tempWeapon);
        weaponId = tempWeapon.itemId;

        rb = GetComponent<Rigidbody2D>();
        fireTimer = 0;//start timer at 0;
        targetObj = GameObject.FindGameObjectWithTag("Player");//target the player
        target = targetObj.transform;//transform of target
        Wander();//start off in wander state
        ShipI = gameObject.GetComponent<ShipInteraction>();
        path = gameObject.GetComponent<TestCode>();

        ObList = new ArrayList();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(target.position, transform.position, Color.yellow);//Make sure that the right target is being pointed too
        targetDis = Vector3.Magnitude(transform.position - target.position);//magnitude/distane of enemy and target

        if (ShipI.seat != ShipInteraction.ShipSeat.passenger) { State(target, targetDis); }
        else
        {
            AIState = AIFSM.InShip;
        }

        if (fireTimer <= 2)
        {
            fireTimer += Time.deltaTime;
        }

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
                Attack(target);
                break;

            case AIFSM.Defend:
                Defend();
                break;
            case AIFSM.InShip:
                break;

            case AIFSM.ObAv:

                
               point = asl();
                //  targetDis = Vector3.Magnitude(transform.position - point);//magnitude/distane of enemy and target
                targetDis = Vector3.Distance(transform.position, point);
              //  if (targetDis < 0.6f)
              //  { desIndex++; }
                Attack2(point);
                if (targetDis < 0.6f)
                { desIndex++; }

                //ObstacleAvoid(target.position);


                break;

        }
    }

    //////////////////////////////////////////
    ////////STATE MACHINE FUNCTIONS //////////
    //////////////////////////////////////////

    //determines which state the enemy should be in
    private void State(Transform target, float distance)
    {

        if (0.8f <= distance && distance <= 2.8f && spotted == true)
        {
            AIState = AIFSM.Attack;
        }//end if
        else if (distance >= 2.8f && spotted == true)
        {
            AIState = AIFSM.Defend;
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
                tempWeapon.Fire(this.transform);
                fireTimer = 0.0f;
            }
        }
    }//Attack()

    private void Attack2(Vector3 point)
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
                        tempWeapon.Fire(this.transform);
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

    private Vector3 asl()
    {
        Vector3 pos;
        if (PDesIndex == 0) { 
        ObList = path.pathArray;
            PDesIndex++;
    }

        drawpath();

        pos = getDestination(ObList);

        return pos;
        
        // Debug.Log("yay");
    }

    private void ObstacleAvoid(Vector3 target)
    {
        startNode = new Node2(GridManager.instance.GetGridCellCenter(
        GridManager.instance.GetGridIndex(transform.position)));

        goalNode = new Node2(GridManager.instance.GetGridCellCenter(
        GridManager.instance.GetGridIndex(target)));

        ObList = AStar.FindPath(startNode, goalNode);

    }

    private Vector3 getDestination(ArrayList path)
    {
        Vector3 pos = new Vector3();
       //  int desIndex = 1;
      //  foreach (Node2 node in ObList)
      //  {
            if (desIndex < ObList.Count)
            {

                //float howfar = 0.0f;
                Node2 nextNode = (Node2)ObList[desIndex];

            pos = nextNode.position;

            //    targetV = new Vector3(nextNode.position.x, nextNode.position.y, 0);
               // enemyV = new Vector3(transform.position.x, transform.position.y, 0);
               // NLinear = Norm(enemyV, targetV);
              //  transform.position += (NLinear * speed * Time.deltaTime);
              //  rotateForward(target.position);

               // howfar = Vector3.Magnitude(transform.position - nextNode.position);




                //Debug.DrawLine(node.position, nextNode.position,
                //Color.green);
               
                  //  desIndex++;
             //   }

            }
       // }

        return pos;
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

    }






}
