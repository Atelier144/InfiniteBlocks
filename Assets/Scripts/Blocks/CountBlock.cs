using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBlock : Block {

    [SerializeField] Sprite[] spritesCountBlock = new Sprite[71];
    [SerializeField] string[] triggerNames = new string[7];

    int colorCode;
    int breakCount;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            if (breakCount > 1)
            {
                breakCount--;
                int spriteIndex = colorCode * 10 + breakCount;
                spriteRenderer.sprite = spritesCountBlock[spriteIndex];
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
                int spriteIndex = colorCode * 10 + breakCount;
                spriteRenderer.sprite = spritesCountBlock[spriteIndex];
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


    public void Initialize(int colorCode, int breakCount)
    {
        this.colorCode = colorCode;
        this.breakCount = breakCount;
        triggerName = triggerNames[colorCode];

        int spriteIndex = colorCode * 10 + breakCount;
        GetComponent<SpriteRenderer>().sprite = spritesCountBlock[spriteIndex];
    }
}
