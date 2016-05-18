using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    public itemType currentCategory; //the current inventory category that is selected.
    public itemType lastCategory; //the last category selected.

    public int currentitemIndex; //the current item at the top of the inventory scrolling.

    [SerializeField]
    private HUDManager hudManager;

    private List<UIItem> uiItems;

    // Use this for initialization
    void Start()
    {
        currentCategory = 0;
        lastCategory = 0;

        uiItems = new List<UIItem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!hudManager)
        {
            Debug.Log("Inventory missing HUD Manager Reference");
            return;
        }
        if(currentCategory != lastCategory)
        {
            lastCategory = currentCategory;
            generateUIItems(currentCategory);
        }
    }

    void OnEnable()
    {
        foreach(var item in uiItems)
        {
            Destroy(item.gameObject);
        }
        uiItems.Clear();
        generateUIItems(currentCategory);
    }

    void openInventory()
    {

    }

    void generateUIItems(itemType type)
    {
        var items = hudManager.player.playerInventory.items.Values.Where(i => i.inventoryItemType == currentCategory).ToList();

        foreach(var item in items)
        {
            int i = 0;
            var toAdd = Resources.Load("Prefabs/UI/InventoryUIItem") as GameObject;
            if (toAdd)
            {
                var created = Instantiate(toAdd);
                created.transform.parent = this.transform;
                if (created)
                {
                    var itemScript = created.GetComponent<UIItem>();
                    uiItems.Add(itemScript);
                    itemScript.Initilize("Textures/Items/" + item.itemName, item.itemName, item.count);
                    itemScript.index = i;
                }
            }
            ++i;
        }
    }
}
