using UnityEngine;
using System.Collections;

public class AIHealth : MonoBehaviour {

   public  float maxHealth = 100.0f;
    public float currentHealth = 100.0f;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (currentHealth <= 0)
        {
            SendMessage("Drops");
            Destroy(this.gameObject);

        } 


	}


    void ApplyDamage(float damage)
    {
        currentHealth -= damage;
    }

}
