
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    protected Joystick joystick;
    protected JoyButton joybutton;

    Rigidbody rb;
    bool jump;
    public bool grounded = false;
    public bool wallJump = false;
    public float speed;
  //  Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        jump = false;
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<JoyButton>();


        //  anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded || Input.GetButtonDown("Jump") && wallJump)
        {
            jump = true;
            grounded = false;
            wallJump = false;
        }


        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        //{
        //    anim.enabled = true;
        //}
        //else
        //{
        //    anim.enabled = false;
        //}


    }

    private void FixedUpdate()
    {
        if (jump == true)
        {
            rb.AddForce(Vector2.up * 300);
            jump = false;

        }

        float h = Input.GetAxis("Horizontal");
        rb.velocity += speed * h * Vector3.right;

        rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
                                  

        if (h == 0f && grounded)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            StartCoroutine(LandingLagTimer());
        }
        if (collision.gameObject.tag == "jumpableWall")
        {
            StartCoroutine(WallJumpTime());
        }
    }

    IEnumerator LandingLagTimer()
    {
        yield return new WaitForSeconds(0.2f);
        grounded = true;
    }

    IEnumerator WallJumpTime()
    {
        yield return new WaitForSeconds(0.1f);
        wallJump = true;
        yield return new WaitForSeconds(0.5f);
        wallJump = false;
    }
}
