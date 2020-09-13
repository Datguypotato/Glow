using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    [SerializeField] float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(up))
        {
            transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
        }
        if (Input.GetKey(down))
        {
            transform.Translate(Vector3.down * m_Speed * Time.deltaTime);
        }
        if (Input.GetKey(left))
        {
            transform.Translate(Vector3.left * m_Speed * Time.deltaTime);
        }
        if (Input.GetKey(right))
        {
            transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " has been clicked");
        LocalPlayerManager.instance.UpdateInputField(this);
    }

}
