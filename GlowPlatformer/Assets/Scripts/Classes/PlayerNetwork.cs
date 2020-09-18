using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class holding player data

[System.Serializable]
public class PlayerNetwork
{
    public string id;
    public Position position;
    private bool isDead;
}