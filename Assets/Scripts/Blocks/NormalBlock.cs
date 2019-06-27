using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlock : Block {

    [SerializeField] Sprite[] spritesNormalBlock = new Sprite[8];

    BoxCollider2D boxCollider2D;

    [SerializeField] string[] triggerNames = new string[8];
    int colorCode;

    protected override void Start()
    {
        base.Start();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        boxCollider2D.isTrigger = signalManager.IsActivePowerUp();
    }

    public void Initialize(int colorCode)
    {
        this.colorCode = colorCode;
        this.GetComponent<SpriteRenderer>().sprite = spritesNormalBlock[colorCode];
        triggerName = triggerNames[colorCode];
    }
}
