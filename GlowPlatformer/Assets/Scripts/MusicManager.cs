using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    //waiting for soundbites
    [SerializeField] AudioClip[] notes;
    [SerializeField] int noteCounter = 0;

    AudioSource m_audioSource;

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
    }

    public void PlayNote()
    {
        m_audioSource.PlayOneShot(notes[noteCounter]);
        noteCounter++;

        if (noteCounter >= notes.Length)
            noteCounter = 0;
    }
}
