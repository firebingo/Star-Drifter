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
    [SerializeField]
    public PlayerController player;

    /// <summary>
    /// Destributes stat points randomly to each stat, to a defined max, based on player level.
    /// </summary>
    public void setStats()
    {
        // int StatMax = 
        int StatMax = 6;
        int[] stats = { 2, 2, 2, 2 };
        while (Sum(stats) < 16)
        {
            int statIndex = Random.Range(0, 4);
            if (stats[statIndex] < StatMax)
                stats[statIndex]++;
        }
        /*Damage          = stats[0];
        shotTimer       = stats[1];
        Power           = stats[2];
        bulletSpread    = stats[3];*/
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