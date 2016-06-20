using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoadController : MonoBehaviour
{
	public bool saveGame()
	{
		var saveObject = new SaveMasterObject();
		
		var playerObject = FindObjectOfType(typeof(PlayerController)) as PlayerController;
		if(!playerObject) 
		{
			Debug.LogError("SaveLoadController: Player object missing.");
			Debug.Break();
			return false;
		}
		saveObject.playerSave = playerObject.savePlayer();
		
		BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/gameSave.wort", FileMode.OpenOrCreate);
		bf.Serialize(file, saveObject);
        file.Close();
		return true;
	}
	
	public bool loadGame()
	{
		if (File.Exists(Application.dataPath + "/gameSave.wort"))
		{
			BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/gameOptions.wort", FileMode.Open);
            var saveObject = bf.Deserialize(file) as SaveMasterObject;
            file.Close();
			return true;
		}
		else
			return false;
	}
}

[Serializable]
public class SaveMasterObject
{
	public PlayerControllerSave playerSave;
}