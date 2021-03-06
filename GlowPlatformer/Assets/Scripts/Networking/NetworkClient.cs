﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using SimpleJSON;

public class NetworkClient : SocketIOComponent 
{
    public static NetworkClient instance;

    [SerializeField] private Transform m_NetworkContainer;
    [SerializeField] private Dictionary<string, NetworkPlayer> m_ServerObjects;

    [SerializeField] private GameObject m_PlayerPrefab;

    public static string Client_ID { get; private set; }

    public override void Awake()
    {
        base.Awake();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public override void Start()
    {
        base.Start();
        Initialize();
        SetUpEvents();
    }

    private void Initialize()
    {
        m_ServerObjects = new Dictionary<string, NetworkPlayer>();
    }

    // voor playerinfo
    public Dictionary<string, NetworkPlayer> GetPlayersData()
    {
        return m_ServerObjects;
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
        });

        On("spawn", (E) =>
        {
            JSONNode node = JSON.Parse(E.data.ToString());

            string id = node["id"].Value;
            float x = NetworkPlayerManager.instance.GetMinMaxX();
            float y = NetworkPlayerManager.instance.GetMinMaxy();
            Vector3 randomPos = new Vector3(Random.Range(-x, x), Random.Range(-y, y), 0);

            GameObject g = Instantiate(m_PlayerPrefab, randomPos, Quaternion.identity,  m_NetworkContainer);
            g.name = string.Format("Player ({0})", id);

            NetworkPlayer ni = g.GetComponent<NetworkPlayer>();
            ni.SetID(id);
            ni.SetUsername(node["username"]);

            // first player wil be always the ticker
            if (m_ServerObjects.Count == 0)
                ni.isTagger = true;

            m_ServerObjects.Add(id, ni);
        });

        On("webplayerdisconnect", (E) =>
        {
            Debug.Log("a player disconnected");
            JSONNode node = JSON.Parse(E.data.ToString());

            string id = node["id"].Value;

            
            GameObject g = m_ServerObjects[id].gameObject;
            Destroy(g);
            m_ServerObjects.Remove(id);

        });

        // getting web joystick data
        On("joy", (E) =>
        {
            JSONNode node = JSON.Parse(E.data.ToString());

            string id = node["id"].Value;
            float x = node["position"]["x"].AsFloat / 100;
            float y = node["position"]["y"].AsFloat / 100;

            NetworkPlayer ni = m_ServerObjects[id];
            ni.SetJoyDir(new Vector3(x, y, 0));
        });
    }

    // called from PlayInfo
    public void KickPlayer(NetworkPlayer a_Player)
    {
        m_ServerObjects.Remove(a_Player.GetID());
        JSONObject data = new JSONObject();
        data.Add(a_Player.GetID());

        // emit to the user that he/she is kicked
        Emit("kickPlayer", data);
    }

    public void SendStatus(string a_PlayerID, Color a_PlayerColor)
    {        
        JSONObject data = new JSONObject();
        data.Add(a_PlayerID);
        data.Add(a_PlayerColor.r);
        data.Add(a_PlayerColor.g);
        data.Add(a_PlayerColor.b);

        Emit("playerColor", data);
    }

    public void SendStatus(string a_PlayerID, bool a_IsTagger)
    {
        JSONObject data = new JSONObject();
        data.Add(a_PlayerID);
        data.Add(a_IsTagger);

        Emit("playerIsTagger", data);
    }
}

