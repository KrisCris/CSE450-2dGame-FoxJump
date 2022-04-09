using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundController : MonoBehaviour {
    public static SoundController Instance;

    private AudioSource _audioSource;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void PlaySound(AudioClip sound) {
        _audioSource.PlayOneShot(sound);
    }
}
