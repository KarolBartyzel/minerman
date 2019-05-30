using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class MediumBotPlayer: BotPlayer {
    private static readonly System.Random rnd = new System.Random();

    override protected void NextStep()
    {
        var prob = rnd.Next(0, 1000);
        if (prob < 1) {
            DropBomb();
        }
    }
}