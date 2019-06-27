using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundNames
{
    Miss,
    GetPoint
}

public class SoundPlayer : MonoBehaviour {

    public AudioClip audioClipSilent;
    public AudioClip audioClipMiss;
    public AudioClip audioClipGetPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaySound(SoundNames soundName)
    {
        AudioClip audioClip = audioClipSilent;
        switch (soundName)
        {
            case SoundNames.Miss:
                audioClip = audioClipMiss;
                break;
        }
        this.GetComponent<AudioSource>().PlayOneShot(audioClip);
    }

    public void SetAudioSourceVolume(bool s)
    {
        if (s)
        {
            this.GetComponent<AudioSource>().volume = 1.0f;
        }
        else
        {
            this.GetComponent<AudioSource>().volume = 0.0f;
        }
    }
}
