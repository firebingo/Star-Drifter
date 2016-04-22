using UnityEngine;
using System.Collections;

public class CenterCam : MonoBehaviour 
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

	void Update () 
    {
        CenterCamera();
	}

    void CenterCamera()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
