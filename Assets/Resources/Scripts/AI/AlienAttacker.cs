using UnityEngine;
using System.Collections;

public class AlienAttacker : MonoBehaviour {
    //enum for FSM
    public enum AIFSM  {Patrol, Attack, Flee}
    public AIFSM AIState = AIFSM.Patrol;




    void Awake() { }
	// Use this for initialization
	void Start () {
       // Patrol();
	}
	
	// Update is called once per frame
	void Update () {

        switch (AIState) {
            case AIFSM.Patrol:
                break;
            case AIFSM.Attack:
                break;
            case AIFSM.Flee:
                break;

        }//end switch

	}
    //State Machine Functions
    private void Patrol() { }//Patrol
    private void Attack() { }//Attack
    private void Flee() { }//Flee

}
