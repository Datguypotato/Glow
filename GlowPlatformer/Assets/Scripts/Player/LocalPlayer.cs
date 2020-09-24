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
    }
    


    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " has been clicked");
        LocalPlayerManager.instance.UpdateInputField(this);
    }

    protected override void OnTargetHit(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<LocalPlayer>() != null)
        {
            LocalPlayer lp = collision.gameObject.GetComponent<LocalPlayer>();
            Vector3 collPoint = (transform.position + lp.transform.position) / 2;

            LocalPlayerManager.instance.StartRespawn(this, lp, collPoint);
            MusicManager.instance.PlayNote();
        }
    }
}
