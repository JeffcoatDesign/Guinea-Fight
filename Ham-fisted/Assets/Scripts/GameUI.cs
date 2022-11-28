using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameUI : MonoBehaviourPun
{
    public Transform row;
    public GameObject playerIconPrefab;

    public static GameUI instance;

    private GameObject[] icons = new GameObject[12];

    private void Awake()
    {
        instance = this;
    }

    [PunRPC]
    public void SpawnPlayerIcon (int id)
    {
        GameObject icon = Instantiate(playerIconPrefab, row);
        icon.GetComponentInChildren<PlayerIcon>().SetColor(id - 1);
        icons[id - 1] = icon;
    }
}
