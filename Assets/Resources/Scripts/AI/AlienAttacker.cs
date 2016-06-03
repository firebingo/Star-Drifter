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
    public enum AIFSM { Patrol, Attack, Flee }
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
    GameObject targetObj;
    private Transform target;
    private float targetDis;
    private bool spotted = false;

    // Use this for initialization
    void Start()
    {
        alienInventory = this.GetComponent<Inventory>();
        Weapon tempWeapon = new Weapon();
        tempWeapon.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 5f, 5f, shotTimer, 3f, 1f, 32, weaponTypes.Pistol, weaponLayers.Enemy);
        alienInventory.items.Add(tempWeapon.itemId, tempWeapon);
        weaponId = tempWeapon.itemId;

        targetObj = GameObject.FindGameObjectWithTag("Player");//target the player
        target = targetObj.transform;//target transform
        Patrol();
    }

    // Update is called once per frame
    void Update()
    {
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
