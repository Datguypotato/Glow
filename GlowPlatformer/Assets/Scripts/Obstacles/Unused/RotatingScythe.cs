using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingScythe : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            LocalObstacleManager.instance.PlayerDie(collision.gameObject);
        }
    }
}
