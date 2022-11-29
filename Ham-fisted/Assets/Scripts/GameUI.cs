using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameUI : MonoBehaviourPun
{
    public Transform row;
    public GameObject playerIconPrefab;
    public TextMeshProUGUI winText;

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
        icon.SpawnLives(GameManager.instance.playerLives);
        icons[id - 1] = icon;
    }

    [PunRPC]
    public void RemoveLife(int id)
    {
        icons[id - 1].RemoveLife();
    }

    public void RemoveIcon (int id)
    {
        icons[id - 1].Remove();
    }

    public void SetWinText (string text)
    {
        winText.gameObject.SetActive(true);
        winText.text = text + " is the Champion";
    }
}
