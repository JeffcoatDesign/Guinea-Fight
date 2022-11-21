using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject localPlayerObj;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
}
