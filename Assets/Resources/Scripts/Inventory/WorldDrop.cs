using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class WorldDrop : Inventory 
{
    [SerializeField]
    bool inRange;

    void Start()
    {
        items = new Dictionary<Guid, inventoryItem>();

        //Test
        Weapon weapon1 = ScriptableObject.CreateInstance("Weapon") as Weapon;
		weapon1.Initialize(Resources.Load("Prefabs/Bullet") as GameObject, 50f, 8f, 1f, 5f, 1f, 32, weaponTypes.Pistol, weaponLayers.Player);
        Guid id = weapon1.itemId;

        items.Add(weapon1.itemId, weapon1);
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerInventory.Merge(items);
            Destroy(this.gameObject);    
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
}
