using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class NetworkTransform : MonoBehaviour
{
    [SerializeField] private Vector3 m_OldPos;

    private NetworkIdentity m_NetworkIdentity;
    private PlayerNetwork m_Player;

    private float stillCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_NetworkIdentity = GetComponent<NetworkIdentity>();
        m_OldPos = transform.position;
        m_Player = new PlayerNetwork();
        m_Player.position = new Position();

        if(!m_NetworkIdentity.isControlling())
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(m_NetworkIdentity.isControlling())
        //{
        //    if(m_OldPos != transform.position)
        //    {
        //        m_OldPos = transform.position;
        //        stillCounter = 0;
        //        SendData();
        //    }
        //    else
        //    {
        //        stillCounter += Time.deltaTime;

        //        if(stillCounter >= 1)
        //        {
        //            stillCounter = 0;
        //            SendData();
        //        }
        //    }
        //}
    }

    // deprecated
    //private void SendData()
    //{
    //    m_Player.position.x = Mathf.Round(transform.position.x * 1000.0f) / 1000.0f; 
    //    m_Player.position.y = Mathf.Round(transform.position.y * 1000.0f) / 1000.0f;

    //    m_NetworkIdentity.GetSocket().Emit("updatePosition", new JSONObject(JsonUtility.ToJson(m_Player)));
    //}
}
