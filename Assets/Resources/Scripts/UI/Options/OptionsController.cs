using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[RequireComponent(typeof(GameController))]
public class OptionsController : MonoBehaviour
{
    public int frameTarget; //frame rate target, 0 = 30, 1 = 60, 2 = 75, 3 = 120, 4 = 144, 5 = 300
    public bool vSync; // false = off, true = on

    /// <summary>
    /// Initilizies the options. This is done instead of start to make sure
    ///  game controller gets setup first then calls this.
    /// </summary>
    public void InitOptions()
    {
        //load the options and apply them is aviliable. Otherwise load defaults.
        if (!loadOptions())
        {
            frameTarget = 5;
            vSync = false;
        }
        applyGraphicsSettings();
    }

    /// <summary>
    /// applies graphics settings and saves them.
    /// </summary>
    public void applyGraphicsSettings()
    {
        switch (frameTarget)
        {
            case 0:
                Application.targetFrameRate = 30;
                break;
            default:
            case 1:
                Application.targetFrameRate = 60;
                break;
            case 2:
                Application.targetFrameRate = 75;
                break;
            case 3:
                Application.targetFrameRate = 120;
                break;
            case 4:
                Application.targetFrameRate = 144;
                break;
            case 5:
                Application.targetFrameRate = 300;
                break;
        }

        if (vSync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        saveOptions();
    }

    /// <summary>
    /// Saves the games options to a binary file
    /// </summary>
    private void saveOptions()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/gameOptions.wort", FileMode.OpenOrCreate);

        GameOptions options = new GameOptions(vSync, frameTarget);

        bf.Serialize(file, options);
        file.Close();
    }

    /// <summary>
    /// Loads the game options from a file if it exists. returns true if succeeded.
    /// </summary>
    /// <returns></returns>
    private bool loadOptions()
    {
        if (File.Exists(Application.dataPath + "/gameOptions.wort"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/gameOptions.wort", FileMode.Open);
            GameOptions options = bf.Deserialize(file) as GameOptions;
            file.Close();
            vSync = options.vSync;
            frameTarget = options.frameTarget;
            return true;
        }
        else
            return false;
    }
}

[Serializable]
public class GameOptions
{
    public GameOptions(bool iVsync, int iFrameTarget)
    {
        vSync = iVsync;
        frameTarget = iFrameTarget;
    }

    //Options
    public int frameTarget; //frame rate target, 0 = 30, 1 = 60, 2 = 75, 3 = 120, 4 = 144, 5 = 300
    public bool vSync; // false = off, true = on
}