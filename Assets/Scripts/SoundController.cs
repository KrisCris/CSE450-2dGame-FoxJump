using UnityEngine;

public class SoundController : MonoBehaviour {
    public static SoundController Instance;

    public AudioClip enemyCrushed;

    private void Awake() {
        Instance = this;
    }

    public void PlayEnemyCrushed() {
        PlaySound(enemyCrushed);
    }
    
    public void PlaySound(AudioClip sound) {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.Play();
        Destroy(audioSource, sound.length);
    }
}
