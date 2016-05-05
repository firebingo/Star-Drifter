using UnityEngine;
using System.Collections;

public class WorldDrop : Inventory 
{
    [SerializeField]
    bool inRange;

    Inventory playerInventory;

    void Start()
    {
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
            inRange = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
            inRange = false;
    }

    void Update()
    {
        if (inRange == true)
        {
            //Call Merge on Player/Enemies inventory script.
            playerInventory.Merge(items); //Not tested
            Destroy(this.gameObject);
            
        }

        foreach (var item in items)
        {
            item.Value.updateItem();
        }
    }

}
