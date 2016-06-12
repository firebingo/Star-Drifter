using UnityEngine;
using System.Collections;

public class TileDestruction : MonoBehaviour {

    [SerializeField]
    float maxHealth = 100f;
    float currentHealth;
    [SerializeField]
    int armor = 0;

    bool StationUnderAttack = false;
	// Use this for initialization
	void Start () 
    {
        currentHealth = maxHealth;
      
	}

    // Update is called once per frame
    void Update()
    {   if (currentHealth < maxHealth)
        {
            StationUnderAttack = true;
        }

        if (currentHealth <= 0) {
            //GameObject.Find("GridManager").SendMessage("CalculateObstacles");
        Destroy(this.gameObject);
    }
	}

    void ApplyDamage(float damage)
    {
        float damageDealt = damage - armor;
        if (damageDealt <= 0)
            damageDealt = 1;
        currentHealth -= damageDealt;
    }
    void OnGUI()
    {
        if (StationUnderAttack)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Your Base is Being Attacked");
        }
    }
}
