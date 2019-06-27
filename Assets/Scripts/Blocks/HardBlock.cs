using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardBlock : Block {

    [SerializeField] Sprite[] spritesHardBlock = new Sprite[3];

    BoxCollider2D boxCollider2D;

    int breakCount = 2;

    protected override void Start()
    {
        base.Start();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        boxCollider2D.isTrigger = breakCount == 0 && signalManager.IsActivePowerUp();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            if(breakCount > 0)
            {
                breakCount--;
                this.GetComponent<SpriteRenderer>().sprite = spritesHardBlock[breakCount];
                this.GetComponent<AudioSource>().Play();
                base.mainManager.AddGameScore(1);
            }
            else
            {
                DestroyBlock(1);
            }
        }
        if(collision.gameObject.tag == "PoweredBall")
        {
            DestroyBlock(1);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PoweredBall")
        {
            DestroyBlock(0);
        }
        if(collision.gameObject.tag == "Bullet")
        {
            if (breakCount > 0)
            {
                breakCount--;
                this.GetComponent<SpriteRenderer>().sprite = spritesHardBlock[breakCount];
                this.GetComponent<AudioSource>().Play();
                base.mainManager.AddGameScore(1);
            }
            else
            {
                DestroyBlock(0);
            }
        }
    }
}
