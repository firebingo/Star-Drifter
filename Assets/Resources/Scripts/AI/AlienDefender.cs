using UnityEngine;
using System.Collections;

public class AlienDefender : MonoBehaviour {

    //Enums for the FSM
    public  enum AIFSM { Wander,Attack, Defend};
    public AIFSM AIState = AIFSM.Wander;

    //variables for AI transform
    public Transform transform;

    //AI stat variables
    float speed = 0.5f;

    //max variables for AI
    float maxRotation;
    float maxSpeed;

    //variables for Wander 
    Vector3 wanderPoint;
    float wanderBreak = 100.0f;
    float range = 2.0f;

    //variables for Attack

    //variables for Defend

    void Awake() {

        transform = gameObject.transform;
    }

	// Use this for initialization
	void Start () {
        Wander();//start off in wander state
	}
	
	// Update is called once per frame
	void Update () {

        switch(AIState)//FSM 
        {
            case AIFSM.Wander:
                transform.position += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
                if ((transform.position - wanderPoint).magnitude < 2.0f)// if the distance is less than 2.0f
                    {
                    float wbreak = 0.0f;//variable for break between wanders
                    while (wbreak <= wanderBreak)//meant to pause the current wander before getting new point currently not working.
                    { wbreak += Time.deltaTime; }//end while
                    Wander(); 
                    }//endif
                    break;

            case AIFSM.Attack:
                //Code for Attack state
                break;

            case AIFSM.Defend:
                //Code for Defend state
                break;

        }

           
    }

    private void Wander(){
        wanderPoint.z = 1.0f;
        wanderPoint = new  Vector3(Random.Range(transform.position.x - range, transform.position.x + range),  Random.Range(transform.position.y - range, transform.position.y + range),1.0f);
        wanderPoint.z = 1.0f;
        transform.LookAt(wanderPoint);
      
    }//Wander()
    private void Attack() { }//Attack
    private void Defend() { }//Defend
}
