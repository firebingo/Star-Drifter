using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

public class HUDCategories : MonoBehaviour
{
    private List<string> categoryNames;
    private List<UICategory> categories;
    private int currentCategories;
    private UIInventory parentInventory;

    // Use this for initialization
    void Start()
    {
        parentInventory = transform.parent.GetComponent<UIInventory>();
        categoryNames = Enum.GetNames(typeof(itemType)).ToList();
        categories = new List<UICategory>();
        categoryNames = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        foreach (var item in categories)
        {
            Destroy(item.gameObject.transform.parent);
        }
        categories.Clear();
        categoryNames.Clear();
        generateCategories();
    }

    void generateCategories()
    {
        foreach(var name in Enum.GetNames(typeof(itemType))) 
        {
            var toAdd = Resources.Load("Prefabs/UI/InventoryUICategory") as GameObject;
            if (toAdd)
            {
                var created = Instantiate(toAdd);
                created.transform.parent = this.transform;
                if(created)
                {
                    var Text = created.transform.GetChild(0).GetComponent<Text>();
                    Text.text = name;
                    var categoryScript = created.transform.GetChild(0).GetComponent<UICategory>();
                    categories.Add(categoryScript);
                    categoryNames.Add(name);
                    categoryScript.categoryType = (itemType)Enum.Parse(typeof(itemType), name);
                }
            }
        }

        foreach(var cat in categories)
        {
            if(cat.categoryType != parentInventory.currentCategory)
                cat.gameObject.SetActive(false);
        }
    }
}
