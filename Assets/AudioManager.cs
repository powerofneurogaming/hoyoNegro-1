using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clips;

    private void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();

    }
    public void PlayClip(int index)
    {
        AudioClip clip = clips[index];
        audioSource.PlayOneShot(clip);
    }
}
