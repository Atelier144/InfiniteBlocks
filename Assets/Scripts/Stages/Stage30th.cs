using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage30th : Stage {

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
        for (int y = 0; y < 3; y++)
        {
            int[] colorCodes = { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6 };
            int a = colorCodes.Length;
            while (a > 0)
            {
                int i = a - 1;
                int j = Random.Range(0, a);
                int tmp = colorCodes[i];
                colorCodes[i] = colorCodes[j];
                colorCodes[j] = tmp;
                a--;
            }
            for (int x = 0; x < 20; x++)
            {
                float positionX = x * 50.0f - 475.0f;
                float positionY = y * 20.0f + 162.0f;
                int colorCode = colorCodes[x];
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        prefabCreator.CreateInfiniteBlocksSystem();
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        return itemCode;
    }

    public override bool IsLevelUp()
    {
        return false;
    }
}
