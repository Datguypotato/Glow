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
            Debug.Log("register");
            JSONNode node = JSON.Parse(E.data.ToString());
            Client_ID = node["id"].Value;

            Debug.Log("Our client ID: " + Client_ID);
        });

        On("spawn", (E) =>
        {
            Debug.Log("spawn");
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

        On("joy", (E) =>
        {
            JSONNode node = JSON.Parse(E.data.ToString());

            string id = node["id"].Value;
            float x = node["position"]["x"].AsFloat / 100;
            float y = node["position"]["y"].AsFloat / 100;

            Debug.Log("received position X: " + x + " Y: " + y + "\n from id: " + id);

            NetworkIdentity ni = m_ServerObjects[id];


            //Debug.Log(m_ServerObjects[id]);
            ni.Move(x, y);
        });
    }

}

