using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemFace : MonoBehaviour {

    [SerializeField] Sprite[] spritesFace = new Sprite[14];

    SpriteRenderer spriteRenderer;

    int countDamageFace;

    int faceCode;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        countDamageFace--;
        if(countDamageFace == 1)
        {
            countDamageFace = 0;
            DrawFace();
        }
    }
    public void DamageFace()
    {
        countDamageFace = 10;
        DrawFace();
    }

    public void ChangeFace(int faceCode)
    {
        this.faceCode = faceCode;
        DrawFace();
    }

    void DrawFace()
    {
        if (countDamageFace > 0) spriteRenderer.sprite = spritesFace[5];
        else spriteRenderer.sprite = spritesFace[faceCode];
    }
}
