using UnityEngine;
using System.Collections;

public class quitGame : MonoBehaviour
{
    /// <summary>
    /// Quits the game when the button is pressed.
    /// </summary>
    public void onPress()
    {
        GameController.instance.quitGame(false);
    }
}
