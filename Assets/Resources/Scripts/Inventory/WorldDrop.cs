using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldDrop : Inventory 
{
    [SerializeField]
    bool inRange;
    public bool enemyHas;

    void Start()
    {
        items = new Dictionary<Guid, inventoryItem>();

        //Test
        Weapon weapon1 = ScriptableObject.CreateInstance("Weapon") as Weapon;
		weapon1.Initialize(weaponLayers.Player);
        Guid id = weapon1.itemId;

        items.Add(weapon1.itemId, weapon1);
        //Test
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            inRange = true;
        }
        else
        {
            enemyHas = true;
        }

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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerInventory.Merge(items);
            Destroy(this.gameObject);    
        }
        if (enemyHas == true)
        {



        }

        foreach (var item in items)
        {
            item.Value.updateItem();
        }

        if (items.Count <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Merge(Dictionary<Guid, inventoryItem> merger) //Not tested
    {
        foreach (KeyValuePair<Guid, inventoryItem> item in merger)
        {
            items.Add(item.Key, item.Value);
        }
    }
}
