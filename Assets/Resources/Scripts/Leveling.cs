using UnityEngine;
using System.Collections;

public class Leveling : MonoBehaviour 
{
    public struct Stats
    {
        public int power;
        public float speed;
        public int life;
        public int armor;
    }

    public Stats stats;
    [SerializeField]
    int statPoints = 0;

    [SerializeField]
    public int level = 1;
    [SerializeField]
    public float currentExperience { get; private set; }
    [SerializeField]
    public float toNextLevel { get; private set; }
    [SerializeField]
    public float lastLevel { get; private set; }
    float expScaling;
    [SerializeField]
    float expYield = 10;

    GameObject target;

    void Start()
    {
        level = 1;
        currentExperience = 0f;
        toNextLevel = 100f;
        lastLevel = 0f;
        expScaling = 2.1f; //Sets up the exp scaling per lvl;

        stats.power = 0;
        stats.speed = 3;
        stats.life = 100;
        stats.armor = 0;

        SendMessage("setStats", stats);
    }

    void Update()
    {
        if (currentExperience >= toNextLevel)
        {
            if (tag == "Player")
            {
                level++;
                lastLevel = toNextLevel;
                toNextLevel = lastLevel * expScaling;
                statPoints += 5;
            }
        }

        //Test/placeholder for stat points till a menu interface is created
        if (Input.GetKeyDown(KeyCode.Alpha1) && statPoints >= 1)
        {
            stats.power += 1;
            statPoints -= 1;
            SendMessage("setStats", stats);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && statPoints >= 1)
        {
            stats.speed += 0.25f;
            statPoints -= 1;
            SendMessage("setStats", stats);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && statPoints >= 1)
        {
            stats.life += 10;
            statPoints -= 1;
            SendMessage("setStats", stats);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && statPoints >= 1)
        {
            stats.armor += 1;
            statPoints -= 1;
            SendMessage("setStats", stats);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            currentExperience += 10;
        }
    }

    void OnDestroy()
    {
        if (tag != "Player")
        {
            target = GameObject.Find("Player");
            target.GetComponent<Leveling>().currentExperience += expYield;
        }
    }

}
