using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scores : MonoBehaviour
{
    public GameObject scores;
    public GameObject menu;

    public void Back()
    {
        scores.SetActive(false);
        menu.SetActive(true);
    }
}
