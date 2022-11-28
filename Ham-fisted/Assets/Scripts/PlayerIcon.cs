using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIcon : MonoBehaviour
{
    public void SetColor (int index)
    {
        GetComponent<Image>().color = GameManager.instance.colors[index];
    }
}
