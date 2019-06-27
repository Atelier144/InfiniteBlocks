using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSystemDrum : MonoBehaviour {

    [SerializeField] Sprite[] sprites = new Sprite[18];
    [SerializeField] int initialDrumPosition;

    SpriteRenderer spriteRenderer;
    int drumPosition;

    bool isRotating;


	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

        drumPosition = initialDrumPosition;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isRotating)
        {
            drumPosition++;
            if (drumPosition >= 18) drumPosition = 0;
            spriteRenderer.sprite = sprites[drumPosition];
        }
    }

    public void StartDrum()
    {
        isRotating = true;
    }

    public void StopDrum(int numberPosition)
    {
        isRotating = false;
        drumPosition = numberPosition * 2;
        spriteRenderer.sprite = sprites[drumPosition];
    }

    public int GetNumberPosition()
    {
        return drumPosition / 2;
    }

    public bool IsRotating()
    {
        return isRotating;
    }
}
