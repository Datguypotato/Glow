using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalPlayerManager : MonoBehaviour
{
    public static LocalPlayerManager instance;

    [SerializeField] private GameObject m_localPlayerPrefab;
    [SerializeField] private Transform m_PlayerHolder;
    [SerializeField] private LocalPlayer m_CurrFocusedPlayer;

    [SerializeField] private TMP_InputField up;
    [SerializeField] private TMP_InputField down;
    [SerializeField] private TMP_InputField left;
    [SerializeField] private TMP_InputField right;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SpawnPlayer()
    {
        Debug.Log("spawned player");
        Instantiate(m_localPlayerPrefab, m_PlayerHolder);
    }

    public void UpdateInputField(LocalPlayer a_localPlayer)
    {
        m_CurrFocusedPlayer = a_localPlayer;

        up.text = a_localPlayer.up.ToString();
        down.text = a_localPlayer.down.ToString();
        left.text = a_localPlayer.left.ToString();
        right.text = a_localPlayer.right.ToString();
    }

    public void UpdatePlayerInputs()
    {
        m_CurrFocusedPlayer.up = (KeyCode)System.Enum.Parse(typeof(KeyCode), up.text.ToUpper());
        m_CurrFocusedPlayer.down = (KeyCode)System.Enum.Parse(typeof(KeyCode), down.text.ToUpper());
        m_CurrFocusedPlayer.left = (KeyCode)System.Enum.Parse(typeof(KeyCode), left.text.ToUpper());
        m_CurrFocusedPlayer.right = (KeyCode)System.Enum.Parse(typeof(KeyCode), right.text.ToUpper());

        Debug.Log("Updated " + m_CurrFocusedPlayer.name + " inputs");
    }
}
