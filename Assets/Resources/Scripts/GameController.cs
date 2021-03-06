using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// This is the GameController class- it acts as the 'Main Loop' of the program.
/// It is designed as a singleton, that is so that only one instance of it can ever exist.
/// Code that does not rely on MonoBehavior will probably be instanciated and executed from this file.
/// </summary>

public enum EntityType
{
    Tile_0, Tile_1, Tile_2, Tile_3, Tile_4,
    Tile_5, Tile_6, Tile_7, Tile_8, Tile_9,
    Tile_10, Tile_11, Tile_12, Tile_13, Tile_14,
    Tile_15, Tile_16, Tile_17, Tile_18, Tile_19,
    Tile_20, Tile_21, Tile_22, Tile_23, Tile_24,
    Tile_25, Tile_26, Tile_27, Tile_28, Tile_29,
    Tile_30, Tile_31, Tile_32, Tile_33, Tile_34,
	Tile_35, Tile_36, Tile_37, Tile_38, Tile_39,
	Tile_40, Tile_41, Tile_42
}

public enum POIType
{
    POI_0, POI_1, POI_2, POI_3
}

[RequireComponent(typeof(OptionsController))]
public class GameController : MonoBehaviour
{
    // Create a static instance variable for the GameController class
    public static GameController instance { get; private set; }

    // Create a public static dictionary for generation nodes and their contents
    public static List<POI> POIs { get; set; }

    public static int tileSize = 1;

	public static float SectorCellSize = 50.0f;
	public static int SectorCellCount = 11;

	private static bool sectorLoaded = false;

    [SerializeField]
    public OptionsController options;

    // Use this for initialization
    void Start()
    {
        // Check to see if the GameController class already has an active instance
        if (instance != null)
        {
            Destroy(this.gameObject);
			return;
        }
        else
        {
            // GameController class does not have an active instance, so set it to our current instance
            instance = this;
            options = this.GetComponent<OptionsController>();
            DontDestroyOnLoad(this.gameObject);
        }

        // Instantiate node dict
        POIs = new List<POI>();

        options.InitOptions();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //quits the application
    public void quitGame(bool save = false)
    {
        if (save)
        {

        }
        Application.Quit();
    }

	public void OnLevelWasLoaded(int level)
	{
		if (!sectorLoaded)
		{
			sectorLoaded = true;

			// Initialize the object pool controller and object pools
			ObjectPoolController opc = GameObject.FindGameObjectWithTag("OPC").GetComponent<ObjectPoolController>();
			opc.Initialize();

			EntityUtil.GenerateSector(new IntVector2(0, 0), 25);
			EntityUtil.GeneratePOI(new IntVector2(-18, -8), POIType.POI_1);
			//EntityUtil.GeneratePOI(new IntVector2(-3, -20), POIType.POI_1);
			//EntityUtil.GeneratePOI(new IntVector2(-25, -10), POIType.POI_2);
		}
	}

	/// <summary>
	/// Loads a level given a scene name. Only uses single scene loading.
	/// This is needed so buttons on menus can call it since they only support
	///  one in parameter.
	/// </summary>
	/// <param name="levelName"></param>
	public void loadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Loads a level given a scene name. Has option for addative loading.
    /// </summary>
    /// <param name="levelName"></param>
    /// <param name="addative"></param>
    public void loadLevel(string levelName, bool addative = false)
    {
        if (addative)
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Loads a level given a scene index. Has option for addative loading.
    /// </summary>
    /// <param name="levelIndex"></param>
    /// <param name="addative"></param>
    public void loadLevel(int levelIndex, bool addative = false)
    {
        if (addative)
            SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
        else
            SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
}
