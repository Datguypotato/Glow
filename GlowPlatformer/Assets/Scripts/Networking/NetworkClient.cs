using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkClient : SocketIOComponent 
{
    [SerializeField] private Transform m_NetworkContainer;
    private Dictionary<string, GameObject> m_ServerObjects;

    [SerializeField] private GameObject m_PlayerPrefab;

    public static string Client_ID { get; private set; }

    public override void Start()
    {
        base.Start();
        Initialize();
        SetUpEvents();
    }

    private void Initialize()
    {
        m_ServerObjects = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    private void SetUpEvents()
    {
        On("open", (E) =>
        {
            Debug.Log("Connection made to the server");
        });

        On("register", (E) =>
        {
            Client_ID = E.ToString().CleanSocketData();
            Debug.Log(E.data.ToString().CleanSocketData());

            Debug.Log("Our client ID: " + E.data.ToString().CleanSocketData());
        });

        On("spawn", (E) =>
        {
            string id = E.ToString().CleanSocketData();

            GameObject g = new GameObject("Server ID: " + id);
            g.transform.SetParent(m_NetworkContainer);
            m_ServerObjects.Add(id, g);
        });

        On("disconnected", (E) =>
        {
            string id = E.ToString().CleanSocketData();

            GameObject g = m_ServerObjects[id];
            Destroy(g);
            m_ServerObjects.Remove(id);

        });
    }
}

