using UnityEngine;
using System.Collections;

public class PirateWarrior : MonoBehaviour {

    //enum for FSM
    public enum AIFSM { Attack, Destroy }
    public AIFSM AIState = AIFSM.Destroy;




    void Awake() { }
    // Use this for initialization
    void Start()
    {
        // Destroy();
    }

    // Update is called once per frame
    void Update()
    {

        switch (AIState)
        {
            case AIFSM.Destroy:
                break;
            case AIFSM.Attack:
                break;
        }//end switch

    }
    //State Machine Functions
    private void Destroy() { }//Destroy
    private void Attack() { }//Attack

}
