using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalStateManager: MonoBehaviour
{
    private IList<int> deadPlayers = new List<int>();

    public void PlayerDied(int playerNumber)
    {
        deadPlayers.Add(playerNumber);

        if (deadPlayers.Count >= 2) 
        {
            Invoke("CheckPlayersDeath", .3f);
        }  
    }

    void CheckPlayersDeath() 
    {
        if (!deadPlayers.Contains(1)) {
            Debug.Log("Player 1 is the winner!");
        }
        else if (!deadPlayers.Contains(2)) {
            Debug.Log("Player 2 is the winner!");
        }
        else if (!deadPlayers.Contains(3)) {
            Debug.Log("Player 3 is the winner!");
        }
        // else if (!deadPlayers.Contains(4)) {
        //     Debug.Log("Player 4 is the winner!");
        // }
        else 
        {
            Debug.Log("The game ended in a draw!");
        }

        Application.Quit();
    } 
}
