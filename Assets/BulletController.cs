using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour 
{
    [SerializeField]
    private int speed = 5;

    [SerializeField]
    private float bulletTime = 3;

    private float bulletTimer = 0;

	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.position += transform.up * speed * Time.deltaTime;
        if (bulletTimer >= bulletTime)
        {
            Destroy(this.gameObject);
        }
        bulletTimer += Time.deltaTime;
	}
}
