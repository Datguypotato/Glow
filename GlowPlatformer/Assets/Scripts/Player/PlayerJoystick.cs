using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoystick : BasePlayer
{
    public float speed;

    protected Joystick joystick;

    Rigidbody2D rigidBody;

    //Rigidbody2D rigidBody;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        joystick = FindObjectOfType<Joystick>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(joystick.Horizontal * speed, joystick.Vertical * speed);
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " has been clicked");
       // LocalPlayerManager.instance.UpdateInputField(this);
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
    
    */
}
