using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// This is the GameController class- it acts as the 'Main Loop' of the program.
/// It is designed as a singleton, that is so that only one instance of it can ever exist.
/// Code that does not rely on MonoBehavior will probably be instanciated and executed from this file.
/// </summary>

[RequireComponent(typeof(OptionsController))]
public class GameController : MonoBehaviour
{
    // Create a static instance variable for the GameController class
    public static GameController instance { get; private set; }

	// Create a public static dictionary for generation nodes and their contents
	public static Dictionary<IntVector2, List<GameObject>> node { get; set; }

	public static int tileSize = 1;

    [SerializeField]
    public OptionsController options;

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
            options = this.GetComponent<OptionsController>();
            DontDestroyOnLoad(this.gameObject);
        }

		// Instantiate node dict
		node = new Dictionary<IntVector2, List<GameObject>>();

        options.InitOptions();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //quits the application
    public void quitGame(bool save = false)
    {
        if(save)
        {

        }
        Application.Quit();
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
        if(addative)
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
