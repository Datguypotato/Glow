using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    public bool isTarget;

    [SerializeField] private AudioClip[] m_CollSFX;
    private AudioSource m_AudioSource;

    [SerializeField] protected ParticleSystem m_Particle;

    [SerializeField] protected float m_KnockbackForce = 500;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        m_Particle.Stop();
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTarget)
        {
            OnTargetHit(collision);
        }

        if (!collision.transform.CompareTag("Player"))
        {
            Debug.Log("hit wall");
            int randomIndex = Random.Range(0, m_CollSFX.Length);
            m_AudioSource.PlayOneShot(m_CollSFX[randomIndex]);

            Vector2 knockbackDir = transform.position - collision.transform.position;
            rb.AddForce(knockbackDir.normalized * m_KnockbackForce);
        }
    }

    protected abstract void OnTargetHit(Collision2D collision);

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Particle.Play();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_Particle.Stop();
        }
    }
}
