using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WeaponDetails : MonoBehaviour
{
    UIInventory inventoryScript;
    Text itemName;
    Text weaponClass;
    Text bulletType;
    Text bulletSpeed;
    Text Damage;
    Text fireRate;
    Text clipSize;
    Text reloadTime;
    Guid weaponGuid;
    bool isPrimary;
    bool isSecondary;

    public void Initilize(string itemName, string weaponClass, string bulletType, string bulletSpeed, string damage, string fireRate, Guid weaponGuid, 
        bool isPrimary, bool isSecondary, UIInventory uiScript, string clipSize, string reloadTime)
    {
        if (!checkFailure())
            return;

        this.itemName.text = "Name: " + itemName;
        this.weaponClass.text = "Weapon Class: " + weaponClass;
        this.bulletType.text = "Bullet Type: " + bulletType;
        this.bulletSpeed.text = "Bullet Speed: " + bulletSpeed;
        this.Damage.text = "Damage: " + damage;
        this.fireRate.text = "Fire Rate: " + fireRate;
        this.clipSize.text = "Clip Size: " + clipSize;
        this.reloadTime.text = "Reload Time: " + reloadTime;
        this.weaponGuid = weaponGuid;
        this.isPrimary = isPrimary;
        this.isSecondary = isSecondary;
        inventoryScript = uiScript;

        if (isPrimary)
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (isSecondary)
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
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

        temp = this.gameObject.transform.FindChild("ReloadTime").gameObject;
        reloadTime = temp ? temp.GetComponent<Text>() : null;
        temp = null;

        temp = this.gameObject.transform.FindChild("ClipSize").gameObject;
        clipSize = temp ? temp.GetComponent<Text>() : null;
        temp = null;

        checkFailure();
    }

    bool checkFailure()
    {
        if (!itemName || !weaponClass || !bulletType || !bulletSpeed || !Damage || !fireRate || !reloadTime || !clipSize)
        {
            Debug.Log("Item Details Failed");
            return false;
        }
        return true;
    }

    public void setPrimary()
    {
        if(!isPrimary)
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            inventoryScript.hudManager.player.primaryWeapon = weaponGuid;
            inventoryScript.buildUI();
        }
    }

    public void setSecondary()
    {
        if (!isSecondary)
        {
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            inventoryScript.hudManager.player.secondaryWeapon = weaponGuid;
            inventoryScript.buildUI();
        }
    }
}
