using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    public bool hasCollided = false;
    public bool isTicker = false;

    [SerializeField] private AudioClip[] m_CollSFX;
    private AudioSource m_AudioSource;
    private int lastIndex = 0;

    //[SerializeField] protected ParticleSystem m_Particle;

    [SerializeField] protected float m_KnockbackForce = 500;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        //m_Particle.Stop();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !collision.gameObject.GetComponent<BasePlayer>().hasCollided)
        {
            OnTargetHit(collision);
        }

        if (!collision.transform.CompareTag("Player"))
        {
            Debug.Log("hit wall");
            int randomIndex = Random.Range(0, m_CollSFX.Length);
            // making sure you dont get the same in a row
            if (randomIndex == lastIndex)
            {
                if (randomIndex == m_CollSFX.Length)
                    randomIndex = 0;
                else
                    randomIndex++;
            }
                

            m_AudioSource.PlayOneShot(m_CollSFX[randomIndex]);

            Vector2 knockbackDir = transform.position - collision.transform.position;
            rb.AddForce(knockbackDir.normalized * m_KnockbackForce);
        }
    }

    //public ParticleSystem GetParticle()
    //{
    //    return m_Particle;
    //}

    protected virtual void OnTargetHit(Collision2D collision)
    {

        BasePlayer bp = collision.gameObject.GetComponent<BasePlayer>();
        hasCollided = true;

        if (isTicker == bp.isTicker)
        {
            return;
        }

        if (isTicker)
        {
            bp.isTicker = true;
            isTicker = false;
        }
        else if (bp.isTicker)
        {
            isTicker = true;
            bp.isTicker = false;
        }
    }

    //protected virtual void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        m_Particle.Play();
    //    }
    //}

    //protected virtual void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        m_Particle.Stop();
    //    }
    //}
}
