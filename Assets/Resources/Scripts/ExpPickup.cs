using UnityEngine;
using System.Collections;

public class ExpPickup : MonoBehaviour 
{
    GameObject player;

    [SerializeField]
    int expAmount = 10;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            player.GetComponent<Leveling>().currentExperience += expAmount;
            Destroy(this.gameObject);
        }
           
    }
}
