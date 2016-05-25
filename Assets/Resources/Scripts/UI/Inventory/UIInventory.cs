﻿using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System;

public class UIInventory : MonoBehaviour
{
    public itemType currentCategory; //the current inventory category that is selected.
    public itemType lastCategory; //the last category selected.

    [SerializeField]
    public int currentItemIndex; //the current item at the top of the inventory scrolling.

    [SerializeField]
    private HUDManager hudManager;

    private List<UIItem> uiItems;

    private GameObject itemDetails;

    private bool isInit = false;

    //Delegate for item details.
    //This is used so this event can be called for each itemtype and it will call the proper
    // function and generate the details for the item type.
    delegate void generateCurrentItemDetails();
    static event generateCurrentItemDetails generateDetails;

    // Use this for initialization
    void Start()
    {
        Intilize();
    }

    void Intilize()
    {
        if(!isInit)
        {
            currentCategory = 0;
            lastCategory = itemType.Null;

            uiItems = new List<UIItem>();
            isInit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hudManager)
        {
            Debug.Log("Inventory Missing HUD Manager Reference");
            return;
        }
        if (currentCategory != lastCategory)
        {
            currentItemIndex = 0;

            cleanItems();
            generateUIItems(currentCategory);

            registerDelegate(currentCategory);
            if(generateDetails != null)
                generateDetails();

            lastCategory = currentCategory;

        }
    }

    void cleanItems()
    {
        if (uiItems != null)
        {
            foreach (var item in uiItems)
            {
                Destroy(item.gameObject);
            }
            uiItems.Clear();
        }

        if (itemDetails)
        {
            Destroy(itemDetails.gameObject);
            itemDetails = null;
        }
    }

    void registerDelegate(itemType category)
    {
        if (generateDetails != null)
        {
            foreach (var d in generateDetails.GetInvocationList())
                generateDetails -= (d as generateCurrentItemDetails);
        }

        switch (category)
        {
            case itemType.Weapon:
                generateDetails += generateCurrentWeaponDetails;
                break;
            case itemType.Ammo:
                //TODO: Implement
                break;
            case itemType.Scrap:
                //TODO: Implement
                break;
            default:
                break;
        }
    }

    void OnEnable()
    {
        Intilize();
        cleanItems();
        lastCategory = itemType.Null;
        generateUIItems(currentCategory);
    }

    void openInventory()
    {

    }

    void generateUIItems(itemType type)
    {
        var items = hudManager.player.playerInventory.items.Values.Where(i => i.inventoryItemType == currentCategory).ToList();

        int index = 0;
        foreach (var item in items)
        {
            var toAdd = Resources.Load("Prefabs/UI/InventoryUIItem") as GameObject;
            if (toAdd)
            {
                var created = Instantiate(toAdd);
                if (created)
                {
                    created.transform.SetParent(this.transform);
                    created.transform.localPosition = new Vector3(-133, 198 - (index * 85), 0);
                    var itemScript = created.GetComponent<UIItem>();
                    uiItems.Add(itemScript);
                    itemScript.Initilize("Textures/Items/" + item.itemName, item.itemName, item.count, item.itemId);
                    itemScript.index = index;
                    if (index > currentItemIndex + 5)
                        created.SetActive(false);
                }
            }
            ++index;
        }
    }

    /// <summary>
    /// Updates the current scroll index.
    /// true is up (-), false is down (+)
    /// </summary>
    /// <param name="direction"></param>
    public void changeScrollIndex(bool direction)
    {
        if (direction)
        {
            if (currentItemIndex > 0)
            {
                --currentItemIndex;
                foreach (var item in uiItems)
                {
                    item.gameObject.SetActive(false);
                    if (item.index < currentItemIndex + 5)
                        item.gameObject.SetActive(true);
                }
                if (generateDetails != null)
                    generateDetails();
            }
        }
        else
        {
            if (currentItemIndex < uiItems.Count-1)
            {
                ++currentItemIndex;
                foreach (var item in uiItems)
                {
                    item.gameObject.SetActive(false);
                    if (item.index < currentItemIndex + 5)
                        item.gameObject.SetActive(true);
                }
                if (generateDetails != null)
                    generateDetails();
            }
        }
    }

    #region Item Details Generation
    void generateCurrentWeaponDetails()
    {
        if (itemDetails)
        {
            Destroy(itemDetails.gameObject);
            itemDetails = null;
        }

        var item = hudManager.player.playerInventory.items[uiItems[currentItemIndex].itemId] as Weapon;

        if (item)
        {
            var toAdd = Resources.Load("Prefabs/UI/InventoryWeaponDetails") as GameObject;
            if (toAdd)
            {
                var created = Instantiate(toAdd);
                if (created)
                {
                    created.transform.SetParent(this.transform);
                    created.transform.localPosition = new Vector3(145, -13, 0);
                    itemDetails = created;
                    var detailScript = created.GetComponent<WeaponDetails>();
                    if (detailScript)
                    {
                        detailScript.Initilize(item.itemName, Enum.GetName(typeof(weaponTypes), item.Type), Enum.GetName(typeof(bulletTypes), item.bulletType),
                            item.bulletSpeed.ToString(), item.Damage.ToString(), item.shotTimer.ToString());
                    }
                }
            }
        }
    }
    #endregion
}