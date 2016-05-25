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
    public GameObject inventoryParent;

    // Update is called once per frame
    void Update()
    {
        if(EXPBar && healthBar && player && playerLeveling && inventoryParent)
        {
            healthBar.maxValue = player.maxHealth;
            healthBar.currentValue = player.currentHealth;
            healthBar.ManualUpdate();
            EXPBar.maxValue = playerLeveling.toNextLevel;
            EXPBar.currentValue = playerLeveling.currentExperience;
            EXPBar.minValue = playerLeveling.lastLevel;
            EXPBar.ManualUpdate();
            player.HudManager = this;
        }
        else
        {
            Debug.LogError("Hud Manager missing references.");
        }
    }
}
