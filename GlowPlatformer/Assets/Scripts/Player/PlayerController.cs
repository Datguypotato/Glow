using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BasePlayer
{
    public float speed;

    public string inputH;
    public string inputV;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis(inputH);
        rigidBody.velocity += speed * h * Vector2.right;

        float v = Input.GetAxis(inputV);
        rigidBody.velocity += speed * v * Vector2.up;
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " has been clicked");
     //   LocalPlayerManager.instance.UpdateInputField(this);
    }

    /*
    protected override void OnPlayerHit(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BasePlayer>() != null)
        {
            BasePlayer bp = collision.gameObject.GetComponent<BasePlayer>();
            Vector3 collPoint = (transform.position + bp.transform.position) / 2;

            LocalPlayerManager.instance.StartRespawn(this, bp, collPoint);
            MusicManager.instance.ChangeMusic();
        }
    }
    */
}
