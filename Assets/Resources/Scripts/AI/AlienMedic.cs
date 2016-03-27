using UnityEngine;
using System.Collections;

public class AlienMedic : MonoBehaviour {

    //enum for FSM
    public enum AIFSM { Seek, Heal, Flee }
    public AIFSM AIState = AIFSM.Seek;




    void Awake() { }
    // Use this for initialization
    void Start()
    {
        // Seek();
    }

    // Update is called once per frame
    void Update()
    {

        switch (AIState)
        {
            case AIFSM.Seek:
                break;
            case AIFSM.Heal:
                break;
            case AIFSM.Flee:
                break;

        }//end switch

    }
    //State Machine Functions
    private void Seek() { }//Seek
    private void Heal() { }//Heal
    private void Flee() { }//Flee

}
