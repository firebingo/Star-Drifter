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
        foreach (KeyValuePair<Guid, inventoryItem> item in merger)
        {
            items.Add(item.Key, item.Value);
        }
        //items = items.Concat(merger).ToDictionary(item => item.Key, item => item.Value);
        //merger.ToList().ForEach(item => items.Add(item.Key, item.Value));
    }
}
