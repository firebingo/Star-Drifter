using UnityEngine;
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
    public HUDManager hudManager;

    private List<UIItem> uiItems;

    private GameObject itemDetails;

    private bool isInit = false;

    private Color selectedColor = new Color(0.5f, 1.0f, 1.0f, 0.639f);
    private Color otherColor = new Color(1.0f, 1.0f, 1.0f, 0.639f);

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
            buildUI();
        }
    }

    public void buildUI()
    {
        currentItemIndex = 0;

        generateUIItems(currentCategory);

        registerDelegate(currentCategory);
        if (generateDetails != null)
            generateDetails();

        lastCategory = currentCategory;
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
        lastCategory = itemType.Null;
        generateUIItems(currentCategory);
    }

    void openInventory()
    {

    }

    void generateUIItems(itemType type)
    {
        cleanItems();
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
                    var isPrimary = false; 
                    var isSecondary = false; 
                    string itemRarity = "";
                    var itemWeapon = item as Weapon;
                    if(itemWeapon)
                    {
                        isPrimary = item.itemId == hudManager.player.primaryWeapon ? true : false;
                        isSecondary = item.itemId == hudManager.player.secondaryWeapon ? true : false;
                        itemRarity = Enum.GetName(typeof(weaponRarity), itemWeapon.Rarity);
                    }
                    itemScript.Initilize("Textures/Items/" + item.itemName, item.itemName, item.count, isPrimary, isSecondary, item.itemId, itemRarity);
                    itemScript.index = index;
                    if (index > currentItemIndex + 5)
                        created.SetActive(false);
                    if (index == currentItemIndex)
                    {
                        var createdImage = created.GetComponent<Image>();
                        if (createdImage)
                            createdImage.color = selectedColor;
                    }
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
                    //set the objects to disabled and enable the ones that now should be displayed.
                    item.gameObject.SetActive(false);
                    if (item.index < currentItemIndex + 5 && !(item.index < currentItemIndex))
                        item.gameObject.SetActive(true);

                    //move the item in the list
                    var itemTransform = item.gameObject.transform;
                    itemTransform.localPosition = new Vector3(itemTransform.localPosition.x, itemTransform.localPosition.y - 85, 0);

                    //set the color of the now selected item.
                    if (item.index == currentItemIndex)
                    {
                        var itemImage = item.gameObject.GetComponent<Image>();
                        if (itemImage)
                            itemImage.color = selectedColor;
                    }
                    else
                    {
                        var itemImage = item.gameObject.GetComponent<Image>();
                        if (itemImage)
                            itemImage.color = otherColor;
                    }
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
                    //set the objects to disabled and enable the ones that now should be displayed.
                    item.gameObject.SetActive(false);
                    if (item.index < currentItemIndex + 5 && !(item.index < currentItemIndex))
                        item.gameObject.SetActive(true);

                    //move the item in the list
                    var itemTransform = item.gameObject.transform;
                    itemTransform.localPosition = new Vector3(itemTransform.localPosition.x, itemTransform.localPosition.y + 85, 0);

                    //set the color of the now selected item.
                    if (item.index == currentItemIndex)
                    {
                        var itemImage = item.gameObject.GetComponent<Image>();
                        if (itemImage)
                            itemImage.color = selectedColor;
                    }
                    else
                    {
                        var itemImage = item.gameObject.GetComponent<Image>();
                        if (itemImage)
                            itemImage.color = otherColor;
                    }
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
                        bool isPrimary = item.itemId == hudManager.player.primaryWeapon ? true : false;
                        bool isSecondary = item.itemId == hudManager.player.secondaryWeapon ? true : false;
                        detailScript.Initilize(item.itemName, Enum.GetName(typeof(weaponTypes), item.Type), Enum.GetName(typeof(bulletTypes), item.bulletType),
                            item.bulletSpeed.ToString(), item.Damage.ToString(), item.shotTimer.ToString(), item.itemId, isPrimary, isSecondary, this, 
                            item.clipSize.ToString(), item.reloadTime.ToString());
                    }
                }
            }
        }
    }
    #endregion
}
