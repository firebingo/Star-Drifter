using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    public itemType currentCategory; //the current inventory category that is selected.
    public itemType lastCategory; //the last category selected.

    [SerializeField]
    private Scrollbar inventoryBar;
    public int currentItemIndex; //the current item at the top of the inventory scrolling.

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
                if (created)
                {
                    created.transform.parent = this.transform;
                    created.transform.localPosition = new Vector3(-133, 198 - (i * 85), 0);
                    var itemScript = created.GetComponent<UIItem>();
                    uiItems.Add(itemScript);
                    itemScript.Initilize("Textures/Items/" + item.itemName, item.itemName, item.count);
                    itemScript.index = i;
                    if(i > currentItemIndex + 5)
                        created.SetActive(false);
                }
            }
            ++i;
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
                ++currentItemIndex;
                foreach (var item in uiItems)
                {
                    item.gameObject.SetActive(false);
                    if (item.index < currentItemIndex + 5)
                        item.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (currentItemIndex < uiItems.Count)
            {
                --currentItemIndex;
                foreach (var item in uiItems)
                {
                    item.gameObject.SetActive(false);
                    if (item.index < currentItemIndex + 5)
                        item.gameObject.SetActive(true);
                }
            }
        }
    }
}
