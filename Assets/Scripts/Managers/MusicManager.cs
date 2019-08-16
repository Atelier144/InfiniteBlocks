using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    AudioSource audioSource;

    [SerializeField] AudioClip[] audioClips = new AudioClip[8];
    [SerializeField] float[] volumeBalancers = new float[8];
    [SerializeField] float[] loopStart = new float[8];
    [SerializeField] float[] loopEnd = new float[8];

    [SerializeField] Text textMusic;

    [SerializeField] bool isMusicTest;

    int currentMusicChannel = 0;
	// Use this for initialization
	void Start () {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.mute = false;
	}
	
	// Update is called once per frame
	void Update () {
        textMusic.text = "MUSIC TIME = " + audioSource.time;
    }

    private void FixedUpdate()
    {
        if (audioSource.time >= loopEnd[currentMusicChannel]) audioSource.time = loopStart[currentMusicChannel];
        if(isMusicTest && audioSource.time >= loopStart[currentMusicChannel] + 5.0f && audioSource.time <= loopStart[currentMusicChannel] + 6.0f)
        {
            audioSource.time = loopEnd[currentMusicChannel] - 5.0f;
        }

    }
    public void MuteMusic()
    {
        audioSource.mute = true;
    }

    public void HaltMusic()
    {
        audioSource.mute = true;
    }

    public void ResumeMusic()
    {
        audioSource.mute = false;
    }

    public void ChangeMusic(int channel)
    {
        int previousMusicChannel = currentMusicChannel;
        currentMusicChannel = channel;
        audioSource.clip = audioClips[currentMusicChannel];
        audioSource.volume = volumeBalancers[currentMusicChannel];
        if (previousMusicChannel != currentMusicChannel)
        {
            audioSource.time = 0.0f;
            audioSource.Play();
        }

    }
}
