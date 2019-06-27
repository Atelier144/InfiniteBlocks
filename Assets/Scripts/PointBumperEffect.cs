using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBumperEffect : MonoBehaviour {

    [SerializeField] Sprite[] sprites = new Sprite[3];

    Animator animator;
    SpriteRenderer spriteRenderer;
 	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetEffect(int bumperCode)
    {
        spriteRenderer.sprite = sprites[bumperCode];
        animator.SetTrigger("Illuminate");
    }

    public void UnsetEffect()
    {
        spriteRenderer.sprite = null;
    }
}
