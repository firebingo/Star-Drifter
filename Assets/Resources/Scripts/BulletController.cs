using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour 
{
    [SerializeField]
    private int speed = 5;

    [SerializeField]
    private float bulletTime = 3;

    private float bulletTimer = 0;

    /// <summary>
    /// Unity's Start Function
    /// </summary>
    void Start () 
    {
	    
	}

    /// <summary>
    /// Unity's Update Function
    /// </summary>
    void Update () 
    {
        transform.position += transform.up.normalized * speed * Time.deltaTime;

        if (bulletTimer >= bulletTime)
            Destroy(this.gameObject);

        bulletTimer += Time.deltaTime;
	}
}
