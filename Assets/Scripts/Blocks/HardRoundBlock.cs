using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardRoundBlock : Block {

    [SerializeField] Sprite[] sprites = new Sprite[3];

    SpriteRenderer spriteRenderer;
    CircleCollider2D circleCollider2D;
    AudioSource audioSource;

    int breakCount = 2;

    protected override void Start()
    {
        base.Start();

        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        circleCollider2D.isTrigger = breakCount == 0 && signalManager.IsActivePowerUp();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (breakCount > 0)
            {
                breakCount--;
                spriteRenderer.sprite = sprites[breakCount];
                audioSource.Play();
                mainManager.AddGameScore(1);
            }
            else
            {
                DestroyBlock(1);
            }
        }
        if (collision.gameObject.tag == "PoweredBall")
        {
            DestroyBlock(1);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PoweredBall")
        {
            DestroyBlock(0);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            if (breakCount > 0)
            {
                breakCount--;
                spriteRenderer.sprite = sprites[breakCount];
                audioSource.Play();
                mainManager.AddGameScore(1);
            }
            else
            {
                DestroyBlock(0);
            }
        }
    }
}
