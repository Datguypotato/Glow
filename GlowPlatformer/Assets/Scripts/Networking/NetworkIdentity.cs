using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class NetworkIdentity : MonoBehaviour
{
    [Header("Helpfull values")]
    [GreyOut]
    [SerializeField] private string m_Id;
    [GreyOut]
    [SerializeField] private bool m_IsControlling;

    private SocketIOComponent m_Socket;
    // Start is called before the first frame update
    public void Awake()
    {
        m_IsControlling = false;
    }

    public void SetControllerID(string a_ID)
    {
        m_Id = a_ID;
        m_IsControlling = (NetworkClient.Client_ID == m_Id ? true : false;
    }

    public void SetSocketRefference(SocketIOComponent a_Socket)
    {
        m_Socket = a_Socket;
    }


    public string GetId()
    {
        return m_Id;
    }


    public bool isControlling()
    {
        return m_IsControlling;
    }

    public SocketIOComponent GetSocket()
    {
        return m_Socket;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
