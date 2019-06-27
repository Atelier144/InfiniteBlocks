using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSliderManager : MonoBehaviour {

    int frameCount;

	// Use this for initialization
	void Start () {
        frameCount = Random.Range(0, 40) * 20;
	}
	
	// Update is called once per frame
	void Update () {


    }

    private void FixedUpdate()
    {
        frameCount++;
        if (frameCount > 800)
        {
            frameCount = 0;
        }
        float positionX = FunctionPositionX(frameCount);
        this.gameObject.transform.localPosition = new Vector3(positionX, 0.0f, 0.0f);
    }

    float FunctionPositionX(int f)
    {
        if(f < 400)
        {
            return 2.375f * f - 475.0f;
        }
        else
        {
            return -2.375f * f + 1425.0f;
        }
    }
}
