using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage11th : Stage {

    [SerializeField] GameObject prefabNormalBlock;
    [SerializeField] GameObject prefabSixGatesSystem;
    [SerializeField] GameObject prefabCeilingSystem;

    SixGatesSystem sixGatesSystem;

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
        if (brokenBlocks >= 4) sixGatesSystem.OpenGate(0);
        if (brokenBlocks >= 10) sixGatesSystem.OpenGate(1);
        if (brokenBlocks >= 18) sixGatesSystem.OpenGate(2);
        if (brokenBlocks >= 28) sixGatesSystem.OpenGate(3);
        if (brokenBlocks >= 40) sixGatesSystem.OpenGate(4);
        if (brokenBlocks >= 54) sixGatesSystem.OpenGate(5);
    }

    public override void GenerateStage()
    {
        for (int y = 0; y < 7; y++)
        {
            for (int x = 0; x < 4 + y * 2; x++)
            {
                float positionX = (x - y) * 50.0f - 75.0f;
                float positionY = y * 60.0f + 0.0f;
                int colorCode = y;
                prefabCreator.CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        sixGatesSystem = prefabCreator.CreateSixGatesSystem();
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
