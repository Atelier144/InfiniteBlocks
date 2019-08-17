using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage14th : Stage {

    SteelBlocksWheelSystem steelBlocksWheelSystem;

    int[] numbersOfAppearItem = new int[5];
    int[] numbersOfAppearItemCode = { 3, 5, 8, 9, 11, 14, 101 };

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
        int[] numbersMin = { 3, 9, 15, 21, 25 };
        int[] numbersMax = { 6, 12, 18, 24, 27 };
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
        /*
        for (int i = 0; i < 10; i++)
        {
            int[][] colorCodes =
            {

                new int[]{0,0,1,1,2,2,3,3,4,4,5,5,6,6},
                new int[]{0,0,1,1,2,2,3,3,4,4,5,5,6,6}

            };
            for (int j = 0; j < 2; j++)
            {
                int a = colorCodes[j].Length;
                while (a > 0)
                {
                    int b = a - 1;
                    int c = Random.Range(0, a);
                    int tmp = colorCodes[j][b];
                    colorCodes[j][b] = colorCodes[j][c];
                    colorCodes[j][c] = tmp;
                    a--;
                }
            }
            float radius = i * 36.0f * Mathf.Deg2Rad;
            float positionX1 = Mathf.Round(Mathf.Cos(radius) * 140.0f) - 250.0f;
            float positionX2 = Mathf.Round(Mathf.Cos(radius) * 140.0f) + 250.0f;
            float positionY = Mathf.Round(Mathf.Sin(radius) * 140.0f) + 140.0f;
            int colorCode1 = colorCodes[0][i];
            int colorCode2 = colorCodes[1][i];
            prefabCreator.CreateRoundBlock(positionX1, positionY, colorCode1);
            prefabCreator.CreateRoundBlock(positionX2, positionY, colorCode2);

        }
        */      
        for (int i = 0; i < 2; i++)
        {
            int[] colorCodes = { 0, 1, 2, 3, 4, 5, 6, 0, 1, 2 };
            int[] colorDice = { 0, 1, 2, 3, 4, 5, 6 };
            {
                int a = colorDice.Length;
                while (a > 0)
                {
                    int s = a - 1;
                    int t = Random.Range(0, a);
                    int tmp = colorDice[s];
                    colorDice[s] = colorDice[t];
                    colorDice[t] = tmp;
                    a--;
                }
            }
            colorCodes[7] = colorDice[0];
            colorCodes[8] = colorDice[1];
            colorCodes[9] = colorDice[2];
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
            for (int j = 0; j < 10; j++)
            {
                float radius = j * 36.0f * Mathf.Deg2Rad;
                float positionX = Mathf.Round(Mathf.Cos(radius) * 140.0f) + i * 500.0f - 250.0f;
                float positionY = Mathf.Round(Mathf.Sin(radius) * 140.0f) + 140.0f;
                int colorCode = colorCodes[j];
                prefabCreator.CreateRoundBlock(positionX, positionY, colorCode);
            }
        }
        for (int i = 0; i < 6; i++)
        {
            float radius = i * 60.0f * Mathf.Deg2Rad;
            float positionX1 = Mathf.Round(Mathf.Cos(radius) * 70.0f) - 250.0f;
            float positionX2 = Mathf.Round(Mathf.Cos(radius) * 70.0f) + 250.0f;
            float positionY = Mathf.Round(Mathf.Sin(radius) * 70.0f) + 140.0f;
            prefabCreator.CreateHardRoundBlock(positionX1, positionY);
            prefabCreator.CreateHardRoundBlock(positionX2, positionY);
        }
        prefabCreator.CreatePointBumper(0.0f, 320.0f, 0);
        prefabCreator.CreatePointBumper(250.0f, 140.0f, 2);
        prefabCreator.CreatePointBumper(-250.0f, 140.0f, 2);
        prefabCreator.CreateSteelBlocksWheelSystem(8, 0.4f);
        prefabCreator.CreateCeilingSystem();
    }

    public override int GenerateItemCode(int itemCode)
    {
        int resultItemCode = 0;
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) resultItemCode = numbersOfAppearItemCode[i];
        if (resultItemCode == 101) resultItemCode = Random.Range(0, 2) == 0 ? 19 : 20;
        return resultItemCode;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
