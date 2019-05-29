using UnityEngine;
using System.Collections;
using System;
using System.Threading;

public class HardBotPlayer: BotPlayer {
    private static readonly System.Random rnd = new System.Random();

    override protected void UpdateMovement()
    {
        if (rigidBody.velocity.magnitude < 1) {
            makeStep();
        }
        animator.SetBool("Walking", false);
        var prob = rnd.Next(0, 1000);
        if (prob < 3) {
            makeStep();
        }
        else if (prob >= 3 && prob < 5) {
            direction = (-1) * direction;
        }
        else if (prob >=5 && prob < 6) {
            DropBomb();
        }

        move();
    }
}