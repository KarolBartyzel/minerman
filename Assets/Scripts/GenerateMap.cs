using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class GenerateMap : MonoBehaviour
{
    public GameObject players;
    public GameObject outsideHardWalls;
    public GameObject insideHardWalls;
    public GameObject weakWalls;

    private const int X = 10;
    private const int Z = 9;

    private bool[,] fields = new bool[X, Z];

    void reshuffle(int[] numbers, System.Random rand)
    {
        for (int t = 0; t < numbers.Length; t++ )
        {
            int tmp = numbers[t];
            int r = rand.Next(t, numbers.Length);
            numbers[t] = numbers[r];
            numbers[r] = tmp;
        }
    }

    private bool checkIfDownIndexFree(int x, int y, int len)
    {
        if (x + len >= X - 1)
        {
            return false;
        }

        for (var i = x - 1; i <= x + len; i++)
        {
            for (var j = y - 1; j <= y + 1; j++)
            {
                if (fields[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private bool checkIfRightIndexFree(int x, int y, int len)
    {
        if (y + len >= Z - 1)
        {
            return false;
        }

        for (var i = x - 1; i <= x + 1; i++)
        {
            for (var j = y - 1; j <= y + len; j++)
            {
                if (fields[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    private Tuple<int, int>[] findNextFreeIndexes(int len)
    {
        var res = new Tuple<int, int>[len];

        for (var i = 1; i < X - 1; i++)
        {
            for (var j = 1; j < Z - 1; j++)
            {
                if (checkIfDownIndexFree(i, j, len))
                {
                    for (var k = 0; k < len; k++)
                    {
                        fields[i+k,j] = true;
                        res[k] = new Tuple<int, int>(i+k, j);
                    }
                    return res;
                }
                else if (checkIfRightIndexFree(i, j, len))
                {
                    for (var k = 0; k < len; k++)
                    {
                        fields[i,j+k] = true;
                        res[k] = new Tuple<int, int>(i, j+k);
                    }
                    return res;
                }
            }
        }

        return null;
    }

    private void placeInsideWalls(System.Random rand)
    {
        var lengths = new int[13]{3,3,2,2,2,2,1,1,1,1,1,1,1};
        reshuffle(lengths, rand);
        var res = new Tuple<int, int>[21];
        var i = 0;

        foreach (var len in lengths)
        {
            var indexes = findNextFreeIndexes(len);
            for (var k = 0; k < len; k++)
            {
                res[i++] = indexes == null ? null : indexes[k];
            }
        }

        var g = 0;
        foreach (Transform wall in insideHardWalls.transform)
        {
            if (res[g] != null)
            {
                wall.position = new Vector3(res[g].Item1 + 1, 0.5f, res[g].Item2 - 1);
            }
            else
            {
                wall.gameObject.SetActive(false);
            }
            g++;
        }
    }

    void placeWeakWalls(System.Random rand)
    {
        foreach (Transform wall in weakWalls.transform)
        {
            int x, y;
            do
            {
                x = rand.Next(0, X);
                y = rand.Next(0, Z);
            }
            while(fields[x, y]);
            
            fields[x, y] = true;
            wall.position = new Vector3(x + 1, 0f, y - 1);
        }
    }

    void placePlayer(System.Random rand, Transform player)
    {
        int x, y;
        do
        {
            x = rand.Next(0, X);
            y = rand.Next(0, Z);
        }
        while(fields[x, y]);

        fields[x, y] = true;
        player.position = new Vector3(x + 1, 1.2f, y - 1);
    }

    void Start()
    {
        var rand = new System.Random();

        placeInsideWalls(rand);
        placeWeakWalls(rand);

        using (StreamReader streamReader = File.OpenText(Path.Combine(Application.persistentDataPath, "GameSettings.txt")))
        {
            GameSettings gameSettings = JsonConvert.DeserializeObject<GameSettings>(streamReader.ReadToEnd());

            foreach (Transform player in players.transform)
            {
                if (player.name.StartsWith("Human") || (player.name.StartsWith("Bot") && player.name.Contains(gameSettings.level)))
                {
                    player.gameObject.SetActive(true);
                    placePlayer(rand, player);
                }
            }
        }
    }
}
