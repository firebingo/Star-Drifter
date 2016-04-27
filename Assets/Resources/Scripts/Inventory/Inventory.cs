using UnityEngine;
using System;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public Dictionary<Guid, inventoryItem> items;

    // Use this for initialization
    void Start()
    {
        items = new Dictionary<Guid, inventoryItem>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var item in items)
        {
            item.Value.updateItem();
        }
    }
}
