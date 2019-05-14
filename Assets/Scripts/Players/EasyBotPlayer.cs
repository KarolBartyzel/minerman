using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class EasyBotPlayer: BotPlayer {
    override protected void UpdateMovement()
    {
        if (rigidBody.velocity.magnitude < 1) {
            makeStep();
        }

        animator.SetBool("Walking", false);
        move();
    }
}