//=========================================================
// Author: Bobby Lewis
// Date: 3-22-16
// Version: 1.0
// Description: This code runs the Alien Attacker AI
//==========================================================

using UnityEngine;
using System.Collections;
using System;

public class AlienAttacker : MonoBehaviour
{
    [SerializeField]
    float range = 2; //used for drop offset

    //enum for FSM
    public enum AIFSM { Patrol, Attack, Flee, ObAv }
    public AIFSM AIState = AIFSM.Patrol;

    //variables for AI stats
    float speed = 0.7f;


    //variables for patrol
    private float patrolRange = 2.0f;
    private Vector3 patrolPoint;
    private float patrolSpeed = 0.3f;

    //variables for attack
    Vector3 targetV, enemyV, NLinear;
    public float fireTimer;//cooldown timer for shooting
    private float shotTimer = 1.3f;//cooldown time required for shooting

    //variables for flee
    private float fleeSpeed = 0.5f;

    public Inventory alienInventory;
    public Guid weaponId;

    //variables for targeting player
   public GameObject targetObj;
    private Transform target;
    private float targetDis;
    private bool spotted = false;


    //Pathfinding Variables 
    public PathfindAttacker path;
    public ArrayList ObList;
    public Node2 startNode { get; set; }
    public Node2 goalNode { get; set; }
    public Vector3 point;
    public int desIndex = 1;
    public int PDesIndex = 0;
    public float targetDisA;

    // Use this for initialization
    void Start()
    {
        alienInventory = this.GetComponent<Inventory>();
        Weapon tempWeapon = ScriptableObject.CreateInstance("Weapon") as Weapon;
        tempWeapon.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 5f, 5f, shotTimer, 3f, 1f, 32, weaponTypes.Pistol, weaponLayers.Enemy);
        alienInventory.items.Add(tempWeapon.itemId, tempWeapon);
        weaponId = tempWeapon.itemId;

        targetObj = GameObject.FindGameObjectWithTag("Player");
        target = targetObj.transform;
        path = gameObject.GetComponent<PathfindAttacker>();//Using TestCode.cs
        ObList = new ArrayList();//new array list to hold all nodes for A* algorithm
        Patrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObj == null) { 
        targetObj = GameObject.FindGameObjectWithTag("Player");
        target = targetObj.transform;
        }
        Debug.DrawLine(target.position, transform.position, Color.yellow);//Make sure that the right target is being pointed too
        targetDis = Vector3.Magnitude(transform.position - target.position);//magnitude/distane of enemy and target
        State(target, targetDis);

        switch (AIState)
        {
            case AIFSM.Patrol:
                if ((transform.position - patrolPoint).magnitude < 2.0f)// if the distance is less than 2.0f
                {
                    Patrol();
                }
                else
                {
                    NLinear = Norm(patrolPoint, transform.position);
                    transform.position += (NLinear * patrolSpeed * Time.deltaTime);
                    rotateForward(patrolPoint);
                }
                if (targetDis < 3.0f)
                { spotted = true; }
                break;
            case AIFSM.Attack:
                Attack(target);
                break;
            case AIFSM.Flee:
                Flee(target);
                break;
            case AIFSM.ObAv:
                point = asl();
                targetDisA = Vector2.Distance(transform.position, point);
                MovetoPoint(point);
                if (targetDis > 2.8f && targetDis < 3.5f)
                {
                    AIState = AIFSM.Attack;
                    resetAstar();
                }
                if (targetDisA < 0.7f)
                { desIndex++; }
                break;

        }//end switch
    }

    //State Machine Functions
    //determines which state the enemy should be in
    private void State(Transform target, float distance)
    {
        if (0.8f <= distance && distance <= 2.8f && spotted == true)
        {
            AIState = AIFSM.Attack;
        }//end if
        else if (distance > 5.0f && spotted == true)
        {
            AIState = AIFSM.ObAv;
            resetAstar();

        }
    }//State()

    private void Patrol()
    {
        patrolPoint = new Vector3(UnityEngine.Random.Range(transform.position.x - patrolRange, transform.position.x + patrolRange), UnityEngine.Random.Range(transform.position.y - patrolRange, transform.position.y + patrolRange), 1.0f);
        patrolPoint.z = 1.0f;
        NLinear = Norm(patrolPoint, transform.position);
        transform.position += (NLinear * patrolSpeed * Time.deltaTime);

    }//Patrol

    private void Attack(Transform target)
    {
        targetV = new Vector3(target.position.x, target.position.y, -1);
        enemyV = new Vector3(transform.position.x, transform.position.y, -1);
        NLinear = Norm(targetV, enemyV);
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

    private void Flee(Transform target)
    {
        targetV = new Vector3(target.position.x, target.position.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        NLinear = Norm(enemyV, targetV);
        transform.position += (NLinear * speed * Time.deltaTime);
        rotateForward(transform.position);
    }
    /// /////////////////////////////////////////////////////
    /// ///////////////////////A* Functions/////////////////
    /// ///////////////////////////////////////////////////

    private Vector3 asl()
    {
        Vector3 pos;
        path = gameObject.GetComponent<PathfindAttacker>();
        if (PDesIndex == 0 && path.pathArray.Count > 0)
        {
            ObList = path.pathArray;
            desIndex = 0;
            PDesIndex++;
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

        if (desIndex >= path.Count)
        {//AIState = AIFSM.Defend;
            PDesIndex = 0;
        }
        return pos;
    }//getdestination;

    void resetAstar()
    {
        PDesIndex = 0;
        desIndex = 0;
    }

    private void MovetoPoint(Vector3 point)
    {

        rotateForward(point);
        targetV = new Vector3(point.x, point.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        NLinear = Norm(enemyV, targetV);
        transform.position += (NLinear * speed * Time.deltaTime);


    }//Attack2()



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

    /// //////////////////////////////////////////
    /// ///////extra functions///////////////////
    /// /////////////////////////////////////////

    //normalize vectors
    private Vector3 Norm(Vector3 target, Vector3 position)
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


    //Adam - Creating a drop when enemy is destroyed based on their inventory
    void Drops()
    {
        var expDrop = Resources.Load("Prefabs/ExpPickup");
        var exp = UnityEngine.Random.Range(1, 3);
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
