using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using SimpleJSON;

public class NetworkClient : SocketIOComponent 
{
    [SerializeField] private Transform m_NetworkContainer;
    [SerializeField] private Dictionary<string, NetworkIdentity> m_ServerObjects;

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
        m_ServerObjects = new Dictionary<string, NetworkIdentity>();
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
            JSONNode node = JSON.Parse(E.data.ToString());
            Client_ID = node["id"].Value;

            Debug.Log("Our client ID: " + Client_ID);
        });

        On("spawn", (E) =>
        {
            JSONNode node = JSON.Parse(E.data.ToString());

            string id = node["id"].Value;

            GameObject g = Instantiate(m_PlayerPrefab, m_NetworkContainer);
            g.name = string.Format("Player ({0})", id);
            NetworkIdentity ni = g.GetComponent<NetworkIdentity>();
            ni.SetControllerID(id);
            ni.SetSocketRefference(this);
            m_ServerObjects.Add(id, ni);
        });

        On("disconnected", (E) =>
        {
            JSONNode node = JSON.Parse(E.data.ToString());

            string id = node["id"].Value;

            GameObject g = m_ServerObjects[id].gameObject;
            Destroy(g);
            m_ServerObjects.Remove(id);

        });

        On("updatePosition", (E) =>
        {
            JSONNode node = JSON.Parse(E.data.ToString());

            string id = node["id"].Value;
            float x = node["position"]["x"];
            float y = node["position"]["y"];

            NetworkIdentity ni = m_ServerObjects[id];

            Debug.Log(m_ServerObjects[id]);
            ni.transform.position = new Vector3(x, y, 0);
        });
    }

}

