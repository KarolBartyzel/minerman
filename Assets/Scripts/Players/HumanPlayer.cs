using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HumanPlayer: BasePlayer {

    public Text coinsText;
    public Image shieldImg;

    protected override void Init()
    {
        this.shieldImg.enabled = false;
        playerId = globalManager.AddPlayer(string.IsNullOrEmpty(GlobalState.playerName) ? "Anonim" : GlobalState.playerName, true);
        hasLight = true;
    }

    protected override void UpdateCollectables()
    {
        coinsText.text = "Coins: " + base.coins;
        this.shieldImg.enabled = base.hasShield;
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