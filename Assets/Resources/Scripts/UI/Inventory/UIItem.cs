using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIItem : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private Text itemName;
    [SerializeField]
    private Text itemCount;
    [SerializeField]
    private Text itemPrimaryStatus;
    [SerializeField]
    private Text itemRarity;

    public Guid itemId {get; private set;}

    public int index; //the index of the item in the ui.

    public bool hasComps = false;

    public void Initilize(string imagePath, string itemName, int itemCount, bool itemPrimary, bool itemSecondary, Guid itemId, string itemRarity)
    {
        if (!hasComps)
            findComps();

        if(itemImage)
            itemImage.sprite = Resources.Load(imagePath) as Sprite;
        if (this.itemName)
            this.itemName.text = (itemName == null ? "No Name" : itemName);
        if (this.itemCount)
            this.itemCount.text = string.Format("x{0}", itemCount);
        if(itemPrimaryStatus)
        {
            if (itemPrimary)
                itemPrimaryStatus.text = "P";
            else if (itemSecondary)
                itemPrimaryStatus.text = "S";
            else
                itemPrimaryStatus.text = "";
        }
        if (this.itemRarity)
            this.itemRarity.text = itemRarity;

        this.itemId = itemId;
    }

    void findComps()
    {
        itemImage = this.transform.GetChild(0).GetComponent<Image>();
        itemName = this.transform.GetChild(1).GetComponent<Text>();
        itemCount = this.transform.GetChild(2).GetComponent<Text>();
        itemPrimaryStatus = this.transform.GetChild(3).GetComponent<Text>();
        itemRarity = this.transform.GetChild(4).GetComponent<Text>();
        hasComps = true;
    }

    // Use this for initialization
    void Start()
    {
        findComps();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
