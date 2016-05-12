using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour 
{
    GameObject tile;


    Vector2 coords;

	// Update is called once per frame
	void Update() 
    {
	    //if (Input.GetKeyDown(KeyCode.Alpha6))
        if (Input.anyKeyDown)
        {
            switch (Input.inputString)
            {
                case "6":
                    tile = Resources.Load("Prefabs/Tile_0") as GameObject;
                    coords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Instantiate(tile, coords, tile.transform.rotation);
                    break;
                case "7":
                    tile = Resources.Load("Prefabs/Tile_1") as GameObject;
                    coords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Instantiate(tile, coords, tile.transform.rotation);
                    break;
                case "8":
                    tile = Resources.Load("Prefabs/Tile_2") as GameObject;
                    coords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Instantiate(tile, coords, tile.transform.rotation);
                    break;
            }
        }
	}
}
