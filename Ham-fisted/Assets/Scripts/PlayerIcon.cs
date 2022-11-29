using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerIcon : MonoBehaviour
{
    public float slideSpeed = 0.5f;
    public float slideTime = 2.0f;
    public TextMeshProUGUI numberText;
    public Transform livesContainer;
    public GameObject lifeIcon;
    private GameObject[] lifeIcons;
    private bool sliding = false;

    private void FixedUpdate()
    {
        if (sliding)
            transform.parent.position += new Vector3(0, slideSpeed * Time.deltaTime, 0);
    }

    public void SetColor (int index)
    {
        GetComponent<Image>().color = GameManager.instance.colors[index];
    }

    public void SetNumber(int num)
    {
        numberText.text = num.ToString();
    }

    public void SpawnLives (int amount)
    {
        lifeIcons = new GameObject[amount];
        for (int x = 0; x < lifeIcons.Length; x++)
        {
            GameObject newIcon = Instantiate(lifeIcon, livesContainer);
            lifeIcons[x] = newIcon;
        }
    }

    public void RemoveLife ()
    {
        if (lifeIcons.Length > 0)
        {
            List<GameObject> tempList = new List<GameObject>(lifeIcons);
            Destroy(lifeIcons[lifeIcons.Length - 1]);
            tempList.Remove(lifeIcons[lifeIcons.Length - 1]);
            lifeIcons = tempList.ToArray();
        }
    }

    public void Remove ()
    {
        sliding = true;
        Invoke("StopSliding", slideTime);
    }

    void StopSliding ()
    {
        sliding = false;
    }
}
