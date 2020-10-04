using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : BasePlayer
{
    [Header("Helpfull values    DONT EDIT VALUES")]
    [SerializeField] private string m_ID;

    Vector3 joyDir = Vector3.zero;
    [SerializeField] private float m_Speed = 4;
    

    protected override void Start()
    {
        base.Start();
        Color randColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GetComponentInChildren<MeshRenderer>().material.color = randColor;
    }

    private void Update()
    {
        rb.AddForce(joyDir * m_Speed);
    }

    public void SetJoyDir(Vector3 a_dir)
    {
        joyDir = a_dir;
    }

    public void SetID(string a_ID)
    {
        m_ID = a_ID;
    }

    public string GetID()
    {
        return m_ID;
    }

    protected override void OnTargetHit(Collision2D collision)
    {
        base.OnTargetHit(collision);

        if (collision.gameObject.GetComponent<BasePlayer>() != null && !collision.gameObject.GetComponent<BasePlayer>().hasCollided)
        {
            BasePlayer lp = collision.gameObject.GetComponent<BasePlayer>();
            Vector3 collPoint = (transform.position + lp.transform.position) / 2;
            hasCollided = true;

        }
    }
}
