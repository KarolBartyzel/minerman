using UnityEngine;
using System.Collections;
using System;

public class HumanPlayer: BasePlayer {
    private LoadSceneOnClick loadSceneOnClick;

    void Start()
    {
        base.Start();
        this.loadSceneOnClick = GetComponent<LoadSceneOnClick>();
    }

    override protected void UpdateMovement()
    {
        animator.SetBool("Walking", false);

        if (Input.GetKey(KeyCode.Escape))
        {
            this.loadSceneOnClick.LoadByIndex(0);
        }

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