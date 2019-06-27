using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelBlocksWheelSystem : MonoBehaviour {

    SignalManager signalManager;

    [SerializeField] GameObject[] roundSteelBlocks = new GameObject[16];

    float[] roundSteelBlockDegrees = new float[16];
    int[] roundSteelBlockGroups = new int[16];

    float rotationSpeed;
    int numberOfBlocks;
	// Use this for initialization
	void Start () {
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (!signalManager.IsActiveTrapGuard())
        {
            for (int i = 0; i < 16; i++)
            {
                float basePositionX = 0.0f;
                float basePositionY = 140.0f;
                if (roundSteelBlockGroups[i] == 0)
                {
                    basePositionX = -250.0f;
                    roundSteelBlockDegrees[i] += rotationSpeed;
                }
                if (roundSteelBlockGroups[i] == 1)
                {
                    basePositionX = 250.0f;
                    roundSteelBlockDegrees[i] -= rotationSpeed;
                }
                while (roundSteelBlockDegrees[i] >= 360.0f) roundSteelBlockDegrees[i] -= 360.0f;
                while (roundSteelBlockDegrees[i] < 0.0f) roundSteelBlockDegrees[i] += 360.0f;

                float degree = roundSteelBlockDegrees[i] * Mathf.Deg2Rad;
                float positionX = Mathf.Cos(degree) * 200.0f + basePositionX;
                float positionY = Mathf.Sin(degree) * 200.0f + basePositionY;
                roundSteelBlocks[i].transform.position = new Vector3(positionX, positionY, 0.0f);
            }
        }
    }

    public void Initialize(int numberOfBlocks, float rotationSpeed)
    {
        SetNumberOfBlocks(numberOfBlocks);
        SetRotationSpeed(rotationSpeed);
    }

    public void SetNumberOfBlocks(int numberOfBlocks)
    {
        this.numberOfBlocks = numberOfBlocks;
        switch (numberOfBlocks)
        {
            case 8:
                roundSteelBlockDegrees[0] = 0.0f;
                roundSteelBlockDegrees[1] = 90.0f;
                roundSteelBlockDegrees[2] = 180.0f;
                roundSteelBlockDegrees[3] = 270.0f;
                roundSteelBlockDegrees[4] = 0.0f;
                roundSteelBlockDegrees[5] = 90.0f;
                roundSteelBlockDegrees[6] = 180.0f;
                roundSteelBlockDegrees[7] = 270.0f;

                roundSteelBlockGroups[0] = 0;
                roundSteelBlockGroups[1] = 0;
                roundSteelBlockGroups[2] = 0;
                roundSteelBlockGroups[3] = 0;
                roundSteelBlockGroups[4] = 1;
                roundSteelBlockGroups[5] = 1;
                roundSteelBlockGroups[6] = 1;
                roundSteelBlockGroups[7] = 1;

                for (int i = 0; i < 8; i++) roundSteelBlocks[i].SetActive(true);
                for (int i = 8; i < 16; i++) roundSteelBlocks[i].SetActive(false);
                break;

            case 12:
                roundSteelBlockDegrees[0] = 0.0f;
                roundSteelBlockDegrees[1] = 60.0f;
                roundSteelBlockDegrees[2] = 120.0f;
                roundSteelBlockDegrees[3] = 180.0f;
                roundSteelBlockDegrees[4] = 240.0f;
                roundSteelBlockDegrees[5] = 300.0f;
                roundSteelBlockDegrees[6] = 0.0f;
                roundSteelBlockDegrees[7] = 60.0f;
                roundSteelBlockDegrees[8] = 120.0f;
                roundSteelBlockDegrees[9] = 180.0f;
                roundSteelBlockDegrees[10] = 240.0f;
                roundSteelBlockDegrees[11] = 300.0f;

                roundSteelBlockGroups[0] = 0;
                roundSteelBlockGroups[1] = 0;
                roundSteelBlockGroups[2] = 0;
                roundSteelBlockGroups[3] = 0;
                roundSteelBlockGroups[4] = 0;
                roundSteelBlockGroups[5] = 0;
                roundSteelBlockGroups[6] = 1;
                roundSteelBlockGroups[7] = 1;
                roundSteelBlockGroups[8] = 1;
                roundSteelBlockGroups[9] = 1;
                roundSteelBlockGroups[10] = 1;
                roundSteelBlockGroups[11] = 1;

                for (int i = 0; i < 12; i++) roundSteelBlocks[i].SetActive(true);
                for (int i = 12; i < 16; i++) roundSteelBlocks[i].SetActive(false);
                break;

            case 16:
                roundSteelBlockDegrees[0] = 0.0f;
                roundSteelBlockDegrees[1] = 45.0f;
                roundSteelBlockDegrees[2] = 90.0f;
                roundSteelBlockDegrees[3] = 135.0f;
                roundSteelBlockDegrees[4] = 180.0f;
                roundSteelBlockDegrees[5] = 225.0f;
                roundSteelBlockDegrees[6] = 270.0f;
                roundSteelBlockDegrees[7] = 315.0f;
                roundSteelBlockDegrees[8] = 0.0f;
                roundSteelBlockDegrees[9] = 45.0f;
                roundSteelBlockDegrees[10] = 90.0f;
                roundSteelBlockDegrees[11] = 135.0f;
                roundSteelBlockDegrees[12] = 180.0f;
                roundSteelBlockDegrees[13] = 225.0f;
                roundSteelBlockDegrees[14] = 270.0f;
                roundSteelBlockDegrees[15] = 315.0f;

                roundSteelBlockGroups[0] = 0;
                roundSteelBlockGroups[1] = 0;
                roundSteelBlockGroups[2] = 0;
                roundSteelBlockGroups[3] = 0;
                roundSteelBlockGroups[4] = 0;
                roundSteelBlockGroups[5] = 0;
                roundSteelBlockGroups[6] = 0;
                roundSteelBlockGroups[7] = 0;
                roundSteelBlockGroups[8] = 1;
                roundSteelBlockGroups[9] = 1;
                roundSteelBlockGroups[10] = 1;
                roundSteelBlockGroups[11] = 1;
                roundSteelBlockGroups[12] = 1;
                roundSteelBlockGroups[13] = 1;
                roundSteelBlockGroups[14] = 1;
                roundSteelBlockGroups[15] = 1;

                for (int i = 0; i < 16; i++) roundSteelBlocks[i].SetActive(true);
                break;
        }
    }

    public void SetRotationSpeed(float rotationSpeed)
    {
        this.rotationSpeed = rotationSpeed;
    }
}
