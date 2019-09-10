using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage28th : Stage {

    [SerializeField] GameObject prefabNormalBlock;
    [SerializeField] GameObject prefabCeilingSystem;

    int[] numbersOfAppearItemCode = { 5, 6, 8, 9, 11, 13, 14, 19, 20 };

    int brokenBlocks;

    SKL144Sytem skl144system;

    protected override void Start()
    {
        base.Start();
        if (true)
        {
            int a = numbersOfAppearItemCode.Length;
            while (a > 0)
            {
                int i = a - 1;
                int j = Random.Range(0, a);
                int tmp = numbersOfAppearItemCode[i];
                numbersOfAppearItemCode[i] = numbersOfAppearItemCode[j];
                numbersOfAppearItemCode[j] = tmp;
                a--;
            }
        }
        brokenBlocks = 0;
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
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 100.0f - 450.0f;
                float positionY = y * 40.0f + 70.0f;
                int colorCode = Random.Range(0, 7);
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        skl144system = prefabCreator.CreateSKL144System();
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        if (brokenBlocks % 10 == 0)
        {
            int index = brokenBlocks / 10 - 1;
            int retval = numbersOfAppearItemCode[index];
            if(brokenBlocks >= 90)
            {
                brokenBlocks = 0;
                if (true)
                {
                    int a = numbersOfAppearItemCode.Length;
                    while (a > 0)
                    {
                        int i = a - 1;
                        int j = Random.Range(0, a);
                        int tmp = numbersOfAppearItemCode[i];
                        numbersOfAppearItemCode[i] = numbersOfAppearItemCode[j];
                        numbersOfAppearItemCode[j] = tmp;
                        a--;
                    }
                }
            }
            return retval;
        }
        return 0;
    }

    public override bool IsLevelUp()
    {
        return skl144system.IsLevelUp();
    }
}
