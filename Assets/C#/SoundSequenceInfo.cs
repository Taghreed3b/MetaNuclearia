using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSequenceInfo : MonoBehaviour
{
    public AudioSource audioSource;
    public Text displayText;
    public AudioClip[] clips;
    public string[] texts;
    public float delayBetweenSounds = 2.0f;
   


    private int currentClipIndex = 0;


    void Start()
    {
        StartCoroutine(PlaySoundsSequentially());
    }
    IEnumerator PlaySoundsSequentially()
    {
        for (int i = 0; i < clips.Length && i < texts.Length; i++)
        {
            audioSource.clip = clips[i];
            displayText.text = texts[i];
            audioSource.Play();

            yield return new WaitForSeconds(audioSource.clip.length + delayBetweenSounds);

            currentClipIndex++;
        }

    }
}
