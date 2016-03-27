using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private int bulletSpeed = 5;

    [SerializeField]
    private float bulletTime = 3;

    private float bulletTimer = 0;

    public void Fire()
    {
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        temp.GetComponent<BulletController>().Initialize(bulletSpeed, bulletTime);
    }
}
