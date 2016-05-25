using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponDetails : MonoBehaviour
{
    Text itemName;
    Text weaponClass;
    Text bulletType;
    Text bulletSpeed;
    Text Damage;
    Text fireRate;

    public void Initilize(string itemName, string weaponClass, string bulletType, string bulletSpeed, string damage, string fireRate)
    {
        if (!checkFailure())
            return;

        this.itemName.text = "Name: " + itemName;
        this.weaponClass.text = "Weapon Class: " + weaponClass;
        this.bulletType.text = "Bullet Type: " + bulletType;
        this.bulletSpeed.text = "Bullet Speed: " + bulletSpeed;
        this.Damage.text = "Damage: " + damage;
        this.fireRate.text = "Fire Rate: " + fireRate;
    }

    // Use this for initialization
    void Awake()
    {
        GameObject temp = this.gameObject.transform.FindChild("WeaponType").gameObject;
        weaponClass = temp ? temp.GetComponent<Text>() : null;
        temp = null;

        temp = this.gameObject.transform.FindChild("Name").gameObject;
        itemName = temp ? temp.GetComponent<Text>() : null;
        temp = null;

        temp = this.gameObject.transform.FindChild("BulletType").gameObject;
        bulletType = temp ? temp.GetComponent<Text>() : null;
        temp = null;

        temp = this.gameObject.transform.FindChild("BulletSpeed").gameObject;
        bulletSpeed = temp ? temp.GetComponent<Text>() : null;
        temp = null;

        temp = this.gameObject.transform.FindChild("Damage").gameObject;
        Damage = temp ? temp.GetComponent<Text>() : null;
        temp = null;

        temp = this.gameObject.transform.FindChild("FireRate").gameObject;
        fireRate = temp ? temp.GetComponent<Text>() : null;
        temp = null;

        checkFailure();
    }

    bool checkFailure()
    {
        if (!itemName || !weaponClass || !bulletType || !bulletSpeed || !Damage || !fireRate)
        {
            Debug.Log("Item Details Failed");
            return false;
        }
        return true;
    }
}
