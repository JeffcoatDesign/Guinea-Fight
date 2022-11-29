using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    //Changes the scene after the singleton objects are spawned and shows logo
    private void Awake()
    {
        Invoke("Load", 1.0f);
    }
    private void Load()
    {
        SceneManager.LoadScene("Menu");
    }
}
