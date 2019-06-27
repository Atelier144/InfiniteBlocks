using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundBlock : Block {

    [SerializeField] Sprite[] spritesRoundBlock = new Sprite[7];
    [SerializeField] string[] triggerNames = new string[7];
    CircleCollider2D circleCollider2D;

    int colorCode;

    protected override void Start()
    {
        base.Start();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        circleCollider2D.isTrigger = signalManager.IsActivePowerUp();
    }

    public override void DestroyBlock(int comboBonus)
    {
        base.DestroyBlock(comboBonus);
    }

    public void Initialize(int colorCode)
    {
        this.colorCode = colorCode;
        triggerName = triggerNames[colorCode];
        this.GetComponent<SpriteRenderer>().sprite = spritesRoundBlock[colorCode];
    }
}
