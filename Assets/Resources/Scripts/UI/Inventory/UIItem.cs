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

    public Guid itemId {get; private set;}

    public int index; //the index of the item in the ui.

    public void Initilize(string imagePath, string itemName, int itemCount, Guid itemId)
    {
        if(itemImage)
            itemImage.sprite = Resources.Load(imagePath) as Sprite;
        if(this.itemName)
            this.itemName.text = itemName;
        if (this.itemCount)
            this.itemCount.text = string.Format("x{0}", itemCount);

        this.itemId = itemId;
    }

    // Use this for initialization
    void Start()
    {
        itemImage = this.transform.GetChild(0).GetComponent<Image>();
        itemName = this.transform.GetChild(1).GetComponent<Text>();
        itemCount = this.transform.GetChild(2).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
