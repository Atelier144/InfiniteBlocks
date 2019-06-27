using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSlider : MonoBehaviour {

    [SerializeField] GameObject prefabCountBlock;

    int frameCount = 0;

	// Use this for initialization
	void Start () {
        frameCount = Random.Range(0, 15) * 15;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if(this.transform.childCount > 0)
        {
            frameCount++;
            if (frameCount > 450)
            {
                frameCount = 0;
            }

            float positionX;
            float positionY = this.transform.position.y;
            if (frameCount < 225)
            {
                positionX = 4.0f * frameCount - 450.0f;
            }
            else
            {
                positionX = -4.0f * frameCount + 1350.0f;
            }
            this.transform.GetChild(0).transform.position = new Vector3(positionX, positionY, 0.0f);
        }
    }

    public void CreateCountBlock(int colorCode, int breakCount)
    {
        GameObject countBlock = Instantiate(prefabCountBlock, this.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        countBlock.GetComponent<CountBlock>().Initialize(colorCode, breakCount);
        countBlock.transform.parent = this.gameObject.transform;
    }
}
