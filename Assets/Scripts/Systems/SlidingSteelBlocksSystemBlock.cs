using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingSteelBlocksSystemBlock : MonoBehaviour {

    [SerializeField] int positionX;
    [SerializeField] int positionY;
    [SerializeField] int directionCode;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        switch (directionCode)
        {
            case 0:
                positionX++;
                if (positionX >= 630) positionX = -630;
                break;
            case 1:
                positionX--;
                if (positionX <= -630) positionX = 630;
                break;
        }
        this.transform.position = new Vector3(positionX, positionY, 0.0f);
    }
}
