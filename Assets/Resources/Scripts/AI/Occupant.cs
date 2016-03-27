//=========================================================
// Author: Allen Jones
// Date: 3-26-16
// Version: 1.0
// Description: This code runs the Occupant AI FSM
//==========================================================

using UnityEngine;
using System.Collections;

public class Occupant : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private int shotTimer = 50;
    [SerializeField]
    private GameObject bullet;

    //Variables
    private Vector2 position;

    private float rotation;
    public float fireTimer;

    private int health;
    private int weaponHeld;

    private float velocity;


    //enum for FSM
    public enum AIFSM  {Wander, Attack, Wait}
    public AIFSM AIState = AIFSM.Wait;



    /// <summary>
    /// AI bot is awake
    /// </summary>
    void Awake() { }
    
    /// <summary>
    ///  Use this for initialization
    /// </summary>
    void Init ()
    {
        health = 100;
        fireTimer = 0;
        weaponHeld = 0;

        position.x = transform.position.x;
        position.y = transform.position.y;

        velocity = 0;
            
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update ()
    {
        
        switch (AIState)
        {
            case AIFSM.Wander:
                break;
            case AIFSM.Attack:
                break;
            case AIFSM.Wait:
                break;

        }//end switch

	}//end update
    
    //State Machine Functions
    private void Wander() { }//Patrol
    private void Attack() { }//Attack
    private void Wait() { }//Flee

}
