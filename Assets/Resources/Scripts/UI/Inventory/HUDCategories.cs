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
    private itemType currentCategories;
    private UIInventory parentInventory;

    private bool isInit = false;

    // Use this for initialization
    void Start()
    {
        Intilize();
    }

    void Intilize()
    {
        if (!isInit)
        {
            parentInventory = transform.parent.GetComponent<UIInventory>();
            categoryNames = Enum.GetNames(typeof(itemType)).ToList();
            categories = new List<UICategory>();
            categoryNames = new List<string>();
            isInit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentCategories = parentInventory.currentCategory;
    }

    void OnEnable()
    {
        Intilize();
        foreach (var item in categories)
        {
            Destroy(item.gameObject.transform.parent.gameObject);
        }
        categories.Clear();
        categoryNames.Clear();
        generateCategories();
    }

    void generateCategories()
    {
        foreach (var name in Enum.GetNames(typeof(itemType)))
        {
            if (name != "Null")
            {
                var toAdd = Resources.Load("Prefabs/UI/InventoryUICategory") as GameObject;
                if (toAdd)
                {
                    var created = Instantiate(toAdd);
                    if (created)
                    {
                        created.transform.SetParent(this.transform);
                        created.transform.localPosition = new Vector3(7.5f, -13, 0);
                        var Text = created.transform.GetChild(0).GetComponent<Text>();
                        Text.text = name;
                        var categoryScript = created.transform.GetChild(0).GetComponent<UICategory>();
                        categories.Add(categoryScript);
                        categoryNames.Add(name);
                        categoryScript.categoryType = (itemType)Enum.Parse(typeof(itemType), name);
                        if (categoryScript.categoryType != parentInventory.currentCategory)
                            created.SetActive(false);
                    }
                }
            }
        }

        //foreach(var cat in categories)
        //{
        //    if(cat.categoryType != parentInventory.currentCategory)
        //        cat.gameObject.SetActive(false);
        //}
    }

    /// <summary>
    /// Changes the ui category.
    /// true is right(+), false is left(-).
    /// </summary>
    /// <param name="direction"></param>
    public void changeCategory(bool direction)
    {
        if (direction)
        {
            if (parentInventory.currentCategory < Enum.GetValues(typeof(itemType)).Cast<itemType>().Max())
            {
                categories[(int)currentCategories].gameObject.transform.parent.gameObject.SetActive(false);
                parentInventory.currentCategory = ++parentInventory.currentCategory;
                currentCategories = parentInventory.currentCategory;
                categories[(int)currentCategories].gameObject.transform.parent.gameObject.SetActive(true);
            }
        }
        else
        {
            if (parentInventory.currentCategory > 0)
            {
                categories[(int)currentCategories].gameObject.transform.parent.gameObject.SetActive(false);
                parentInventory.currentCategory = --parentInventory.currentCategory;
                currentCategories = parentInventory.currentCategory;
                categories[(int)currentCategories].gameObject.transform.parent.gameObject.SetActive(true);
            }
        }
    }
}
