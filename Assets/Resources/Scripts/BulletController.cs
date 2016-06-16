using UnityEngine;
using System.Collections;

public enum bulletTypes
{
    Basic,
    Explosive
}

public class BulletController : MonoBehaviour
{
    public bulletTypes bulletType;

    private float bulletSpeed;

    private float bulletTime;

    private float bulletTimer;

    private float damage;

    [SerializeField]
    private float bulletSpread;

    private GameObject particles;

    void Start()
    {
        transform.Rotate(0, 0, Random.Range(-bulletSpread, bulletSpread));
    }

    /// <summary>
    /// Unity's Update Function
    /// </summary>
    /// 
    void Update()
    {
        transform.position += transform.up.normalized * bulletSpeed * Time.deltaTime;

        if (bulletTimer >= bulletTime)
        {
            if (bulletType == bulletTypes.Explosive)
            {
                AreaDamage();
                Destroy(this.gameObject);
            }
            else
                Destroy(this.gameObject);
        }

        bulletTimer += Time.deltaTime;
    }

    public void Initialize(bulletTypes bType, float speed, float time, float bulletDamage, float accuracy, weaponLayers type)
    {
        bulletType = bType;
        bulletSpeed = speed;
        bulletTime = time;
        damage = bulletDamage;
        bulletSpread = accuracy;
        gameObject.layer = (int)type; //Determines collision layers (8 = player vs 9 = enemy)
       
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (bulletType == bulletTypes.Explosive)
        {
            AreaDamage();
            Destroy(this.gameObject);
        }
        else
        {
            other.gameObject.SendMessage("ApplyDamage", damage);

            //Load particles for collision
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
                particles = (GameObject)Resources.Load("Prefabs/BulletParticleSystem-Character");
            else
                particles = (GameObject)Resources.Load("Prefabs/BulletParticleSystem-Tiles");
            
            Instantiate(particles, transform.position, transform.rotation);

            Destroy(this.gameObject);
        }
    }

    void AreaDamage()
    {
        particles = (GameObject)Resources.Load("Prefabs/BulletParticleSystem-Explosions");
        Instantiate(particles, transform.position, transform.rotation);

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 3);

        foreach (Collider2D col in targets)
        {
            if (col.tag != "Player")
                col.SendMessage("ApplyDamage", damage);
        }
    }
}
