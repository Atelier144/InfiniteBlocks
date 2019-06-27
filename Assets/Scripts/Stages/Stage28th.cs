using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage28th : Stage {

    [SerializeField] GameObject prefabNormalBlock;
    [SerializeField] GameObject prefabCeilingSystem;

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
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 100.0f - 450.0f;
                float positionY = y * 40.0f + 70.0f;
                int colorCode = Random.Range(0, 7);
                CreateNormalBlock(positionX, positionY, colorCode);
            }
        }
        CreateCeilingSystem();
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
        return false;
    }

    void CreateNormalBlock(float positionX, float positionY, int colorCode)
    {
        Instantiate(prefabNormalBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<NormalBlock>().Initialize(colorCode);
    }

    void CreateCeilingSystem()
    {
        Instantiate(prefabCeilingSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }
}
