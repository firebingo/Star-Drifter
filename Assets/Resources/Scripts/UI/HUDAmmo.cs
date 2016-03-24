using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDAmmo : MonoBehaviour
{
    [SerializeField]
    Text clipText; //The text for the current aviliable ammo in the clip.
    [SerializeField]
    Text maxClipText; //the text for the max amount that can be in a clip.
    [SerializeField]
    Text currentText; //the text for the current amount of aviliable ammo.

    //ex. of the layout of this in order 15/30/300
    public int clipAmount; //the count of ammo in the current clip.
    public int maxClipAmount; //the max amount in a clip.
    public int currentAmount; //the amount of extra ammo held.

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(clipText != null)
            clipText.text = clipAmount.ToString();

        if (maxClipText != null)
            maxClipText.text = maxClipAmount.ToString();

        if (currentText != null)
            currentText.text = currentAmount.ToString();
    }
}
