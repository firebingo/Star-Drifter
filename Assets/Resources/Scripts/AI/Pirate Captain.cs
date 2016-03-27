using UnityEngine;
using System.Collections;

public class PirateCaptain : MonoBehaviour {

    //enum for FSM
    public enum AIFSM { Attack, Morale }
    public AIFSM AIState = AIFSM.Morale;




    void Awake() { }
    // Use this for initialization
    void Start()
    {
        // Morale();
    }

    // Update is called once per frame
    void Update()
    {

        switch (AIState)
        {
            case AIFSM.Morale:
                break;
            case AIFSM.Attack:
                break;

        }//end switch

    }
    //State Machine Functions
    private void Morale() { }//Morale
    private void Attack() { }//Attack

}
