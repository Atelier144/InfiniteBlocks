using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCreator : MonoBehaviour {

    [SerializeField] GameObject prefabNormalBlock;
    [SerializeField] GameObject prefabHardBlock;
    [SerializeField] GameObject prefabSmallBlock;
    [SerializeField] GameObject prefabRoundBlock;
    [SerializeField] GameObject prefabHardRoundBlock;
    [SerializeField] GameObject prefabAtelierHardBlock;
    [SerializeField] GameObject prefabAtelierHardestBlock;
    [SerializeField] GameObject prefabItemBlock;
    [SerializeField] GameObject prefabFlashBlock;
    [SerializeField] GameObject prefabCountBlock;
    [SerializeField] GameObject prefabBlockSlider;
    [SerializeField] GameObject prefabCountLevelUpBlock;
    [SerializeField] GameObject prefabSilverBlock;
    [SerializeField] GameObject prefabGoldBlock;
    [SerializeField] GameObject prefabTransparentBlock;
    [SerializeField] GameObject prefabCountRoundBlock;
    [SerializeField] GameObject prefabCountRoundLevelUpBlock;
    [SerializeField] GameObject prefabAccelerateBlock;

    [SerializeField] GameObject prefabSteelBlock;
    [SerializeField] GameObject prefabPrecipitateBlock;
    [SerializeField] GameObject prefabPointBumper;
    [SerializeField] GameObject prefabUpDownGate;

    [SerializeField] GameObject prefabSingleGateSystem;
    [SerializeField] GameObject prefabSixGatesSystem;
    [SerializeField] GameObject prefabSlidingSteelBlocksSystem;
    [SerializeField] GameObject prefabSteelBlocksWheelSystem;
    [SerializeField] GameObject prefabBonusStageSystem;
    [SerializeField] GameObject prefabBlackBoxesSystem;
    [SerializeField] GameObject prefabSlotSystem;
    [SerializeField] GameObject prefabSlidingLongSteelBlocksSystem;
    [SerializeField] GameObject prefabJackpotChallengeSystem;
    [SerializeField] GameObject prefabLv26System;
    [SerializeField] GameObject prefabLv27System;
    [SerializeField] GameObject prefabSKL144System;
    [SerializeField] GameObject prefabTimeBomberSystem;
    [SerializeField] GameObject prefabInfiniteBlocksSystem;
    [SerializeField] GameObject prefabCeilingSystem;
    [SerializeField] GameObject prefabVerticalLooperSystem;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateNormalBlock(float positionX, float positionY, int colorCode)
    {
        Instantiate(prefabNormalBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<NormalBlock>().Initialize(colorCode);
    }

    public void CreateHardBlock(float positionX, float positionY)
    {
        Instantiate(prefabHardBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateHardRoundBlock(float positionX, float positionY)
    {
        Instantiate(prefabHardRoundBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateSmallBlock(float positionX, float positionY, int colorCode)
    {
        Instantiate(prefabSmallBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<NormalBlock>().Initialize(colorCode);
    }

    public void CreateRoundBlock(float positionX, float positionY, int colorCode)
    {
        Instantiate(prefabRoundBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<RoundBlock>().Initialize(colorCode);
    }

    public void CreateAtelierHardBlock(float positionX, float positionY, int characterCode)
    {
        Instantiate(prefabAtelierHardBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<AtelierHardBlock>().Initialize(characterCode);
    }

    public void CreateAtelierHardestBlock(float positionX, float positionY, int characterCode)
    {
        Instantiate(prefabAtelierHardestBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<AtelierHardestBlock>().Initialize(characterCode);
    }

    public void CreateCountBlock(float positionX, float positionY, int colorCode, int breakCount)
    {
        Instantiate(prefabCountBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<CountBlock>().Initialize(colorCode, breakCount);
    }

    public void CreateSlidingCountBlock(float positionX, float positionY, int colorCode, int breakCount)
    {
        Instantiate(prefabBlockSlider, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<BlockSlider>().CreateCountBlock(colorCode, breakCount);
    }

    public void CreateCountLevelUpBlock(float positionX, float positionY, int breakCount)
    {
        Instantiate(prefabCountLevelUpBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<CountLevelUpBlock>().Initialize(breakCount);
    }

    public void CreateSilverBlock(float positionX, float positionY)
    {
        Instantiate(prefabSilverBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateGoldBlock(float positionX, float positionY)
    {
        Instantiate(prefabGoldBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateCountRoundBlock(float positionX, float positionY, int colorCode, int breakCount)
    {
        Instantiate(prefabCountRoundBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<CountBlock>().Initialize(colorCode, breakCount);
    }

    public void CreateCountRoundLevelUpBlock(float positionX, float positionY, int breakCount)
    {
        Instantiate(prefabCountRoundLevelUpBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<CountRoundLevelUpBlock>().Initialize(breakCount);
    }

    public void CreateItemBlock(float positionX, float positionY, int itemCode)
    {
        Instantiate(prefabItemBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<ItemBlock>().Initialize(itemCode);
    }

    public void CreateFlashBlock(float positionX, float positionY)
    {
        Instantiate(prefabFlashBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateTransparentBlock(float positionX, float positionY)
    {
        Instantiate(prefabTransparentBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateAccelerateBlock(float positionX, float positionY)
    {
        Instantiate(prefabAccelerateBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }



    public void CreateSteelBlock(float positionX, float positionY)
    {
        Instantiate(prefabSteelBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreatePrecipitateBlock(float positionX, float positionY)
    {
        Instantiate(prefabPrecipitateBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreatePointBumper(float positionX, float positionY, int bumperCode)
    {
        Instantiate(prefabPointBumper, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<PointBumper>().Initialize(bumperCode);
    }

    public void CreateUpDownGate(float positionX, float positionY, int directionCode)
    {
        Instantiate(prefabUpDownGate, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<UpDownGate>().Initialize(directionCode);
    }



    public void CreateSingleGateSystem()
    {
        Instantiate(prefabSingleGateSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public SixGatesSystem CreateSixGatesSystem()
    {
        return Instantiate(prefabSixGatesSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<SixGatesSystem>();
    }

    public void CreateSlidingSteelBlocksSystem()
    {
        Instantiate(prefabSlidingSteelBlocksSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateSteelBlocksWheelSystem(int numberOfBlocks, float rotationSpeed)
    {
        Instantiate(prefabSteelBlocksWheelSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<SteelBlocksWheelSystem>().Initialize(numberOfBlocks, rotationSpeed); ;
    }

    public BonusStageSystem CreateBonusStageSystem()
    {
        return Instantiate(prefabBonusStageSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<BonusStageSystem>();
    }

    public void CreateBlackBoxesSystem()
    {
        Instantiate(prefabBlackBoxesSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateSlotSystem()
    {
        Instantiate(prefabSlotSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateSlidingLongSteelBlocksSystem()
    {
        Instantiate(prefabSlidingLongSteelBlocksSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public JackpotChallengeSystem CreateJackpotChallengeSystem()
    {
        return Instantiate(prefabJackpotChallengeSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<JackpotChallengeSystem>();
    }

    public Level26System CreateLv26System()
    {
        return Instantiate(prefabLv26System, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Level26System>();
    }

    public Level27System CreateLv27System()
    {
        return Instantiate(prefabLv27System, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Level27System>();
    }

    public SKL144Sytem CreateSKL144System()
    {
        return Instantiate(prefabSKL144System, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<SKL144Sytem>();
    }

    public void CreateTimeBomberSystem()
    {
        Instantiate(prefabTimeBomberSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateInfiniteBlocksSystem()
    {
        Instantiate(prefabInfiniteBlocksSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateCeilingSystem()
    {
        Instantiate(prefabCeilingSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    public void CreateVerticalLooperSystem()
    {
        Instantiate(prefabVerticalLooperSystem, new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }
}
