using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentBlock : Block {

    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;
    float alpha = 1.0f;



    protected override void Start()
    {
        base.Start();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        Color color = new Color(1.0f, 1.0f, 1.0f, alpha);
        spriteRenderer.color = color;

    }

    protected override void FixedUpdate()
    {
        boxCollider2D.isTrigger = signalManager.IsActivePowerUp();

        if (signalManager.IsActiveTrapGuard() || mainManager.GetDialogStatus() != 2) alpha += 0.02f;
        else alpha -= 0.01f;

        if (alpha < 0.0f) alpha = 0.0f;
        if (alpha > 1.0f) alpha = 1.0f;
    }
}
