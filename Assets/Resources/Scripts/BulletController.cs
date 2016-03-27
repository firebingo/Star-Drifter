using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour 
{
    private int bulletSpeed;

    private float bulletTime;

    private float bulletTimer;

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
        transform.position += transform.up.normalized * bulletSpeed * Time.deltaTime;

        if (bulletTimer >= bulletTime)
            Destroy(this.gameObject);

        bulletTimer += Time.deltaTime;
	}
    public void Initialize(int speed, float time)
    {
        bulletSpeed = speed;
        bulletTime = time;
    }
}
