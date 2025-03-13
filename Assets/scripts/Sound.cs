using UnityEngine;

[System.Serializable]
public class Sound{
    
    public AudioSource source;
    
    public string name;

    [Tooltip("All the variations of sound that this sound is ")]
    public AudioClip[] clips;

}
