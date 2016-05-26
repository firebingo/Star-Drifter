//5-24; Allen: Created setStats function. To take player level and set weapon stats as weapon is initialized. 
//              A variable system to be used for enemy initialization.

using UnityEngine;
using System.Collections;

/// <summary>
/// The loot generator, will set the stats of every 
/// weapon/item as soon as they as initialized.
/// </summary>
[RequireComponent(typeof(Weapon))]
public class LootGenerator : MonoBehaviour
{
    //The player for reference of the current level
    [SerializeField]
    private PlayerController player;

    //the weapon to be set up
    [SerializeField]
    public Weapon loot;

    /// <summary>
    /// Destributes stat points randomly to each stat, to a defined max, based on player level.
    /// </summary>
    public void setStats()
    {
        //The max value of any one stat
        int StatMax = (player.Leveler.level*2)+5; //lv1=7 lv5=15 lv25=55 lv100=205
        int SumMax = StatMax * 3;
        //Array to hold the stats, with starting values
        float[] stats = { 1, 1, 1, 1 };
       /* while (Sum(stats) < SumMax)
        {
            int statIndex = Random.Range(0, 4);
            if (stats[statIndex] < StatMax)
                stats[statIndex]++;
        }
        loot.bulletSpread   = stats[0];
        loot.Damage         = stats[1];
        loot.power          = stats[2];
        loot.shotTimer      = stats[3];*/
    }
    private int Sum(int[] stats)
    {
        int sum = 0;
        for (int i = 0; i < stats.Length; i++)
        {
            sum += stats[i];
        }
        return sum;
    }
}