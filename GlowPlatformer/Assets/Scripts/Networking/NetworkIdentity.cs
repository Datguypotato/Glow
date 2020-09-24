using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class NetworkIdentity : MonoBehaviour
{
    [SerializeField] float m_Speed;

    [Header("Helpfull values    DONT EDIT VALUES")]
    [SerializeField] private string m_ID;
    [SerializeField] private bool m_IsControlling;

    private SocketIOComponent m_Socket;
    // Start is called before the first frame update
    public void Awake()
    {
        m_IsControlling = false;
    }

    public void Move(float x, float y)
    {
        transform.position += new Vector3(x, y, 0) * m_Speed * Time.deltaTime;
    }

    public void SetControllerID(string a_ID)
    {
        m_ID = a_ID;

        m_IsControlling = (NetworkClient.Client_ID == m_ID) ? true : false;
    }

    public void SetSocketRefference(SocketIOComponent a_Socket)
    {
        m_Socket = a_Socket;
    }


    public string GetId()
    {
        return m_ID;
    }


    public bool isControlling()
    {
        return m_IsControlling;
    }

    public SocketIOComponent GetSocket()
    {
        return m_Socket;
    }
}
