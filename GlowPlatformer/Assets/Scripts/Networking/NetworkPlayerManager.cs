using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerManager : PlayerManager
{
    public static NetworkPlayerManager instance;

    protected void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
