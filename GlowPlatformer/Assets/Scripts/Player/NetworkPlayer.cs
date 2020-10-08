using System.Collections;
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
        Color randColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        modelMeshRenderer.material.color = randColor;
    }

    private void Update()
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
            anim.SetBool("Moving", false);
        else
            anim.SetBool("Moving", true);
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

    protected override void OnPlayerHit(Collision2D collision)
    {
        base.OnPlayerHit(collision);

        if (collision.gameObject.GetComponent<BasePlayer>() != null && !collision.gameObject.GetComponent<BasePlayer>().hasCollided)
        {
            BasePlayer lp = collision.gameObject.GetComponent<BasePlayer>();
            Vector3 collPoint = (transform.position + lp.transform.position) / 2;
            hasCollided = true;

        }
    }
}
