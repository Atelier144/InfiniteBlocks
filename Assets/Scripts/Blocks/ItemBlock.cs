using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : Block {

    [SerializeField] Sprite[] sprites = new Sprite[30];
    [SerializeField] string[] triggerNames = new string[30];

    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprites[itemCode];
        triggerName = triggerNames[itemCode];
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        boxCollider2D.isTrigger = signalManager.IsActivePowerUp();
    }

    public void Initialize(int itemCode)
    {
        this.itemCode = itemCode;
    }
}
