using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public Dictionary<Guid, inventoryItem> items;

    Guid metal;
    Guid grenade;
    Guid scrap;

    // Use this for initialization
    void Start()
    {
        items = new Dictionary<Guid, inventoryItem>();

        metal = new Guid("00000000000000000000000000000000");
        grenade = new Guid("00000000000000000000000000000001");
        scrap = new Guid("00000000000000000000000000000002");
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var item in items)
        {
            item.Value.updateItem();
        }
    }

    public void Merge(Dictionary<Guid, inventoryItem> merger) //Not tested
    {
        foreach (KeyValuePair<Guid, inventoryItem> item in merger)
        {
            try
            {
                items.Add(item.Key, item.Value);
            }
            catch
            {
                //Add to objects count
            }
        }
    }
}
