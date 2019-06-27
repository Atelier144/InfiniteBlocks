using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtelierHardestBlock : Block {

    [SerializeField] GameObject gameObjectAtelierDamage;

    [SerializeField] Sprite[] spritesAtelierHardestBlock = new Sprite[2];
    [SerializeField] Sprite[] spritesAtelierHardestBlockDamaged = new Sprite[2];
    [SerializeField] Sprite[] spritesAtelierHardestBlockFragile = new Sprite[2];
    [SerializeField] Sprite[] spritesAtelierDamage = new Sprite[2];

    SpriteRenderer spriteRenderer;
    PolygonCollider2D polygonCollider2D;
    AudioSource audioSource;

    AtelierDamage atelierDamage;

    Vector2[][] colliderPoints =
    {
        //1
        new Vector2[]
        {
            new Vector2(-46.0f,   46.0f),
            new Vector2(-13.0f,   71.0f),
            new Vector2(-13.0f, -122.0f),
            new Vector2( 20.0f, -122.0f),
            new Vector2( 20.0f,  106.0f),
            new Vector2(-14.0f,  106.0f),
            new Vector2(-46.0f,   82.0f)
        },
        //4
        new Vector2[]
        {
            new Vector2(57.0f, -57.0f),
            new Vector2(57.0f, 8.0f),
            new Vector2(24.0f, 8.0f),
            new Vector2(24.0f, -57.0f),
            new Vector2(-35.0f, -57.0f),
            new Vector2(42.0f, 106.0f),
            new Vector2(5.0f, 106.0f),
            new Vector2(-71.0f, -57.0f),
            new Vector2(-71.0f, -88.0f),
            new Vector2(24.0f, -88.0f),
            new Vector2(24.0f, -122.0f),
            new Vector2(57.0f, -122.0f),
            new Vector2(57.0f, -88.0f),
            new Vector2(75.0f, -88.0f),
            new Vector2(75.0f, -57.0f)
        }
    };

    string[] triggerNames =
    {
        "Atelier1",
        "Atelier4"
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

        duration = Random.Range(29, 40);

        spriteRenderer.sprite = spritesAtelierHardestBlock[characterCode];
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
                if (breakCount >= 15)
                {
                    blockStatus = 1;
                    spriteRenderer.sprite = spritesAtelierHardestBlockDamaged[characterCode];
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
                    spriteRenderer.sprite = spritesAtelierHardestBlockFragile[characterCode];
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
