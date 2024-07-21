using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenAudioController : MonoBehaviour
{
    AudioSource[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();

        sounds[0].Play();
        Invoke("PlayVictoryLoop", sounds[0].clip.length);
    }
    
    private void PlayVictoryLoop()
    {
        sounds[1].Play();
    }
}
