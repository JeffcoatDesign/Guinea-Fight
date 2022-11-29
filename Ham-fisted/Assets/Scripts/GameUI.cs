using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameUI : MonoBehaviourPun
{
    public Transform row;
    public GameObject playerIconPrefab;

    public static GameUI instance;

    private PlayerIcon[] icons = new PlayerIcon[12];

    private void Awake()
    {
        instance = this;
    }

    [PunRPC]
    public void SpawnPlayerIcon (int id)
    {
        PlayerIcon icon = Instantiate(playerIconPrefab, row).GetComponentInChildren<PlayerIcon>();
        icon.SetNumber(id);
        icon.SetColor(id - 1);
        icons[id - 1] = icon;
    }

    [PunRPC]
    public void RemoveIcon (int id)
    {
        icons[id - 1].Remove();
    }
}
