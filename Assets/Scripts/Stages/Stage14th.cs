using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage14th : Stage {

    [SerializeField] GameObject prefabRoundBlock;
    [SerializeField] GameObject prefabHardRoundBlock;
    [SerializeField] GameObject prefabPointBumper;
    [SerializeField] GameObject prefabSteelBlocksWheelSystem;
    [SerializeField] GameObject prefabCeilingSystem;

    SteelBlocksWheelSystem steelBlocksWheelSystem;

    int[] numbersOfAppearItem = { };
    int[] numbersOfAppearItemCode = { };

    int brokenBlocks;

    protected override void Start()
    {
        base.Start();
        int[] numbersMin = { };
        int[] numbersMax = { };
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
        brokenBlocks++;
        if (itemCode != 0) return itemCode;
        for (int i = 0; i < numbersOfAppearItem.Length; i++) if (numbersOfAppearItem[i] == brokenBlocks) return numbersOfAppearItemCode[i];
        return 0;
    }

    public override bool IsLevelUp()
    {
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }
}
