using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakEffect : MonoBehaviour {

    [SerializeField] Sprite[] sprites;

    Animator animator;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;

    string triggerName;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator.SetTrigger(triggerName);
        audioSource.time = 0.1f;
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(string triggerName)
    {
        this.triggerName = triggerName;
    }

    public void ChangeSprite(int s)
    {
        spriteRenderer.sprite = sprites[s];
    }

    public void Diminish()
    {
        Destroy(gameObject);
    }
}
