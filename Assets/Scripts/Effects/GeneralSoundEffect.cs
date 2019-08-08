using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSoundEffect : MonoBehaviour
{
    [SerializeField] float startTime;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.time = startTime;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying) Destroy(gameObject);
    }
}
