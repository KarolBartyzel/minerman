using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public GameObject options;
    public GameObject menu;
    
    public void Back()
    {
        options.SetActive(false);
        menu.SetActive(true);
    }
}
