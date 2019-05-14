using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerStatus 
{
    private static readonly IList<string> BOT_NAMES = new List<string> { "Julia", "Dominik", "Karol" };
    private static readonly System.Random random = new System.Random();

    private static int nextId = 1;

    public int id;
    public string name;
    public bool isHuman;
    public int health;
    public int points;

    public PlayerStatus(string name = "", bool isHuman = false)
    {
        this.id = PlayerStatus.nextId++;
        this.name = !string.IsNullOrEmpty(name) ? name : "Bot " + BOT_NAMES[random.Next(0, 3)];

        this.isHuman = isHuman;
        this.health = 100;
        this.points = 0;
    }
}
