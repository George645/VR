using System;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour{
    [HideInInspector]
    public int volume;

    [SerializeField]
    Sound[] sounds;

    public static AudioManager audioManager;

    void Start(){

        if (audioManager == null) {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.volume = volume;
        }
    }

    public void Play(string name) {
        try {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.clip = s.clips[UnityEngine.Random.Range(0, s.clips.Length)];
            s.source.Play();
        }
        catch {
            Debug.LogWarning(name + "could not be found to be played as audio", this);
        }
    }
}
