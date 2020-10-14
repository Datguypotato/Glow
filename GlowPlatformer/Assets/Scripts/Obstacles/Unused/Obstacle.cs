using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            OnPlayerCollide(collision);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
            OnPlayerInRange(collision);
    }

    protected abstract void OnPlayerCollide(Collision2D collision);

    protected abstract void OnPlayerInRange(Collider2D collision);
}
