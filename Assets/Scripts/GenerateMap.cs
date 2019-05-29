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
    public GameObject hardWall;
    public GameObject insideHardWall;
    public GameObject weakWall;
    public GameObject floor1;
    public GameObject floor2;

    private const int SIZE = 12;

    private Vector3 basicTopLeftPosition = new Vector3(0, 0f, 10f) + new Vector3(0.5f, 0, 0.5f);
    private Vector3 basicDownRightPosition = new Vector3(12.5f, 0f, -2.5f) + new Vector3(0.5f, 0, 0.5f);

    private bool[,] fields = new bool[SIZE, SIZE];

    private void InstantiateWithActivation(UnityEngine.Object obj, Vector3 position, Quaternion rotation)
    {
        var go = Instantiate(obj, position, rotation) as GameObject;
        go.SetActive(true);
    }

    private void placeOutsideWalls()
    {
        for (var i = 0; i < 2 * SIZE + 2; i++)
        {
            InstantiateWithActivation(hardWall, basicTopLeftPosition + new Vector3(0.25f + 0.5f * i, 0, -0.25f), Quaternion.identity);
            InstantiateWithActivation(hardWall, basicDownRightPosition + new Vector3(0.25f -0.5f * i, 0, -0.25f), Quaternion.identity);

            if (i != 0 && i != 2 * SIZE + 1)
            {
                InstantiateWithActivation(hardWall, basicTopLeftPosition + new Vector3(0.25f , 0, -0.25f - 0.5f * i), Quaternion.identity);
                InstantiateWithActivation(hardWall, basicDownRightPosition + new Vector3(0.25f, 0, -0.25f + 0.5f * i), Quaternion.identity);
            }
        }
    }

    private void placeFloor()
    {
        for (var i = 0; i < Convert.ToInt32(SIZE / 3); i++)
        {
            for (var j = 0; j < Convert.ToInt32(SIZE / 3); j++)
            {
                for (var k = 0; k < 3; k++)
                {
                    for (var l = 0; l < 3; l++)
                    {
                        var floor = k == 1 && l == 1 
                            ? ((i + j) % 2 == 0 ? floor1 : floor2)
                            : ((i + j) % 2 == 0 ? floor2 : floor1);

                        InstantiateWithActivation(floor, basicTopLeftPosition + new Vector3(1f, -1f, -1f) + new Vector3((3 * i + k), 0, -(3 * j + l)), Quaternion.identity);
                    }
                }
            }
        }
    }

    void placeWeakWalls(System.Random rand)
    {
        var countOfWeakWalls = rand.Next(5, 10);
        for (var i = 0; i < countOfWeakWalls; i++)
        {
            int x, y;
            do
            {
                x = rand.Next(0, SIZE);
                y = rand.Next(0, SIZE);
            }
            while(!checkSquare1(x, y));

            fields[x, y] = true;
            InstantiateWithActivation(weakWall, new Vector3(1.5f + x, 0f, -1.5f + y), Quaternion.identity);
        }
    }

    private Vector3 getPositionForPlayer(System.Random rand)
    {
        int x, y;
        do
        {
            x = rand.Next(0, SIZE);
            y = rand.Next(0, SIZE);
        }
        while(!checkSquare1(x, y));

        fields[x, y] = true;
        return new Vector3(1.5f + x, 0f, -1.5f + y);
    }
    
    private void placeInsideWalls(System.Random rand)
    {
        var lengths = new int[9]{4,4,3,3,3,2,2,2,2};
        reshuffle(lengths, rand);
        var res = new Tuple<int, int>[25];
        var i = 0;

        foreach (var len in lengths)
        {
            var indexes = findNextFreeIndexes(len,rand);
            for (var k = 0; k < len; k++)
            {
                res[i++] = indexes == null ? null : indexes[k];
            }
        }

        var g = 0;
        foreach (var r in res)
        {
            if (r != null)
            {
                InstantiateWithActivation(insideHardWall, new Vector3(1.5f + r.Item1, 0f, -1.5f + r.Item2), Quaternion.identity);
            }
        }
    }

    void Start()
    {
        var rand = new System.Random();

        placeOutsideWalls();
        placeFloor();
        placeWeakWalls(rand);

        using (StreamReader streamReader = File.OpenText(Path.Combine(Application.persistentDataPath, "GameSettings.txt")))
        {
            GameSettings gameSettings = JsonConvert.DeserializeObject<GameSettings>(streamReader.ReadToEnd());

            foreach (Transform player in players.transform)
            {
                if (player.name.StartsWith("Human"))
                {
                    InstantiateWithActivation(player.gameObject, getPositionForPlayer(rand), Quaternion.identity);
                }
                if (player.name.StartsWith("Bot") && player.name.Contains(gameSettings.level))
                {
                    var botsCount = gameSettings.level == "Easy" ? 2 : gameSettings.level == "Medium" ? 3 : gameSettings.level == "Hard" ? 4 : 0;
                    for (var i = 0; i < botsCount; i++)
                    {
                        InstantiateWithActivation(player.gameObject, getPositionForPlayer(rand), Quaternion.identity);
                    }
                }
            }
        }

        placeInsideWalls(rand);
    }

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

    private bool checkSquare1(int x, int y)
    {
        if (x < 0 || x > SIZE - 1 || y < 0 || y > SIZE - 1)
        {
            return false;
        }

        if (x > 0)
        {
            if ((y > 0 && fields[x - 1, y - 1]) || fields[x - 1, y] || (y < SIZE - 1 && fields[x - 1, y + 1]))
            {
                return false;
            }
        }
        if ((y > 0 && fields[x, y - 1]) || fields[x, y] || (y < SIZE - 1 && fields[x, y + 1]))
        {
            return false;
        }
        if (x < SIZE - 1)
        {
            if ((y > 0 && fields[x + 1, y - 1]) || fields[x + 1, y] || (y < SIZE - 1 && fields[x + 1, y + 1]))
            {
                return false;
            }
        }

        return true;
    }

    private bool checkSquare2(int x, int y)
    {
        if (x <= 0 || x >= SIZE - 1 || y <= 0 || y >= SIZE - 1)
        {
            return false;
        }

        if (x > 0)
        {
            if ((y > 0 && fields[x - 1, y - 1]) || fields[x - 1, y] || (y < SIZE - 1 && fields[x - 1, y + 1]))
            {
                return false;
            }
        }
        if ((y > 0 && fields[x, y - 1]) || fields[x, y] || (y < SIZE - 1 && fields[x, y + 1]))
        {
            return false;
        }
        if (x < SIZE - 1)
        {
            if ((y > 0 && fields[x + 1, y - 1]) || fields[x + 1, y] || (y < SIZE - 1 && fields[x + 1, y + 1]))
            {
                return false;
            }
        }

        return true;
    }

    private bool checkDirection(int i, int j, int len, int ir, int jr, out Tuple<int, int>[] res)
    {
        res = new Tuple<int, int>[len];
        for (var k = 0; k < len; k++)
        {
            if (!checkSquare2(i + ir * k, j + jr * k))
            {
                return false;
            }
        }
        for (var k = 0; k < len; k++)
        {
            fields[i + ir * k, j + jr * k] = true;
            res[k] = new Tuple<int, int>(i + ir * k, j + jr * k);
        }

        return true;
    }

    private Tuple<int, int>[] findNextFreeIndexes(int len, System.Random rand)
    {
        for (var i = 1; i < SIZE - 1; i++)
        {
            for (var j = 1; j < SIZE - 1; j++)
            {
                int ir, jr;
                Tuple<int, int>[] res;

                if (checkDirection(i, j, len, -1, 0, out res) || checkDirection(i, j, len, 1, 0, out res) || checkDirection(i, j, len, 0, -1, out res) || checkDirection(i, j, len, 0, 1, out res))
                {
                    return res;
                }
            }
        }

        return null;
    }

}
