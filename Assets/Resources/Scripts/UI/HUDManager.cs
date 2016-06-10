using UnityEngine;
using System.Collections;

public class HUDManager : MonoBehaviour
{
    private int playerMaxHealth;
    private int playerCurrentHealth;
    private int playerEXP;

    [SerializeField]
    public PlayerController player;
    [SerializeField]
    Leveling playerLeveling;

    [SerializeField]
    valueBar healthBar;
    [SerializeField]
    valueBar EXPBar;
    [SerializeField]
    valueBar AmmoBar;

    [SerializeField]
    public GameObject inventoryParent;

    // Update is called once per frame
    void Update()
    {
        if(EXPBar && healthBar && player && playerLeveling && inventoryParent && AmmoBar)
        {
            healthBar.maxValue = player.maxHealth;
            healthBar.currentValue = player.currentHealth;
            healthBar.ManualUpdate();
            EXPBar.maxValue = playerLeveling.toNextLevel;
            EXPBar.currentValue = playerLeveling.currentExperience;
            EXPBar.minValue = playerLeveling.lastLevel;
            EXPBar.ManualUpdate();

            if (player.usingPrimary)
            {
                if (!(player.playerInventory.items[player.primaryWeapon] as Weapon).reload)
                {
                    AmmoBar.maxValue = (player.playerInventory.items[player.primaryWeapon] as Weapon).clipSize;
                    AmmoBar.currentValue = (player.playerInventory.items[player.primaryWeapon] as Weapon).clip;
                }
                else
                {
                    AmmoBar.maxValue = (player.playerInventory.items[player.primaryWeapon] as Weapon).reloadTime;
                    AmmoBar.currentValue = (player.playerInventory.items[player.primaryWeapon] as Weapon).reloadTimer;
                }
                AmmoBar.ManualUpdate();
            }
            else if(!player.usingPrimary)
            {
                if (!(player.playerInventory.items[player.secondaryWeapon] as Weapon).reload)
                {
                    AmmoBar.maxValue = (player.playerInventory.items[player.secondaryWeapon] as Weapon).clipSize;
                    AmmoBar.currentValue = (player.playerInventory.items[player.secondaryWeapon] as Weapon).clip;
                }
                else
                {
                    AmmoBar.maxValue = (player.playerInventory.items[player.secondaryWeapon] as Weapon).reloadTime;
                    AmmoBar.currentValue = (player.playerInventory.items[player.secondaryWeapon] as Weapon).reloadTimer;
                }
                AmmoBar.ManualUpdate();
            }

            player.HudManager = this;
        }
        else
        {
           Debug.LogError("Hud Manager missing references.");
        }
    }
}
