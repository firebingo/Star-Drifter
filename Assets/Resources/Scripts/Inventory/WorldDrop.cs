using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldDrop : Inventory 
{
    [SerializeField]
    bool inRange;

    public Inventory loot;

    //Inventory playerInventory; 

    void Start()
    {
        //Test
        Weapon weapon1 = new Weapon();
        weapon1.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 50f, 8f, 1f, 5f, weaponTypes.Pistol, weaponLayers.Player);
        Guid id = weapon1.itemId;

        loot.items.Add(weapon1.itemId, weapon1);
        //Test
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerInventory.Merge(loot.items);
            Destroy(this.gameObject);    
        }

        foreach (var item in items)
        {
            item.Value.updateItem();
        }

        if (loot.items.Count <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
