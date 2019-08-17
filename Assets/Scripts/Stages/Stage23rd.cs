using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage23rd : Stage {

    SteelBlocksWheelSystem steelBlocksWheelSystem;

    int[] numbersOfAppearItem = new int[4];
    int[] numbersOfAppearItemCode = { 3, 5, 14, 22 };

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
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
        int[] numbersMin = { 5, 13, 21, 29 };
        int[] numbersMax = { 7, 15, 23, 31 };
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
        for (int j = 0; j < 2; j++)
        {
            float positionXc = j * 500.0f - 250.0f;
            float positionYc = 140.0f;
            int[] colorCodes = { 0, 1, 2, 3, 4, 5, 6 };
            {
                int a = colorCodes.Length;
                while (a > 0)
                {
                    int s = a - 1;
                    int t = Random.Range(0, a);
                    int tmp = colorCodes[s];
                    colorCodes[s] = colorCodes[t];
                    colorCodes[t] = tmp;
                    a--;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                float radius = i * 36.0f * Mathf.Deg2Rad;
                float positionX = Mathf.Round(Mathf.Cos(radius) * 140.0f) + positionXc;
                float positionY = Mathf.Round(Mathf.Sin(radius) * 140.0f) + positionYc;
                prefabCreator.CreateHardRoundBlock(positionX, positionY);
            }
            for (int i = 0; i < 6; i++)
            {
                float radius = i * 60.0f * Mathf.Deg2Rad;
                float positionX = Mathf.Round(Mathf.Cos(radius) * 70.0f) + positionXc;
                float positionY = Mathf.Round(Mathf.Sin(radius) * 70.0f) + positionYc;
                int colorCode = colorCodes[i];
                prefabCreator.CreateCountRoundBlock(positionX, positionY, colorCode, 5);
            }
            prefabCreator.CreateCountRoundLevelUpBlock(positionXc, positionYc, 24);
        }
        prefabCreator.CreateSteelBlocksWheelSystem(12, -0.8f);
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        int resultItemCode = 0;
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) resultItemCode = numbersOfAppearItemCode[i];
        return resultItemCode;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
