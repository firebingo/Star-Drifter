using UnityEngine;
using System.Collections;

public class AIShip : MonoBehaviour {

    public bool boreded = false;
    public int maxSpace = 5;
    int numSpawn = 0;
    public int passengers = 0;
    private GameObject Assassin, Warrior, temp;
    float delay = 5.0f;
    int id = 0;
    float timePassed = 0.0f;

	// Use this for initialization
	void Start () {
        this.Assassin = Resources.Load("Prefabs/Enemy_Assassin") as GameObject;
        this.Warrior = Resources.Load("Prefabs/Enemy_Warrior") as GameObject;

       // GameObject temp = Instantiate(Assassin, gameObject.transform.position, gameObject.transform.rotation) as GameObject;


    }
	
	// Update is called once per frame
	void Update () {


        if (boreded)
        { SpawnEnemies(); }
        else
        { }


        
    

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


    }

}
