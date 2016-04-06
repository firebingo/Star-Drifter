using UnityEngine;
using System.Collections;

public class PirateAssassin : MonoBehaviour {

    //enum for FSM
    public enum AIFSM { Raid, Attack, Return }
    public AIFSM AIState = AIFSM.Raid;




    void Awake() { }
    // Use this for initialization
    void Start()
    {
        //Raid();
    }

    // Update is called once per frame
    void Update()
    {

        switch (AIState)
        {
            case AIFSM.Raid:
                break;
            case AIFSM.Attack:
                break;
            case AIFSM.Return:
                break;

        }//end switch

    }
    //State Machine Functions
   private void Raid() { }//Raid
   private void Attack() { }//Attack
   private void Return() { }//Return

}
