using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2nd : Stage {

    [SerializeField] GameObject prefabHardBlock;
    [SerializeField] GameObject prefabItemBlock;
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
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                float positionX = x * 50.0f - 425.0f;
                float positionY = y * 20.0f + 200.0f;
                CreateHardBlock(positionX, positionY);
            }
        }
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                float positionX = x * 50.0f - 275.0f;
                float positionY = y * 20.0f + 180.0f;
                CreateHardBlock(positionX, positionY);
            }
        }

        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 50.0f - 125.0f;
                float positionY = y * 20.0f + 160.0f;
                if ((x == 1 || x == 4) && y == 1) CreateItemBlock(positionX, positionY, 8); // 8:itemCode of PowerUp
                else CreateHardBlock(positionX, positionY);

            }
        }
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                float positionX = x * 50.0f - 125.0f;
                float positionY = y * 20.0f + 260.0f;
                if ((x == 1 || x == 4) && y == 1) CreateItemBlock(positionX, positionY, 9); // 9:itemCode of Protector
                else CreateHardBlock(positionX, positionY);
            }
        }
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                float positionX = x * 50.0f + 225.0f;
                float positionY = y * 20.0f + 180.0f;
                CreateHardBlock(positionX, positionY);
            }
        }
        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                float positionX = x * 50.0f + 375.0f;
                float positionY = y * 20.0f + 200.0f;
                CreateHardBlock(positionX, positionY);
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
        return GameObject.FindGameObjectsWithTag("Block").Length == 0;
    }

    void CreateHardBlock(float positionX, float positionY)
    {
        Instantiate(prefabHardBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateItemBlock(float positionX, float positionY, int itemCode)
    {
        Instantiate(prefabItemBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<ItemBlock>().Initialize(itemCode);
    }

    void CreateCeilingSystem()
    {
        Instantiate(prefabCeilingSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

}
