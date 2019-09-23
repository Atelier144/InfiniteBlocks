using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtelierHardBlock : Block {

    [SerializeField] GameObject gameObjectAtelierDamage;

    [SerializeField] Sprite[] spritesAtelierHardBlock = new Sprite[6];
    [SerializeField] Sprite[] spritesAtelierHardBlockDamaged = new Sprite[6];
    [SerializeField] Sprite[] spritesAtelierHardBlockFragile = new Sprite[6];
    [SerializeField] Sprite[] spritesAtelierDamage = new Sprite[6];

    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;
    AudioSource audioSource;

    AtelierDamage atelierDamage;

    Vector2[][] colliderPoints =
    {
        //A
        new Vector2[]{
            new Vector2(15.2f, -29.0f),
            new Vector2(24.6f, -29.0f),
            new Vector2(4.0f, 28.0f),
            new Vector2(-4.0f, 28.0f),
            new Vector2(-24.6f, -29.0f),
            new Vector2(-15.2f, -29.0f),
            new Vector2(-11.0f, -16.0f),
            new Vector2(11.0f, -16.0f)
        },
        //T
        new Vector2[]
        {
            new Vector2(20.0f, 28.0f),
            new Vector2(-20.0f, 28.0f),
            new Vector2(-20.0f, 20.0f),
            new Vector2(-5.0f, 20.0f),
            new Vector2(-5.0f, -29.0f),
            new Vector2(5.0f, -29.0f),
            new Vector2(5.0f, 20.0f),
            new Vector2(20.0f, 20.0f)
        },
        //E
        new Vector2[]
        {
            new Vector2(-17.0f, 28.0f),
            new Vector2(-17.0f, -29.0f),
            new Vector2(20.4f, -29.0f),
            new Vector2(20.4f, -20.0f),
            new Vector2(-7.0f, -20.0f),
            new Vector2(-7.0f, -4.0f),
            new Vector2(16.0f, -4.0f),
            new Vector2(16.0f, 4.0f),
            new Vector2(-7.0f, 4.0f),
            new Vector2(-7.0f, 20.0f),
            new Vector2(20.4f, 20.0f),
            new Vector2(20.4f, 28.0f)
        },
        //L
        new Vector2[]
        {
            new Vector2(-17.0f, 28.0f),
            new Vector2(-17.0f, -29.0f),
            new Vector2(20.4f, -29.0f),
            new Vector2(20.4f, -20.0f),
            new Vector2(-7.0f, -20.0f),
            new Vector2(-7.0f, 28.0f)
        },
        //I
        new Vector2[]
        {
            new Vector2(-5.0f, 28.0f),
            new Vector2(-5.0f, -29.0f),
            new Vector2(5.0f, -29.0f),
            new Vector2(5.0f, 28.0f)
        },
        //R
        new Vector2[]
        {
            new Vector2(-19.0f, 28.0f),
            new Vector2(-19.0f, -29.0f),
            new Vector2(-10.0f, -29.0f),
            new Vector2(-10.0f, -5.0f),
            new Vector2(2.0f, -5.0f),
            new Vector2(13.0f, -29.0f),
            new Vector2(24.0f, -29.0f),
            new Vector2(10.5f, -4.0f),
            new Vector2(17.3f, -0.2f),
            new Vector2(20.8f, 5.2f),
            new Vector2(22.0f, 11.5f),
            new Vector2(20.8f, 17.8f),
            new Vector2(17.3f, 23.2f),
            new Vector2(12.1f, 26.7f),
            new Vector2(6.0f, 28.0f)
        }
    };

    string[] triggerNames =
    {
        "AtelierA",
        "AtelierT",
        "AtelierE",
        "AtelierL",
        "AtelierI",
        "AtelierR"
    };

    int breakCount = 0;
    int characterCode;
    int blockStatus = 0;
    int duration;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        audioSource = GetComponent<AudioSource>();

        atelierDamage = gameObjectAtelierDamage.GetComponent<AtelierDamage>();

        duration = Random.Range(6, 10);

        spriteRenderer.sprite = spritesAtelierHardBlock[characterCode];
        polygonCollider2D.points = colliderPoints[characterCode];
        atelierDamage.SetSprite(spritesAtelierDamage[characterCode]);
        triggerName = triggerNames[characterCode];
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        polygonCollider2D.isTrigger = blockStatus == 2 && signalManager.IsActivePowerUp();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") Damage(1);
        if (collision.gameObject.tag == "PoweredBall") Damage(3);

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PoweredBall") DestroyBlock(0);
        if (collision.gameObject.tag == "Bullet") Damage(1);
    }

    void Damage(int damage)
    {
        gameObjectAtelierDamage.SetActive(false);
        gameObjectAtelierDamage.SetActive(true);
        mainManager.AddGameScore(damage);
        audioSource.Play();
        switch (blockStatus)
        {
            case 0:
                if (breakCount >= 5)
                {
                    blockStatus = 1;
                    spriteRenderer.sprite = spritesAtelierHardBlockDamaged[characterCode];
                }
                else
                {
                    breakCount += damage;
                }
                break;
            case 1:
                if (breakCount >= duration)
                {
                    blockStatus = 2;
                    spriteRenderer.sprite = spritesAtelierHardBlockFragile[characterCode];
                }
                else
                {
                    breakCount += damage;
                }
                break;
            case 2:
                base.DestroyBlock(0);
                break;
        }
    }

    public void Initialize(int characterCode)
    {
        this.characterCode = characterCode;
    }
}

//仕様
//最初は10回あてて最初のひびを入れる
//そのあとは1/10(Poweredでは1/3）の確率できついヒビを入れる
//いかなる場合でも30回以上当てれば次の段階
//きついヒビが入れば1回で破壊可能（Poweredなら貫通)
