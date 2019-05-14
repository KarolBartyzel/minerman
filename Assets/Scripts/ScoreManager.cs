using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.IO;
using Newtonsoft.Json;

using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static IList<GameResult> GetResults()
    {
        IList<GameResult> newGameResults = new List<GameResult>();
        if (File.Exists(Path.Combine(Application.persistentDataPath, "Results.txt")))
        {
            using (StreamReader streamReader = File.OpenText(Path.Combine(Application.persistentDataPath, "Results.txt")))
            {
                newGameResults = JsonConvert.DeserializeObject<List<GameResult>>(streamReader.ReadToEnd());
            }
        }
        return newGameResults;
    }

    public static void AddResult(GameResult newGameResult)
    {
        IList<GameResult> gameResults = ScoreManager.GetResults();
        gameResults.Add(newGameResult);
        using (StreamWriter streamWriter = File.CreateText(Path.Combine(Application.persistentDataPath, "Results.txt")))
        {
            streamWriter.Write(JsonConvert.SerializeObject(gameResults, Formatting.Indented));
        }
    }
}
