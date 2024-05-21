using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;
    AudioSource audioSource;

    private void Awake () {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }   
    public void setSound (AudioClip ac) {
        audioSource.PlayOneShot(ac);
        audioSource.volume = 1.0f;
    }

    

    public void setSoundDifferentVolume (AudioClip ac, float volume) {
        audioSource.PlayOneShot(ac);
        
        audioSource.volume = volume;
    }




}
