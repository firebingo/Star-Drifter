//=========================================================
// Author: Allen Jones
// Date: 3-26-16
// Version: 1.0
// Description: This code runs the Occupant AI FSM
// Allen:3/27:  Added aggression and stimuli functions. removed bullet object.
// Allen:4/18:  Added Leveling system 
//==========================================================

using UnityEngine;
using System.Collections;
using System; //for math

public class Occupant : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private int shotTimer = 50;
 

    //Variables
    private Vector2 position;
    private GameObject target;  //Current target ('GameObject' should be 'object'?)
    public GameObject[] playerObj; //Find and store player info to reference Player lvl

    public float fireTimer;

    private int health;
    private int weaponHeld;
    private int aggression;

    private float velocity;
    private float rotation;
    private float targDist;     //Distance from target

    //variables for Wander 
    Vector3 wanderPoint;
    float wanderBreak = 100.0f;
    float range = 2.0f;

    //enum for FSM
    public enum AIFSM  {Wander, Attack, Shoot, Wait, Avoid}
    public AIFSM AIState = AIFSM.Wait;
    private object distance;

    public Inventory occupantInventory;
    public Guid weaponId;

    //Some test crud
    float walkingDirection = 1.0f;
    Vector3 walkAmount;

    /// <summary>
    /// Gather info such as player distance, nearest obstacle, and nerest target
    /// </summary>
    void TakeStim()
    {
        //Get target distance
        //GetClosestEnemy(target);

        //DO stuff...
    }
    
    /// <summary>
    ///  Use this for initialization.
    ///  This function not only sets all stats to 0, it will use the player's 
    ///  level to decide the stats of the enemy. This is also the ground work 
    ///  for the later task: loot generator.
    ///  
    /// Note: these settings override Unities debug settings
    /// </summary>
    void Start ()
    {
        occupantInventory = this.GetComponent<Inventory>();
        Weapon tempWeapon = new Weapon();
        tempWeapon.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 5f, 5f, shotTimer, 3f, 1f, 32, weaponTypes.Pistol, weaponLayers.Enemy);
        occupantInventory.items.Add(tempWeapon.itemId, tempWeapon);
        weaponId = tempWeapon.itemId;

        //Basics
        health = 100;
        fireTimer = 0;
        weaponHeld = 0;
        aggression = 0;

        position.x = transform.position.x;
        position.y = transform.position.y;

        velocity = 0;
        rotation = 0;

        //Test, Set state to override Unity
        AIState = AIFSM.Wander;

        //Enemy Leveling 
        playerObj = GameObject.FindGameObjectsWithTag("Player");
        //playLv = playerObj.GetComponent<PlayerController>();
    }

    //void 

    /// <summary>
    /// Update is called once per frame
    /// </summary>  
    void Update ()
    {
        switch (AIState)
        {
            case AIFSM.Wander:
                Wander();
                break;
            case AIFSM.Attack:
                Attack();
                break;
            case AIFSM.Shoot:
                Shoot();
                break;
            case AIFSM.Wait:
                break;
            case AIFSM.Avoid:
                break;

        }//end switch

	}//end update

    /// <summary>
    /// Find nearest target function to target (will need work)
    /// </summary>
    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    //State Machine Functions (per update)
    private void Wander()
    {
        //Test: Move left (will be removed)
        walkAmount.x = walkingDirection * speed * Time.deltaTime;
        walkingDirection = -1.0f;
        transform.Translate(walkAmount);

    }//Wander

    /// <summary>
    /// The attack state, will pursue or fire 
    /// </summary>
    private void Attack()
    {
        //Can Fire>?
        if (fireTimer > shotTimer)
        {
            //Change to firing state (Will move to takeStim() )
            AIState = AIFSM.Shoot;
        }
        //Increment shot timer, aka Rate of Fire
        fireTimer += 1 + Time.deltaTime;
    }//Attack

    private void Shoot()
    {
        //Reset fire timer
        fireTimer = 0;
        //Reset to attack state
        AIState = AIFSM.Attack;

        //Fire
        var tempWeapon = occupantInventory.items[weaponId] as Weapon;
        if (tempWeapon)
            tempWeapon.Fire(this.transform);
    }//Shoot, a transition of Attack

    private void Wait() { }//Flee

    private void Avoid() { }//Avoid nearby obstacle
}
