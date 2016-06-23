using UnityEngine;
using System.Collections;

public class TileDestruction : MonoBehaviour {

    [SerializeField]
    float maxHealth = 100f;
    float currentHealth;
    [SerializeField]
    int armor = 0;

    Renderer rend;
    public Sprite newSprite;
    private bool destroyed;


    //Removed for performance
    //bool StationUnderAttack = false;

    // Use this for initialization
    void Start () 
    {
        
        currentHealth = maxHealth;
        rend = this.GetComponent<SpriteRenderer>() as SpriteRenderer;

    }
	
	// Update is called once per frame
	void Update () 
    {
        //Removed for performance
        /* if (currentHealth < maxHealth)
         {
             StationUnderAttack = true;
         }*/

        if (currentHealth <= 0 && !destroyed)
        {
            int count = Random.Range(1, 3);
            for (int i = 0; i < count; i++)
            {
                Vector3 positionOffset = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                Instantiate(Resources.Load("Prefabs/Debris") as GameObject, transform.position + positionOffset, transform.rotation);
            }
            //Destroy(this.gameObject);
            destroyed = true;
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
	}

    void ApplyDamage(float damage)
    {
        float damageDealt = damage - armor;
        if (damageDealt <= 0)
            damageDealt = 1;
        currentHealth -= damageDealt;
    }
    //Removed for performance
    /*void OnGUI()
    {
        //Removed for performance
       /* if (StationUnderAttack)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), "Your Base is Being Attacked");
        }
}*/
}
