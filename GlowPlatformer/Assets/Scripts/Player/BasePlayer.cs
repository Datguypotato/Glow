using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlayer : MonoBehaviour
{
    [HideInInspector] public bool hasCollided = false;
    public bool isTicker = false;
    public ParticleSystem ps;


    [SerializeField] private AudioClip[] m_CollSFX;
    private AudioSource m_AudioSource;
    private int lastIndex = 0;
    
    [SerializeField] protected float m_KnockbackForce = 500;
    protected Rigidbody2D rb;
    [SerializeField] protected GameObject collEffectPrefab;

    [SerializeField] protected Animator[] anim;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_AudioSource = GetComponent<AudioSource>();
        anim = GetComponentsInChildren<Animator>();

        if (isTicker)
        {
            ps.Play(true);
        }
        else
        {
            ps.Stop(true);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !collision.gameObject.GetComponent<BasePlayer>().hasCollided)
        {
            hasCollided = true;
            BasePlayer bp = collision.gameObject.GetComponent<BasePlayer>();
            OnPlayerHit(bp);
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

    protected void OnCollisionExit2D(Collision2D collision)
    {
        hasCollided = false;
    }

    //public ParticleSystem GetParticle()
    //{
    //    return m_Particle;
    //}

    protected void OnPlayerHit(BasePlayer a_Player)
    {
        if (isTicker == a_Player.isTicker)
        {
            return;
        }

        if (isTicker)
        {
            a_Player.isTicker = true;
            isTicker = false;            
        }
        else if (a_Player.isTicker)
        {
            isTicker = true;
            a_Player.isTicker = false;
        }


        if (isTicker)
        {
            ps.Play(true);
            a_Player.ps.Stop(true);
        }
        else
        {
            ps.Stop(true);
            a_Player.ps.Play(true);
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
