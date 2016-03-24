using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is the GameController class- it acts as the 'Main Loop' of the program.
/// It is designed as a singleton, that is so that only one instance of it can ever exist.
/// Code that does not rely on MonoBehavior will probably be instanciated and executed from this file.
/// </summary>

// Defining a public enum for TileType here allows us to use it in any other class
public enum TileType
{
    Default,
    Floor,
    WallEW,
    WallNS,
    WallNE,
    WallES,
    WallSW,
    WallNW
}

public class GameController : MonoBehaviour
{
    // Create a static instance variable for the GameController class
    public static GameController instance { get; private set; }

    // Create a static dictionary for tile type textures
    public static Dictionary<TileType, Texture2D> TileDict { get; private set; }

    // Use this for initialization
    void Start()
    {
        // Check to see if the GameController class already has an active instance
        if (instance != null)
        {
            // GameController class already has an active instance, so throw an error and pause execution
            Debug.LogError("You cannot have multiple instances of the GameController class!");
            Debug.Break();
        }
        else
        {
            // GameController class does not have an active instance, so set it to our current instance
            instance = this;
        }

        // Populate the TileDict with resource 
        //TileDict.Add( TileType.Default, Resources.Load<Texture2D>("EmptyTile"));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
