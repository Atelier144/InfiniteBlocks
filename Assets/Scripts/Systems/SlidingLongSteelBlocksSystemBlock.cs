using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingLongSteelBlocksSystemBlock : MonoBehaviour {

    SignalManager signalManager;

    [SerializeField] float basePositionX;
    [SerializeField] float basePositionY;
    [SerializeField] int startCount;

    int count;
	// Use this for initialization
	void Start () {
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        count = startCount;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (!signalManager.IsActiveTrapGuard())
        {
            count++;
            if (count >= 320) count = 0;
            float positionX = count < 160 ? count * 2.0f + basePositionX : count * -2.0f + 640.0f + basePositionX;
            float positionY = basePositionY;
            float positionZ = 0.0f;
            transform.position = new Vector3(positionX, positionY, positionZ);
        }
    }
}
