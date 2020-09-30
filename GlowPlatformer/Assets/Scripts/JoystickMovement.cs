using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{

    protected Joystick joystick;
    Rigidbody rb;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(joystick.Horizontal * speed, rb.velocity.y, joystick.Vertical * speed);
    }
}
