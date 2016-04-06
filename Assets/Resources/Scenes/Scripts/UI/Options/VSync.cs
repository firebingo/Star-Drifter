using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Toggle))]
public class VSync : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;

    void Awake()
    {
        toggle.isOn = GameController.instance.options.vSync;
    }

    /// <summary>
    /// Unity's OnEnable function. Is called when the gameobject is set active.
    /// </summary>
    void OnEnable()
    {
        toggle.isOn = GameController.instance.options.vSync;
    }

    /// <summary>
    /// Unity start function
    /// </summary>
    void Start()
    {
        if (!toggle)
            toggle = this.GetComponent<Toggle>();
    }

    /// <summary>
    /// Call when the toggle is changed. Changes the setting and saves it.
    /// </summary>
    public void onChange()
    {
        GameController.instance.options.vSync = toggle.isOn;
        GameController.instance.options.applyGraphicsSettings();
    }
}
