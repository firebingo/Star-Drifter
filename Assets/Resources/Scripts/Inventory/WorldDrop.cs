using UnityEngine;
using System.Collections;

public class WorldDrop : Inventory 
{
    [SerializeField]
    bool inRange;

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
            foreach (var item in items)
            {
                //Display items to player to transfer
            }
        }

        foreach (var item in items)
        {
            item.Value.updateItem();
        }

        //if (items.Count <= 0)    //Will be added once items are placed into the drop either manually for resources, or populated by defeating an enemy.
            //Destroy(this.gameObject);
    }

}
