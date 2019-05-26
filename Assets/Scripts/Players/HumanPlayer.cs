using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HumanPlayer: BasePlayer {

    public Text coinsText;
    protected override void Init()
    {
        playerId = globalManager.AddPlayer(string.IsNullOrEmpty(GlobalState.playerName) ? "Anonim" : GlobalState.playerName, true);
        hasLight = true;
    }

    void Update()
    {
        base.Update();
        String coinsCount = new String('I', base.coins);
        coinsText.text = "Coins: " + coinsCount;
    }

    override protected void UpdateMovement()
    {
        animator.SetBool("Walking", false);

        if (Input.GetKey(KeyCode.W))
        {
            moveUp();
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveRight();
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDown();
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }

        if (canDropBombs && Input.GetKeyDown(KeyCode.Space))
        {
            DropBomb();
        }
    }
}