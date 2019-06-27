using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage30th : Stage {

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void GenerateStage()
    {
        for (int x = 0; x < 20; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 50.0f - 475.0f;
                float positionY = y * 20.0f + 162.0f;
                int colorCode = Random.Range(0, 7);
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        return 0;
    }

    public override bool IsLevelUp()
    {
        return false;
    }
}
