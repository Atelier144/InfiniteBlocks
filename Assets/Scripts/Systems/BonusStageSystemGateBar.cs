using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusStageSystemGateBar : MonoBehaviour {

    [SerializeField] int directionCode;
    AudioSource audioSource;
    float movingSpeed = 0.0f;
    int movingCount = 0;
    // Use this for initialization
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (movingCount > 0)
        {
            movingCount--;
            this.transform.position += new Vector3(movingSpeed, 0.0f, 0.0f);
        }
    }

    public void Open()
    {
        movingCount = 40;
        switch (directionCode)
        {
            case 0:
                movingSpeed = -15.0f;
                break;
            case 1:
                movingSpeed = 15.0f;
                break;
        }
        audioSource.Play();
    }
}
