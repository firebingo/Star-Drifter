using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

    //The renderer component
    private Renderer rend;

    //The player
    private GameObject player;
    private PlayerController pControl;

    //Speed of BG
    public Vector2 speed = new Vector2(0,0);


    //offset of BG
    private Vector2 offset;


    // Use this for initialization
    void Start ()
    {
        //Set rend, our renderer component
        rend = GetComponent<Renderer>();

        //find the player
        player = GameObject.Find("Player");
        pControl = player.GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        pControl = player.GetComponent<PlayerController>();

        speed = pControl.velocity;

        //update by time
        offset += new Vector2(Time.fixedDeltaTime * speed.x, Time.fixedDeltaTime * speed.y);


        //apply update
        rend.material.mainTextureOffset = offset;

        //set the NG to the players pos
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 1);
    }
}
