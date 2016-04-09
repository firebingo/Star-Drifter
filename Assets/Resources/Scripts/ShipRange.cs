using UnityEngine;
using System.Collections;

public class ShipRange : MonoBehaviour {

    private ShipController ship;

    [SerializeField]
    private float cooldown = 3;

    [SerializeField]
    public float time = 0;

	void Start () 
    {
        ship = GetComponentInParent<ShipController>();	
	}

    void Update()
    {
        time += Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player" && Input.GetButton("Enter Ship") && time >= cooldown) //Enter ship is e
        {
            Debug.Log("Enter");
            ship.ShipEnter(collider.gameObject);
        }
    }
}
