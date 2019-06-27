using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage23rd : Stage {

    [SerializeField] GameObject prefabHardRoundBlock;
    [SerializeField] GameObject prefabCountRoundBlock;
    [SerializeField] GameObject prefabCountRoundLevelUpBlock;
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
            CreateHardRoundBlock(positionX1, positionY);
            CreateHardRoundBlock(positionX2, positionY);

        }
        for (int i = 0; i < 6; i++)
        {
            float radius = i * 60.0f * Mathf.Deg2Rad;
            float positionX1 = Mathf.Round(Mathf.Cos(radius) * 70.0f) - 250.0f;
            float positionX2 = Mathf.Round(Mathf.Cos(radius) * 70.0f) + 250.0f;
            float positionY = Mathf.Round(Mathf.Sin(radius) * 70.0f) + 140.0f;
            int colorCode1 = Random.Range(0, 7);
            int colorCode2 = Random.Range(0, 7);
            CreateCountRoundBlock(positionX1, positionY, colorCode1, 5);
            CreateCountRoundBlock(positionX2, positionY, colorCode2, 5);
        }
        CreateCountRoundLevelUpBlock(250.0f, 140.0f, 24);
        CreateCountRoundLevelUpBlock(-250.0f, 140.0f, 24);
        CreateSteelBlocksWheelSystem(12, -0.8f);
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

    void CreateHardRoundBlock(float positionX, float positionY)
    {
        Instantiate(prefabHardRoundBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateCountRoundBlock(float positionX, float positionY, int colorCode, int breakCount)
    {
        Instantiate(prefabCountRoundBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<CountBlock>().Initialize(colorCode, breakCount);
    }

    void CreateCountRoundLevelUpBlock(float positionX, float positionY, int breakCount)
    {
        Instantiate(prefabCountRoundLevelUpBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<CountRoundLevelUpBlock>().Initialize(breakCount);
    }

    void CreateSteelBlocksWheelSystem(int numberOfBlocks, float rotationSpeed)
    {
        steelBlocksWheelSystem = Instantiate(prefabSteelBlocksWheelSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<SteelBlocksWheelSystem>();
        steelBlocksWheelSystem.Initialize(numberOfBlocks, rotationSpeed);
    }

    void CreateCeilingSystem()
    {
        Instantiate(prefabCeilingSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }
}
