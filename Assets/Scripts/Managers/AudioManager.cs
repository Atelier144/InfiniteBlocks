using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    [SerializeField] AudioClip[] audioClips;
    [SerializeField] float[] startTimes;

    AudioSource[] audioSources = new AudioSource[16];
	// Use this for initialization
	void Start () {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
            audioSources[i].playOnAwake = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(int index)
    {
        for (int i = 0; i < audioSources.Length; i++) 
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].clip = audioClips[index];
                audioSources[i].time = startTimes[index];
                audioSources[i].Play();
                break;
            }
        }
    }

    public void Mute()
    {
        for (int i = 0; i < audioSources.Length; i++) audioSources[i].mute = true;
    }

    public void Unmute()
    {
        for (int i = 0; i < audioSources.Length; i++) audioSources[i].mute = false;
    }
}
