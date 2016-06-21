using UnityEngine;
using System.Collections;

public class AIShip : MonoBehaviour {

    public bool boreded = false;
    public int maxSpace = 5;
    int numSpawn = 0;
    public int passengers = 0;
    private GameObject Assassin, Warrior, temp;
    float delay = 4.0f;
    int id = 0;
    float timePassed = 0.0f;
    Vector3 landingPoint = new Vector3(0.5f,-7f,1f);
    float speed = 1.0f;
   public float distance;



    //temporary Vectors
    Vector3 targetV, enemyV, NLinear;



    // Use this for initialization
    void Start () {
        this.Assassin = Resources.Load("Prefabs/Enemy_Assassin") as GameObject;
        this.Warrior = Resources.Load("Prefabs/Enemy_Warrior") as GameObject;

       // GameObject temp = Instantiate(Assassin, gameObject.transform.position, gameObject.transform.rotation) as GameObject;


    }
	
	// Update is called once per frame
	void Update () {
       
        if (!boreded) {
            MoveToDeboard();

            distance = Mathf.Abs(Vector3.Distance(transform.position, landingPoint));
            if (distance <= 1.5f) { boreded = true; }
        }
        else if (boreded)
        { SpawnEnemies(); }
       


        
    

	}


    void SpawnEnemies()
    {
        if (timePassed <= 6.0f && numSpawn < maxSpace)
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed >= delay)
        {
            id = (UnityEngine.Random.Range(1, 100)) % 3;

            switch (id)
            {
                case 1:
                    temp = Instantiate(Assassin, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                    break;
                case 2:
                    temp = Instantiate(Warrior, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                    break;
            }
            timePassed = 0;
            numSpawn += 1;
        }


    }

    void MoveToDeboard() {
        rotateForward(landingPoint);
        targetV = new Vector3(landingPoint.x, landingPoint.y, 0);
        enemyV = new Vector3(transform.position.x, transform.position.y, 0);
        //NLinear = Norm(enemyV, targetV);
        NLinear = Norm(targetV, enemyV);
        transform.position += (NLinear * speed * Time.deltaTime);

    }
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
}
