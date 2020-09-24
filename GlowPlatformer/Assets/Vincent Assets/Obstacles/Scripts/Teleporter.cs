using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform tp2;
    bool isOpen = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject && isOpen)
        {
            collision.gameObject.transform.position = tp2.position;
            StartCoroutine(Closing());
        }
    }

    IEnumerator Closing()
    {
        isOpen = false;

        yield return new WaitForSeconds(5);

        isOpen = true;
    }
}
