using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour
{
    public AudioSource a_s;
    public static float musicVolume = 1;
    void Start()
    {
        a_s = GetComponent<AudioSource>();
    }
    void Update()
    {
        a_s.volume = musicVolume;
    }
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
