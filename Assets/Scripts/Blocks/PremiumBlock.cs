using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PremiumBlock : Block {

    BoxCollider2D boxCollider2D;

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
}
