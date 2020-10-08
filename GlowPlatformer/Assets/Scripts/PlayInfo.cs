using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInfo : MonoBehaviour
{

    public GameObject InfoText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        InfoText.SetActive(true);
    }

    private void OnMouseExit()
    {
        InfoText.SetActive(false);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
