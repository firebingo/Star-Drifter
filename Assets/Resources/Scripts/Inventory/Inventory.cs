using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

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

    public void Merge(Dictionary<Guid, inventoryItem> merger) //Not tested
    {
        var result = items.Concat(merger).GroupBy(item => item.Key).ToDictionary(item => item.Key, item => item.First().Value);
        items = result;
    }
}
