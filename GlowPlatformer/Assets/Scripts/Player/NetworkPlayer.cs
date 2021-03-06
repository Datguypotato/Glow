﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NetworkPlayer : BasePlayer
{
    [Header("Helpfull values    DONT EDIT VALUES")]
    [SerializeField] private string m_ID;

    Vector3 joyDir = Vector3.zero;
    [SerializeField] private float m_Speed = 4;

    [SerializeField] SkinnedMeshRenderer modelMeshRenderer;
    [SerializeField] TMP_Text m_Username;

    [SerializeField] Transform modelTransform;
    [SerializeField] float yrotation = 80;

    protected override void Start()
    {
        base.Start();
        Color randColor = new Color(Random.Range(0.3f, 1f), Random.Range(0.3f, 1f), Random.Range(0.3f, 1f));
        modelMeshRenderer.material.color = randColor;

        SendPlayerColorStatus();
        SendPlayerTickerStatus();
    }

    protected void Update()
    {
        rb.AddForce(joyDir * m_Speed);

        if(joyDir.x > 0)
        {
            modelTransform.rotation = Quaternion.Euler(0, -yrotation, 0);
        }
        else if (joyDir.x < 0)
        {
            modelTransform.rotation = Quaternion.Euler(0, yrotation, 0);
        }
        else
        {
            modelTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void SetJoyDir(Vector3 a_dir)
    {
        joyDir = a_dir;

        if (a_dir == Vector3.zero)
        {
            for (int i = 0; i < anim.Length; i++)
            {
                anim[i].SetBool("Moving", false);
            }
        }
        else
        {
            for (int i = 0; i < anim.Length; i++)
            {
                anim[i].SetBool("Moving", true);
            }
        }            
    }

    public void SetID(string a_ID)
    {
        m_ID = a_ID;
    }

    public string GetID()
    {
        return m_ID;
    }

    public void SetUsername(string a_username)
    {
        m_Username.text = a_username;
    }

    public string Getusername()
    {
        return m_Username.text;
    }
    protected void SendPlayerColorStatus()
    {
        Color playerColor = modelMeshRenderer.material.color;

        NetworkClient.instance.SendStatus(m_ID, playerColor);
    }

    protected void SendPlayerTickerStatus()
    {
        NetworkClient.instance.SendStatus(m_ID, isTagger);
    }
}
