using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour 
{
    GameObject tile;

    GameObject ghost;

    Vector2 coords;

	// Update is called once per frame
    void Update()
    {
        coords = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (ghost != null)
            ghost.transform.position = new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0);

        //if (Input.GetKeyDown(KeyCode.Alpha6))
        if (Input.anyKeyDown)
        {
            switch (Input.inputString)
            {
                case "6":
                    if (!Physics2D.Raycast(new Vector2(Mathf.Round(coords.x), Mathf.Round(coords.y - 1)), Vector2.up, 1.5f, LayerMask.GetMask("Tile")))//Checking if object is being spawned on another tile (based on layer).
                    {
                        tile = Resources.Load("Prefabs/Tile_0") as GameObject;
                        ghost = (GameObject)Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                        ghost.GetComponent<SpriteRenderer>().color = Color.Lerp(ghost.GetComponent<SpriteRenderer>().color, new Color(ghost.GetComponent<SpriteRenderer>().color.r, ghost.GetComponent<SpriteRenderer>().color.b, ghost.GetComponent<SpriteRenderer>().color.g, 0.5f), 1);
                    }
                    break;
                case "7":
                    if (!Physics2D.Raycast(new Vector2(Mathf.Round(coords.x), Mathf.Round(coords.y - 1)), Vector2.up, 1.5f, LayerMask.GetMask("Tile")))//Checking if object is being spawned on another tile (based on layer).
                    {
                        tile = Resources.Load("Prefabs/Tile_1") as GameObject;
                        ghost = (GameObject)Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                        ghost.GetComponent<SpriteRenderer>().color = Color.Lerp(ghost.GetComponent<SpriteRenderer>().color, new Color(ghost.GetComponent<SpriteRenderer>().color.r, ghost.GetComponent<SpriteRenderer>().color.b, ghost.GetComponent<SpriteRenderer>().color.g, 0.5f), 1);
                    }
                    break;
                case "8":
                    if (!Physics2D.Raycast(new Vector2(Mathf.Round(coords.x), Mathf.Round(coords.y - 1)), Vector2.up, 1.5f, LayerMask.GetMask("Tile")))//Checking if object is being spawned on another tile (based on layer).
                    {
                        tile = Resources.Load("Prefabs/Tile_2") as GameObject;
                        ghost = (GameObject)Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                        ghost.GetComponent<SpriteRenderer>().color = Color.Lerp(ghost.GetComponent<SpriteRenderer>().color, new Color(ghost.GetComponent<SpriteRenderer>().color.r, ghost.GetComponent<SpriteRenderer>().color.b, ghost.GetComponent<SpriteRenderer>().color.g, 0.5f), 1);
                    }
                    break;
            }

            if (Input.GetMouseButtonDown(0))
            {
                ghost.GetComponent<SpriteRenderer>().color = Color.Lerp(ghost.GetComponent<SpriteRenderer>().color, new Color(ghost.GetComponent<SpriteRenderer>().color.r, ghost.GetComponent<SpriteRenderer>().color.b, ghost.GetComponent<SpriteRenderer>().color.g, 1), 1);
                ghost = null;
            }
        }
    }
}
