using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAudio : MonoBehaviour {

    public AudioClip winAudioSample;
    AudioSource audio;
    public float Volume;
    public bool alreadyPlayed = false;

	
	void Start () {

        audio = GetComponent<AudioSource>();
        
	}
	
	void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !alreadyPlayed)
        {
            audio.PlayOneShot(winAudioSample, Volume);
            alreadyPlayed = true;
        }
    }
}
