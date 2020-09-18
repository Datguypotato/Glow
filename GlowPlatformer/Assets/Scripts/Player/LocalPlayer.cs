using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    //[SerializeField] LocalPlayer m_Target;
    public bool isTarget;

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

    //public LocalPlayer GetTarget()
    //{
    //    return m_Target;
    //}
    

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " has been clicked");
        LocalPlayerManager.instance.UpdateInputField(this);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isTarget)
        {
            if (collision.GetComponent<LocalPlayer>() != null)
            {
                LocalPlayer lp = collision.GetComponent<LocalPlayer>();
                Vector3 collPoint = (transform.position + lp.transform.position) / 2;

                LocalPlayerManager.instance.StartRespawn(this, lp, collPoint);
                MusicManager.instance.PlayNote();
            }
        }
    }

    
}
