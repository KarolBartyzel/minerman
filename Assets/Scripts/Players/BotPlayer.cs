using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class BotPlayer: BasePlayer {
    protected static readonly int WALL_LAYER = 8;
    protected static readonly int PLAYER_LAYER = 9;

    protected int direction = 1;
    protected int step = 3;

    protected override void Init()
    {
        playerId = globalManager.AddPlayer();
    }

    protected override void UpdateCollectables(){}

    protected void makeStep() 
    {
        step = (step + direction + 4) % 4;
    }

    protected void move()
    {
        switch(step) {
            case 0:
                moveUp();
                break;
            case 1:
                moveLeft();
                break;
            case 2:
                moveDown();
                break;
            case 3:
                moveRight();
                break;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == PLAYER_LAYER)
        {
            //place bomb, change direction and turn around
            DropBomb();
            direction = (-1) * direction;
            makeStep();
            makeStep();
        }
    }

    override protected void UpdateMovement()
    {
        if (rigidBody.velocity.magnitude < 1) {
            makeStep();
        }

        animator.SetBool("Walking", false);
        switch(step) {
            case 0:
                moveUp();
                break;
            case 1:
                moveLeft();
                break;
            case 2:
                moveDown();
                break;
            case 3:
                moveRight();
                break;
        }
    }
}