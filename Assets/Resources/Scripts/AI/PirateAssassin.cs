//=========================================================
// Author: Bobby Lewis
// Date: 3-19-16
// Version: 1.0
// Description: This code runs the Pirate Assassin AI
//==========================================================

using UnityEngine;
using System.Collections;

public class PirateAssassin : MonoBehaviour {

    //enum for FSM
    public enum AIFSM { Raid, Attack, Return }
    public AIFSM AIState = AIFSM.Raid;

    
    //variables for AI stats
    float speed = 1.0f;

    //variables for Raid
    GameObject[] itemObj;
   public Transform[] item;
    int itemSize;
    public Transform closestItem;
    private float disO;


    //variables for Attack
    GameObject targetObj;
    Transform target;
    private float dis;
    bool spotted = false;

    //varaibles for Return
    GameObject shipObj;
    Transform ship;
    bool loot = false;
    int index;

    //temporary Vectors
    Vector3 targetV, enemyV, NLinear;




    void Awake() { }
    // Use this for initialization
    void Start()
    {

        //get list of items
        shipObj = GameObject.Find("PirateShip");
        ship = shipObj.transform;
        itemObj = GameObject.FindGameObjectsWithTag("Item");
        itemSize = itemObj.Length;
        item = new Transform[itemSize];
        for (int i = 0; i < itemSize; i++)
        {
            item[i] = itemObj[i].transform;

        }//end for
        //get player/target info
        targetObj = GameObject.FindGameObjectWithTag("Player");
        target = targetObj.transform;

    }//start()

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Magnitude(transform.position - target.position);//magnitude/distane of enemy and target

        if (AIState != AIFSM.Return) {State(target, dis);}//check whether to attack or raid as long as the enemy is not already returning an item;

        switch (AIState)
        {
            case AIFSM.Raid:
                if (dis < 2.5f)
                { spotted = true; }
                closestItem = FindClosestItem(item,itemSize);
                Raid(closestItem);
                break;
            case AIFSM.Attack:
                Attack();
                break;
            case AIFSM.Return:
                Return();
                break;

        }//end switch

    }//Update




    //State Machine Functions 
    //determines which state the enemy should be in
    private void State(Transform target, float distance)
    {

        if (0.8f <= distance && distance <= 2.8f && spotted == true)
        {
            AIState = AIFSM.Attack;
        }//end if
        else if(loot)
        {
            AIState = AIFSM.Return;
        }
        else {

            AIState = AIFSM.Raid;
        }
        

    }//State()

  
    private void Raid(Transform CT) {

        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        targetV = new Vector3(CT.position.x, CT.position.y, 0);
        NLinear = Norm(targetV, enemyV);
        transform.position += (NLinear * speed * Time.deltaTime);
        rotateForward(CT.position);

        dis = Vector3.Magnitude(transform.position - CT.position);
       if (dis < 0.5)
        {
            loot = true;
            DestroyObject(itemObj[index]); 
        }
    }//Raid

   private void Attack() {
        targetV = new Vector3(target.position.x, target.position.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        NLinear = Norm(targetV, enemyV);
        transform.position += (NLinear * speed * Time.deltaTime);
        rotateForward(target.position);

    }//Attack
   private void Return() {
        targetV = new Vector3(ship.position.x, ship.position.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        NLinear = Norm(targetV, enemyV);
        transform.position += (NLinear * speed * Time.deltaTime);
        rotateForward(ship.position);



    }//Return


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

    private Transform FindClosestItem(Transform[] item, int size)
    {
        float sDis =Mathf.Infinity;// variable for smallest distance
        Transform near = transform;
      //  Vector3 next;

        for (int i = 0; i < size; i++)
        {
            float dis = Vector3.Magnitude(transform.position - item[i].position);//magnitude/distane of enemy and target
            if (dis < sDis)
            {

                near = item[i];
                sDis = dis;
                index = i;
            }
        }

        return near;
    }//FindClosestItem

}
