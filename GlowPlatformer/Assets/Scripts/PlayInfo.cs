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
    string ID;

    float timeAlive;
    int secondsAlive;

    private void Start()
    {
        tickerCheck = gameObject.GetComponentInParent<NetworkPlayer>().isTicker;
        ID = gameObject.GetComponentInParent<NetworkPlayer>().GetID();
    }

    private void Update()
    {
        timeAlive += Time.deltaTime;
        secondsAlive = (int)(timeAlive % 60);
    }

    private void OnMouseEnter()
    {
        InfoText.SetActive(true);
        if(tickerCheck == true)
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
        GameObject.Find("Networking").GetComponent<NetworkClient>().GetPlayersData().Remove(ID);

        Destroy(gameObject);
    }
}
