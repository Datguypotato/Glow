using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    [HideInInspector] public bool hasCollided = false;
    public bool isTicker = false;

    [SerializeField] private AudioClip[] m_CollSFX;
    private AudioSource m_AudioSource;
    private int lastIndex = 0;
    
    [SerializeField] protected float m_KnockbackForce = 500;
    protected Rigidbody2D rb;
    protected GameObject collEffectPrefab;

    [SerializeField] protected Animator anim;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !collision.gameObject.GetComponent<BasePlayer>().hasCollided)
        {
            OnPlayerHit(collision);
        }

        int randomIndex = Random.Range(0, m_CollSFX.Length);
        // making sure you dont get the same in a row
        if (randomIndex == lastIndex)
        {
            if (randomIndex == m_CollSFX.Length)
                randomIndex = 0;
            else
                randomIndex++;
        }
        GameObject tempEffect = Instantiate(collEffectPrefab, collision.GetContact(0).point, Quaternion.identity);
        Destroy(tempEffect, 2);

        m_AudioSource.PlayOneShot(m_CollSFX[randomIndex]);
    }

    //public ParticleSystem GetParticle()
    //{
    //    return m_Particle;
    //}

    protected virtual void OnPlayerHit(Collision2D collision)
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
