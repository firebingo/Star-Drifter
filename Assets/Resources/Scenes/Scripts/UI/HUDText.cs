using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDText : MonoBehaviour
{
    [SerializeField]
    Text uiText; //reference to the UI Text

    public string text; //the current text to display.

    public Color normalColor; //the color the bar should use if it is not fading.

    /// <summary>
    /// Unity's Start Function
    /// </summary>
    void Start()
    {
        //if the text isin't set already try and get it from the object this is attached to.
        if (!uiText)
            uiText = GetComponent<Text>();
    }

    /// <summary>
    /// Unity's Update Function
    /// </summary>
    void Update()
    {
        //set the text and the color
        uiText.text = text;
        uiText.color = normalColor;
    }
}
