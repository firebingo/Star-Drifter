using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour 
{
    private float bulletSpeed;

    private float bulletTime;

    private float bulletTimer;

    private float damage;

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

    public void Initialize(float speed, float time, float bulletDamage, int type)
    {
        bulletSpeed = speed;
        bulletTime = time;
        damage = bulletDamage;
        gameObject.layer = type; //Determines collision layers (8 = player vs 9 = enemy)
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            other.SendMessage("ApplyDamage", damage);
            Destroy(this.gameObject);
    }
}
