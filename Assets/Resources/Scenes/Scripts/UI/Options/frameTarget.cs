using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Slider))]
public class frameTarget : MonoBehaviour
{

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text text;

    /// <summary>
    /// Unit's awake function
    /// </summary>
    void Awake()
    {
        slider.value = GameController.instance.options.frameTarget;
        if (text != null)
            setText();
    }

    /// <summary>
    /// Unity's OnEnable function. Is called when the gameobject is set active.
    /// </summary>
    void OnEnable()
    {
        slider.value = GameController.instance.options.frameTarget;
        if (text != null)
            setText();
    }

    /// <summary>
    /// Unity start function
    /// </summary>
    void Start()
    {
        if (!slider)
            slider = this.GetComponent<Slider>();
    }

    /// <summary>
    /// Call when the toggle is changed. Changes the setting and saves it.
    /// </summary>
    public void onChange()
    {
        GameController.instance.options.frameTarget = (int)slider.value;
        GameController.instance.options.applyGraphicsSettings();
        if (text != null)
            setText();
    }

    /// <summary>
    /// Sets the text for the control to reflect the frame rate value.
    /// </summary>
    private void setText()
    {
        switch (GameController.instance.options.frameTarget)
        {
            case 0:
                text.text = "Frame Rate Target: 30";
                break;
            default:
            case 1:
                text.text = "Frame Rate Target: 60";
                break;
            case 2:
                text.text = "Frame Rate Target: 75";
                break;
            case 3:
                text.text = "Frame Rate Target: 120";
                break;
            case 4:
                text.text = "Frame Rate Target: 144";
                break;
            case 5:
                text.text = "Frame Rate Target: 300";
                break;
        }
    }
}
