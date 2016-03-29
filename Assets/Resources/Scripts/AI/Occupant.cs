//=========================================================
// Author: Allen Jones
// Date: 3-26-16
// Version: 1.0
// Description: This code runs the Occupant AI FSM
// Allen:3/27:  Added aggression and stimuli functions. removed bullet object.
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

    //Some test crud
 
    float walkingDirection = 1.0f;
    Vector3 walkAmount;

    /// <summary>
    /// Gather info such as player distance, nearest obstacle, and nerest target
    /// </summary>
    void TakeStim()
    {
        //Get target distance
        TargDist(target);

        //DO stuff...
    }

    /// <summary>
    /// AI bot is awake
    /// </summary>
    void Awake() { /*Do stuff?*/ }
    
    /// <summary>
    ///  Use this for initialization
    /// </summary>
    void Start ()
    {
        health = 100;
        fireTimer = 0;
        weaponHeld = 0;
        aggression = 0;

        position.x = transform.position.x;
        position.y = transform.position.y;

        velocity = 0;
        rotation = 0;
            
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
    /// Distance function to target
    /// </summary>
    void TargDist(GameObject target)
    {
        //Needs work
        //distance = Math.Sqrt(((x2 - x1) * (x2 - x1)) + ((y2 - y1) * (y2 - y1)));
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
        gameObject.GetComponent<Weapon>().Fire();
    }//Shoot, a transition of Attack

    private void Wait() { }//Flee

    private void Avoid() { }//Avoid nearby obstacle

}
