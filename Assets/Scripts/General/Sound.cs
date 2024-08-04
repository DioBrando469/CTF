using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;
    public string name;
    public AudioClip clip;
    [Range(0f, 10f)]
    public float volume;
    public float pitch;
}
