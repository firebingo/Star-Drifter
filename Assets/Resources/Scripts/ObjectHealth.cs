using UnityEngine;
using System.Collections;

public class ObjectHealth : MonoBehaviour 
{
   public  float maxHealth = 500.0f;
   public float currentHealth;

	// Use this for initialization
	void Start () 
    {
	    currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);

        } 


	}

    void ApplyDamage(float damage)
    {
        currentHealth -= damage;
    }

}

