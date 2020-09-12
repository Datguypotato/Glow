using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
public class NetworkTransform : MonoBehaviour
{
    [GreyOut]
    [SerializeField] private Vector3 m_OldPos;

    private NetworkIdentity m_NetworkIdentity;
    private Player m_Player;

    private float stillCounter = ;

    // Start is called before the first frame update
    void Start()
    {
        m_NetworkIdentity = GetComponent<NetworkIdentity>();
        m_OldPos = transform.position;
        m_Player = new Player();
        m_Player.position = new Position();

        if(!m_NetworkIdentity.isControlling)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
