using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : BasePlayer
{
    [Header("Controls")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public ParticleSystem ps;

    //[SerializeField] LocalPlayer m_Target;


    [SerializeField] float m_Speed;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(up))
        {
            rb.AddForce(Vector2.up * m_Speed);
            //transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
        }
        if (Input.GetKey(down))
        {
            rb.AddForce(Vector2.down * m_Speed);
            //transform.Translate(Vector3.down * m_Speed * Time.deltaTime);
        }
        if (Input.GetKey(left))
        {
            rb.AddForce(Vector2.left * m_Speed);
            //transform.Translate(Vector3.left * m_Speed * Time.deltaTime);
        }
        if (Input.GetKey(right))
        {
            rb.AddForce(Vector2.right * m_Speed);
            //transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
        }

        if(isTicker == true)
        {
            ps.Play();
        }
        else
        {
            ps.Pause();
        }
    }

    protected override void OnTargetHit(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BasePlayer>() != null && !collision.gameObject.GetComponent<BasePlayer>().hasCollided)
        {
            BasePlayer lp = collision.gameObject.GetComponent<BasePlayer>();
            Vector3 collPoint = (transform.position + lp.transform.position) / 2;
            hasCollided = true;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " has been clicked");
        LocalPlayerManager.instance.UpdateInputField(this);
    }
}
