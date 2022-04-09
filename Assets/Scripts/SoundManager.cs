using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance;

    private AudioSource _audioSource;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(AudioClip sound) {
        _audioSource.PlayOneShot(sound);
    }
}
