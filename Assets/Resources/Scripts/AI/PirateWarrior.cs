using UnityEngine;
using System.Collections;
using System;

public class PirateWarrior : MonoBehaviour {

    [SerializeField]
    float range = 2; //Used for drop offset

    //enum for FSM
    public enum AIFSM { Raid, Attack, Destroy }
    public AIFSM AIState = AIFSM.Raid;
    private Transform target;
    public GameObject targetObj;
    public float distance;

    Vector3 targetV, enemyV, NLinear;//new vectors for attacking
    public float fireTimer = 0;//cooldown timer for shooting
    private float shotTimer = 1.3f;//cooldown time required for shooting

    //AI stat variables
    float speed = 0.7f;// movement speed variable
    float damage = 0.4f;

    //rigid body
    private Rigidbody2D rb;

    //Weapon Invetory Variables 
    public Inventory alienInventory;
    public Guid weaponId;

    

    void Awake() {


        targetObj = GameObject.FindGameObjectWithTag("StationWayPoint");

        target = targetObj.transform;//transform of target

    }
    // Use this for initialization
    void Start()
    {

        alienInventory = this.GetComponent<Inventory>();
        Weapon tempWeapon = ScriptableObject.CreateInstance("Weapon") as Weapon;
        tempWeapon.Initialize(weaponLayers.Enemy);
        alienInventory.items.Add(tempWeapon.itemId, tempWeapon);
        weaponId = tempWeapon.itemId;

        rb = GetComponent<Rigidbody2D>();
        // Destroy();
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(transform.position, target.position);//magnitude/distane of enemy and target


        State(distance);
        switch (AIState)
        {
            case AIFSM.Raid:
                Raid(target);
                break;
            case AIFSM.Destroy:
                break;
            case AIFSM.Attack:
                break;
        }//end switch

    }
    //State Machine Functions
    private void Destroy() { }//Destroy
    private void Attack() { }//Attack
    private void State(float d)
    {
        if (d <= 0.1f)
        { AIState = AIFSM.Attack; }

    }

    private void Raid(Transform target)
    {
        targetV = new Vector3(target.position.x, target.position.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        NLinear = Norm(enemyV, targetV);
        transform.position += (NLinear * speed * Time.deltaTime);
        rotateForward(target.position);
        if (fireTimer > shotTimer)
        {
            var tempWeapon = alienInventory.items[weaponId] as Weapon;
            if (tempWeapon)
            {
                StartCoroutine(tempWeapon.Fire(this.transform));
                fireTimer = 0.0f;
            }
        }
    }//Attack()

    void OnCollisionEnter2D(Collision2D col)
    {

        print("collision with" + col.gameObject.name);

    }
    void OnCollisionStay2D(Collision2D col)
    {

        print("collision with" + gameObject.name);
        col.gameObject.SendMessage("ApplyDamage", damage);

       
    }
   



    /// <summary>
    /// ///////////////////////////////////
    /// ///////////////EXTRA FUNCTIONS////////////////
    /// ///////////////////////////////////////

    //normalize vectors
    private Vector3 Norm(Vector3 position, Vector3 target)
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

    void Drops()
    {
        var expDrop = Resources.Load("Prefabs/ExpPickup");
        var exp = UnityEngine.Random.Range(1, 3);
        for (int i = 0; i < exp; i++)
        {
            Vector3 offset = new Vector3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(-2, 2), 0);
            Instantiate(expDrop, transform.position + offset, transform.rotation);
        }

        var worldDrop = Resources.Load("Prefabs/WorldPickup");
        var temp = (GameObject)Instantiate(worldDrop, transform.position, transform.rotation);
        temp.GetComponent<WorldDrop>().Merge(alienInventory.items);

    }

  

}
