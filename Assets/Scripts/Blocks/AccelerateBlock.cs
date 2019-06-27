using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerateBlock : Block {

    Ball ball;

    BoxCollider2D boxCollider2D;

    protected override void Start()
    {
        base.Start();
        ball = GameObject.Find("TheBall").GetComponent<Ball>();

        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        boxCollider2D.isTrigger = signalManager.IsActivePowerUp();
    }

    public override void DestroyBlock(int comboBonus)
    {
        base.DestroyBlock(comboBonus);
        if (!signalManager.IsActiveTrapGuard()) ball.PrepareAccelerate();
    }
}
