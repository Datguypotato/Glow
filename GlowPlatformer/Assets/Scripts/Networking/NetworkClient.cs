using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkClient : SocketIOComponent 
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        SetUpEvents();
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
    }
}
