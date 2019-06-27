using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountRoundLevelUpBlock : Block {

    [SerializeField] Sprite[] sprites = new Sprite[37];

    int breakCount;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        spriteRenderer.sprite = sprites[breakCount];
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            if (breakCount > 1)
            {
                breakCount--;
                spriteRenderer.sprite = sprites[breakCount];
                mainManager.AddGameScore(1);
                audioSource.time = 0.025f;
                audioSource.Play();
            }
            else
            {
                DestroyBlock(1);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (breakCount > 1)
            {
                breakCount--;
                spriteRenderer.sprite = sprites[breakCount];
                mainManager.AddGameScore(1);
                audioSource.time = 0.025f;
                audioSource.Play();
            }
            else
            {
                DestroyBlock(0);
            }
        }
    }


    public void Initialize(int breakCount)
    {
        this.breakCount = breakCount;
    }
}
