using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class valueBar : MonoBehaviour
{
    [SerializeField]
    Image uiImage; //reference to the Image for the UI Bar

    public float minValue; //the minimum value for the bar
    public float currentValue; //the current value of the bar
    public float maxValue; //the maximum value for the bar

    
    public bool colorFade; //whether or not to fade the colors for the bar.
    public Color normalColor; //the color the bar should use if it is not fading.
    public Color fullColor; //the color to use when the bar is full.
    public Color emptyColor; //the color to use when the bar is empty.

    //the last value of the bar. Used to check if the value of the bar changes before updating it.
    float lastValue; 

    /// <summary>
    /// Unity's Start Function
    /// </summary>
    void Start()
    {
        //set the last value to not equal current value to start.
        lastValue = currentValue - 1;

        //if the image isin't set already try and get it from the object this is attached to.
        if (!uiImage)
        {
            uiImage = GetComponent<Image>();
        }
    }

    /// <summary>
    /// Unity's Update Function
    /// </summary>
    void Update()
    {
        //check if the value of the bar has changed before updating.
        if (currentValue != lastValue && uiImage)
        {
            ManualUpdate();
        }

        lastValue = currentValue;
    }

    //used to manually update the bar if needed
    public void ManualUpdate()
    {
        //change the scale to match the ratio of the current and the max adjusted based on the min.
        transform.localScale = new Vector3((currentValue - minValue) / (maxValue - minValue), 1.0f, 1.0f);

        //fade the color based on the given full and empty colors.
        if (colorFade)
            uiImage.color = Color.Lerp(emptyColor, fullColor, transform.localScale.x);
        else
            uiImage.color = normalColor;
    }
}
