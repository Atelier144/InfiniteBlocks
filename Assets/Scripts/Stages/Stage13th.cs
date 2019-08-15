using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage13th : Stage {

    int[] numbersOfAppearItem = new int[7];
    int[] numbersOfAppearItemCode = { 2, 3, 5, 8, 9, 101, 102 };

    int brokenBlocks;

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
        numbersOfAppearItemCode[4] = 21;
        numbersOfAppearItemCode[6] = 7;
        int[] numbersMin = { 5, 20, 35, 50, 65, 80, 85 };
        int[] numbersMax = { 10, 25, 40, 55, 70, 85, 87 };
        for (int i = 0; i < numbersOfAppearItem.Length; i++) numbersOfAppearItem[i] = Random.Range(numbersMin[i], numbersMax[i]);

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
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 9; y++)
            {
                int[,] pattern =
                {
                    {1,1,1,1,1,1,1},
                    {1,0,0,0,0,0,1},
                    {1,0,2,2,2,0,1},
                    {1,1,2,0,2,1,1},
                    {1,0,2,3,2,0,1},
                    {1,1,2,0,2,1,1},
                    {1,0,2,2,2,0,1},
                    {1,0,0,0,0,0,1},
                    {1,1,1,1,1,1,1}
                };
                float positionX1 = x * 50.0f - 400.0f;
                float positionX2 = x * 50.0f + 100.0f;
                float positionY = y * 20.0f + 120.0f;
                int colorCode1 = 6 - x;
                int colorCode2 = x;
                switch (pattern[y, x])
                {
                    case 1:
                        prefabCreator.CreateNormalBlock(positionX1, positionY, colorCode1);
                        prefabCreator.CreateNormalBlock(positionX2, positionY, colorCode2);
                        break;
                    case 2:
                        prefabCreator.CreateCountBlock(positionX1, positionY, colorCode1, 5);
                        prefabCreator.CreateCountBlock(positionX2, positionY, colorCode2, 5);
                        break;
                    case 3:
                        prefabCreator.CreateCountLevelUpBlock(positionX1, positionY, 16);
                        prefabCreator.CreateCountLevelUpBlock(positionX2, positionY, 16);
                        break;
                }

            }
        }
        prefabCreator.CreatePointBumper(0.0f, 200.0f, 0);
        prefabCreator.CreateSlidingSteelBlocksSystem();
        prefabCreator.CreateVerticalLooperSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        int resultItemCode = 0;
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) resultItemCode = numbersOfAppearItemCode[i];
        if (resultItemCode == 101) resultItemCode = Random.Range(0, 2) == 0 ? 13 : 14;
        if (resultItemCode == 102) resultItemCode = Random.Range(0, 2) == 0 ? 19 : 20;
        return resultItemCode;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
