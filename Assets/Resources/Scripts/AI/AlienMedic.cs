using UnityEngine;
using System.Collections;

public class AlienMedic : MonoBehaviour {

    //enum for FSM
    public enum AIFSM { Seek, Heal, Flee }
    public AIFSM AIState = AIFSM.Seek;


    //variables for target;
   public  GameObject[] AlliesObj;
    public Transform[] Allies;
    public Transform allyTrans;
   


    //variabls for health
    public float[] health;
    public AIHealth[] Ahealth;
   public int ID = 0;
    int allieSize = 0;

    //variables for AI stats
    private float speed = 1.2f;
    public float dis;
    


    //variables for Flee
    private float fleeSpeed = 0.5f;

    //AI attack
    Vector3 targetV, enemyV, NLinear;

    void Awake() { }
    // Use this for initialization
    void Start()
    {
        AlliesObj = GameObject.FindGameObjectsWithTag("Respawn");
        allieSize = AlliesObj.Length;
        Allies = new Transform[allieSize];
        Ahealth = new AIHealth[allieSize];
        health = new float[allieSize];
       

        for (int i = 0; i < allieSize; i++)
        {   
            Allies[i] = AlliesObj[i].transform;
            Ahealth[i] = AlliesObj[i].GetComponent<AIHealth>();

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        AlliesObj = GameObject.FindGameObjectsWithTag("Respawn");
        allieSize = AlliesObj.Length;
        Allies = new Transform[allieSize];
        Ahealth = new AIHealth[allieSize];
        health = new float[allieSize];

        for (int i = 0; i < allieSize; i++)
        {
            Allies[i] = AlliesObj[i].transform;
            Ahealth[i] = AlliesObj[i].GetComponent<AIHealth>();
            health[i] = Ahealth[i].currentHealth;
        }

        //FSM
        switch (AIState)
        {
            case AIFSM.Seek:
                allyTrans = FindHurtAlly(Allies,allieSize,ref ID);
                
                dis = Vector3.Magnitude((transform.position - allyTrans.position));
                if (dis > 0.5f) { Seek(allyTrans); }
                //if (dis < 1.0f){ Heal(ID); }
                if (dis < 1.0f){ AIState = AIFSM.Heal; }
                break;
            case AIFSM.Heal:
                allyTrans = FindHurtAlly(Allies, allieSize, ref ID);
                dis = Vector3.Magnitude((transform.position - allyTrans.position));
                if (dis < 1.0f) { Heal(ID); }
                if (dis >= 1.0f ) { AIState = AIFSM.Seek; }
                break;
            case AIFSM.Flee:
                break;

        }//end switch

    }





    //State Machine Functions
    private void Seek(Transform AT) {
        targetV = new Vector3(AT.position.x, AT.position.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        NLinear = Norm(targetV, enemyV);
        transform.position += (NLinear * speed * Time.deltaTime);
        rotateForward(AT.position);

    }//Seek



    private void Heal(int index) {

        if (AlliesObj[index].GetComponent<AIHealth>().currentHealth < AlliesObj[index].GetComponent<AIHealth>().maxHealth)
        {
            AlliesObj[index].GetComponent<AIHealth>().currentHealth += 0.5f;
        }
       

    }//Heal
    private void Flee() { }//Flee


    //support functions
    private Vector3 Norm(Vector3 target, Vector3 position)
    {
        Vector3 linear = target - position;
        linear = linear.normalized;
        return linear;
    }//Norm()

    private void rotateForward(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        if (dir != Vector3.zero)
        {
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }//rotateforward

    private Transform FindHurtAlly(Transform[] item, int size,ref int num)
    {
        float sHealth = Mathf.Infinity;// variable for smallest distance
        float cHealth = health[0];
        Transform lowest = transform;
        int index = 1;

        for (int i = 0; i < size; i++)
        {
            cHealth = health[i];
            if (cHealth < sHealth)
            {
                sHealth = cHealth;
                index = i;
            }
        }
        num = index;
        lowest = Allies[index];
        return lowest;
    }//FindHurtAlly
}
