using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayInfo : MonoBehaviour
{
   // public static PlayInfo instance;

    public GameObject InfoText;
    public TextMeshProUGUI TextMesh;
    public string text;
    bool tickerCheck;    

    float timeAlive;
    int secondsAlive;

    private void Start()
    {
        tickerCheck = gameObject.GetComponentInParent<NetworkPlayer>().isTicker;
    }

    private void OnMouseEnter()
    {
        InfoText.SetActive(true);
    }
    private void OnMouseOver()
    {
        timeAlive += Time.deltaTime;
        secondsAlive = (int)(timeAlive % 60);

        if (tickerCheck == true)
        {
            TextMesh.text = "Is tikker\n" + secondsAlive;
        }
        if (tickerCheck == false)
        {
            TextMesh.text = "Is geen tikker\n" + secondsAlive;
        }
    }
    private void OnMouseExit()
    {
        InfoText.SetActive(false);
    }

    private void OnMouseDown()
    {
        NetworkClient.instance.KickPlayer(GetComponent<NetworkPlayer>());

        Destroy(gameObject);
    }
}
