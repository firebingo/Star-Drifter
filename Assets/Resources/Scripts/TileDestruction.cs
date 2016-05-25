using UnityEngine;
using System.Collections;

public class TileDestruction : MonoBehaviour {

    [SerializeField]
    float maxHealth = 100f;
    float currentHealth;
    [SerializeField]
    int armor = 0;

	// Use this for initialization
	void Start () 
    {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (currentHealth <= 0)
            Destroy(this.gameObject);
	}

    void ApplyDamage(float damage)
    {
        float damageDealt = damage - armor;
        if (damageDealt <= 0)
            damageDealt = 1;
        currentHealth -= damageDealt;
    }
}
