using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage14th : Stage {

    SteelBlocksWheelSystem steelBlocksWheelSystem;

    int[] numbersOfAppearItem = new int[5];
    int[] numbersOfAppearItemCode = { 4, 5, 8, 9, 11, 14, 101 };

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
        for (int i = 0; i < 10; i++)
        {
            float radius = i * 36.0f * Mathf.Deg2Rad;
            float positionX1 = Mathf.Round(Mathf.Cos(radius) * 140.0f) - 250.0f;
            float positionX2 = Mathf.Round(Mathf.Cos(radius) * 140.0f) + 250.0f;
            float positionY = Mathf.Round(Mathf.Sin(radius) * 140.0f) + 140.0f;
            int colorCode1 = Random.Range(0, 7);
            int colorCode2 = Random.Range(0, 7);
            prefabCreator.CreateRoundBlock(positionX1, positionY, colorCode1);
            prefabCreator.CreateRoundBlock(positionX2, positionY, colorCode2);

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
