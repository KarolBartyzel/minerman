using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class EasyBotPlayer: BasePlayer {
    private static readonly int WALL_LAYER = 8;
    private static readonly int PLAYER_LAYER = 9;

    public LayerMask blockMask;
    private int direction = 1;
    private int step = 3;

    private void makeStep() {
        step = (step + direction + 4) % 4;
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(rigidBody.velocity.magnitude);
 
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