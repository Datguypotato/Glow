using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    //waiting for soundbites
    //[SerializeField] AudioClip[] notes;
    //[SerializeField] int noteCounter = 0;

    AudioSource m_audioSource;

    public AudioSource loop1;
    public AudioSource loop2;
    public AudioSource loop3;
    public AudioSource loop4;
    public AudioSource loop5;

    public int musicIntensity = 0;
    bool onCd;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        loop1.mute = true;
        loop2.mute = true;
        loop3.mute = true;
        loop4.mute = true;
        loop5.mute = true;
    }

    //public void PlayNote()
    //{
    //    m_audioSource.PlayOneShot(notes[noteCounter]);
    //    noteCounter++;

    //    if (noteCounter >= notes.Length)
    //        noteCounter = 0;
    //}

    private void Update()
    {
        if (musicIntensity > 0 && onCd == false)
        {
            StartCoroutine(CountDown());
        }

        if (musicIntensity < 4)
        {
            loop1.mute = false;
        }
        else
        {
            loop1.mute = true;
        }

        if (musicIntensity > 3 && musicIntensity < 7)
        {
            loop2.mute = false;
        }
        else
        {
            loop2.mute = true;
        }

        if (musicIntensity > 6)
        {
            loop3.mute = false;
        }
        else
        {
            loop3.mute = true;
        }

       // if (musicIntensity < 9 && musicIntensity > 12)
    }

    IEnumerator CountDown()
    {
        onCd = true;
        yield return new WaitForSeconds(10);
        musicIntensity -= 1;
        onCd = false;
    }

    // called from the playermanager
    public void ChangeMusic()
    {
        musicIntensity += 1;
    }
}
