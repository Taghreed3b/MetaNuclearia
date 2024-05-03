using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class welcomeSound : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        // تأكد من وجود AudioSource في الكائن
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
