using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour 
{
    GameObject tile;

    GameObject ghost;

    Vector2 coords;

    public bool building = false;

    bool startTimer = false;

    float delay = 1; //Delay before being able to fire again after building.

    float timer = 0;

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
                         if (building == true)
                            Destroy(ghost.gameObject);
                        tile = Resources.Load("Prefabs/Tile_0") as GameObject;
                        ghost = (GameObject)Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                        ghost.GetComponent<SpriteRenderer>().color = Color.Lerp(ghost.GetComponent<SpriteRenderer>().color, new Color(ghost.GetComponent<SpriteRenderer>().color.r, ghost.GetComponent<SpriteRenderer>().color.b, ghost.GetComponent<SpriteRenderer>().color.g, 0.5f), 1);
                        ghost.gameObject.layer = 13; //Sets the ghost tile to no collisions
                        building = true;
                        break;
                case "7":
                        if (building == true)
                            Destroy(ghost.gameObject);
                        tile = Resources.Load("Prefabs/Tile_1") as GameObject;
                        ghost = (GameObject)Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                        ghost.GetComponent<SpriteRenderer>().color = Color.Lerp(ghost.GetComponent<SpriteRenderer>().color, new Color(ghost.GetComponent<SpriteRenderer>().color.r, ghost.GetComponent<SpriteRenderer>().color.b, ghost.GetComponent<SpriteRenderer>().color.g, 0.5f), 1);
                        ghost.gameObject.layer = 13; //Sets the ghost tile to no collisions
                        building = true;
                        break;
                case "8":
                        if (building == true)
                            Destroy(ghost.gameObject);
                        tile = Resources.Load("Prefabs/Tile_2") as GameObject;
                        ghost = (GameObject)Instantiate(tile, new Vector3(Mathf.Round(coords.x), Mathf.Round(coords.y), 0), tile.transform.rotation);
                        ghost.GetComponent<SpriteRenderer>().color = Color.Lerp(ghost.GetComponent<SpriteRenderer>().color, new Color(ghost.GetComponent<SpriteRenderer>().color.r, ghost.GetComponent<SpriteRenderer>().color.b, ghost.GetComponent<SpriteRenderer>().color.g, 0.5f), 1);
                        ghost.gameObject.layer = 13; //Sets the ghost tile to no collisions
                        building = true;
                        break;
            }

            if (Input.GetButton("Fire"))
            {
                if (!Physics2D.Raycast(new Vector2(Mathf.Round(coords.x), Mathf.Round(coords.y - 1)), Vector2.up, 1.5f, LayerMask.GetMask("Tile")))
                {
                    if (ghost != null)
                    {
                        ghost.GetComponent<SpriteRenderer>().color = Color.Lerp(ghost.GetComponent<SpriteRenderer>().color, new Color(ghost.GetComponent<SpriteRenderer>().color.r, ghost.GetComponent<SpriteRenderer>().color.b, ghost.GetComponent<SpriteRenderer>().color.g, 1), 1);
                        ghost.gameObject.layer = 12; //Puts the tile back in the Tile layer when placed.
                    }

                    if (building == true && ghost != null)
                    {
                        startTimer = true;
                    }
                    ghost = null;
                }
            }
        }

        if (startTimer == true)
        {
            timer += Time.deltaTime;

            if (timer >= delay)
            {
                building = false;
                timer = 0;
                startTimer = false;
            }
        }
    }
}
