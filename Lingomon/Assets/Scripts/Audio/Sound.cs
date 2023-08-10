using UnityEngine.Audio;
using UnityEngine;

// this file adjusts different aspects of the sounds.
// credit: Brackeys
[System.Serializable]
public class Sound
{
    // init name and clip
    public string name;
    public AudioClip clip;

    // adjust the volume
    [Range(0f, 1f)]
    public float volume = .5f;
    [Range(0f, 1f)]
    public float volumeVariance = .1f;

    // adjust the pitch
    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float pitchVariance = .1f;

    // loop 
    public bool loop = false;

    // hide the audiosource in the inspector
    [HideInInspector]
    public AudioSource source;

}
