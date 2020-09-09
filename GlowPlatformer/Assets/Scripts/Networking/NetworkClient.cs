using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkClient : SocketIOComponent 
{
    [SerializeField] private Transform m_networkContainer;
    private Dictionary<string, GameObject> m_serverObjects;

    public override void Start()
    {
        base.Start();
        Initialize();
        SetUpEvents();
    }

    private void Initialize()
    {
        m_serverObjects = new Dictionary<string, GameObject>();
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
            string id = E.ToString().RemoveQuotes();

            Debug.Log("Our client ID: " + id);
        });

        On("spawn", (E) =>
        {
            string id = E.ToString().RemoveQuotes();

            GameObject g = new GameObject("Server ID: " + id);
            g.transform.SetParent(m_networkContainer);
            m_serverObjects.Add(id, g);
        });

        On("disconnected", (E) =>
        {
            string id = E.ToString().RemoveQuotes();

            GameObject g = m_serverObjects[id];
            Destroy(g);
            m_serverObjects.Remove(id);

        });
    }
}
