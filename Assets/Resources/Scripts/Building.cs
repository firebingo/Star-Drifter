﻿using UnityEngine;
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
                    coords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (!Physics2D.Raycast(new Vector2(Mathf.Round(coords.x), Mathf.Round(coords.y-1)), Vector2.up, 1.5f, LayerMask.GetMask("Tile")))//Checking if object is being spawned on another tile (based on layer).
                    {
                        tile = Resources.Load("Prefabs/Tile_0") as GameObject;
                        Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                    }
                        break;
                case "7":
                    coords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (!Physics2D.Raycast(new Vector2(Mathf.Round(coords.x), Mathf.Round(coords.y-1)), Vector2.up, 1.5f, LayerMask.GetMask("Tile")))//Checking if object is being spawned on another tile (based on layer).
                    {
                        tile = Resources.Load("Prefabs/Tile_1") as GameObject;
                        Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                    }
                    break;
                case "8":
                    coords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (!Physics2D.Raycast(new Vector2(Mathf.Round(coords.x), Mathf.Round(coords.y-1)), Vector2.up, 1.5f, LayerMask.GetMask("Tile")))//Checking if object is being spawned on another tile (based on layer).
                    {
                        tile = Resources.Load("Prefabs/Tile_2") as GameObject;
                        Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                    }
                    break;
            }
        }
	}
}
