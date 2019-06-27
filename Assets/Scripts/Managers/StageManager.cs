using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    const int RED = 0;
    const int ORANGE = 1;
    const int YELLOW = 2;
    const int GREEN = 3;
    const int BLUE = 4;
    const int INDIGO = 5;
    const int VIOLET = 6;
    const int YELLOW_GREEN = 7;

    MainManager mainManager;
    SystemManager systemManager;

    [SerializeField] GameObject prefabNormalBlock;
    [SerializeField] GameObject prefabHardBlock;
    [SerializeField] GameObject prefabSmallBlock;
    [SerializeField] GameObject prefabRoundBlock;
    [SerializeField] GameObject prefabAtelierHardBlock;
    [SerializeField] GameObject prefabAtelierHardestBlock;
    [SerializeField] GameObject prefabCountBlock;
    [SerializeField] GameObject prefabSilverBlock;
    [SerializeField] GameObject prefabGoldBlock;
    [SerializeField] GameObject prefabFlashBlock;
    [SerializeField] GameObject prefabTransparentBlock;
    [SerializeField] GameObject prefabPowerUpBlock;
    [SerializeField] GameObject prefabProtectorBlock;
    [SerializeField] GameObject prefabTrapGuardBlock;
    [SerializeField] GameObject prefabBlackBlock;
    [SerializeField] GameObject prefabRainbowBlock;
    [SerializeField] GameObject prefabLevelUpBlock;

    [SerializeField] GameObject prefabPrecipitateBlock;
    [SerializeField] GameObject prefabSteelBlock;

    [SerializeField] GameObject prefabBlockSlider;

    // Use this for initialization
    void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateStage()
    {
        int level = mainManager.GetLevel();
        switch (level)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:

                break;
            case 17:
                for(int y=0; y<7; y++)
                {
                    int replaceToSteel1 = Random.Range(0, 4);
                    int replaceToSteel2 = Random.Range(5, 9);
                    for(int x=0; x<9; x++)
                    {
                        float positionX = x * 100.0f - 400.0f;
                        float positionY = y * 40.0f + 30.0f;
                        int colorCode = y;
                        if(x == replaceToSteel1 || x == replaceToSteel2)
                        {
                            CreateSteelBlock(positionX, positionY);
                        }
                        else
                        {
                            CreateNormalBlock(positionX, positionY, colorCode);
                        }
                    }
                }
                //systemManager.StartBlackBoxSystem();
                break;
            case 18:

                break;
            case 19:
                for (int x = 0; x < 15; x++)
                {
                    for (int y = 0; y < 10; y++)
                    {
                        float positionX = x * 50.0f - 350.0f;
                        float positionY = y * 20.0f + 150.0f;
                        int colorCode = y % 7;
                        CreateNormalBlock(positionX, positionY, colorCode);
                    }
                }
                CreatePrecipitateBlock(-450.0f, 200.0f);
                CreatePrecipitateBlock(0.0f, 200.0f);
                CreatePrecipitateBlock(450.0f, 200.0f);
                break;
            case 20:
                //ゴールドブロック&シルバーブロック19x10

                break;

            case 21:
                //systemManager.StartVerticalLooperSystem();
                break;
            case 22:
                //ブロック20x14、ゲームオーバーアイテム付き出現
                for (int x = 0; x < 18; x++)
                {
                    for (int y = 0; y < 14; y++)
                    {
                        float positionX = x * 50.0f - 425.0f;
                        float positionY = y * 20.0f + 60.0f;
                        int colorCode = y % 7;
                        if((x == 3 || x == 14) && y == 7)
                        {
                            CreateBlackBlock(positionX, positionY);
                        }
                        else
                        {
                            CreateNormalBlock(positionX, positionY, colorCode);
                        }
                    }
                }
                break;

            case 23:
                //systemManager.StartSteelBlocksWheelSystem();
                break;
            case 24:
                //6ゲートシステム・上級

                //systemManager.StartSixGatesSystem();
                break;
            case 25:
                //ジャックポットチャレンジ
                break;
            case 26:
                //12連戦
                break;
            case 27:
                //20連戦
                break;
            case 28:
                //なんか強そうな奴
                break;
            case 29:
                //Time limit 10min
                int startColorCodeForLv29 = Random.Range(0, 7);
                for (int x = 0; x < 18; x++)
                {
                    for (int y = 0; y < 1; y++)
                    {
                        float positionX = x * 50.0f - 425.0f;
                        float positionY = y * 20.0f + 140.0f;
                        int colorCode = (startColorCodeForLv29 + x) % 7;
                        int breakCount = y + 1;
                        CreateCountBlock(positionX, positionY, colorCode, breakCount);
                    }
                }
                //systemManager.StartTimeBomberSystem();
                break;
            case 30:
                //Infinite Blocks
                for (int x = 0; x < 20; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        float positionX = x * 50.0f - 475.0f;
                        float positionY = y * 20.0f + 162.0f;
                        int colorCode = Random.Range(0, 7);
                        CreateNormalBlock(positionX, positionY, colorCode);
                    }
                }
                //systemManager.StartInfiniteBlocksSystem();
                break;
        }
    }

    void CreateNormalBlock(float positionX, float positionY, int colorCode)
    {
        Instantiate(prefabNormalBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<NormalBlock>().Initialize(colorCode);
    }

    void CreateHardBlock(float positionX, float positionY)
    {
        Instantiate(prefabHardBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateSmallBlock(float positionX, float positionY, int colorCode)
    {
        Instantiate(prefabSmallBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<NormalBlock>().Initialize(colorCode);
    }

    void CreateRoundBlock(float positionX, float positionY, int colorCode)
    {
        Instantiate(prefabRoundBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<RoundBlock>().Initialize(colorCode);
    }

    void CreateAtelierHardBlock(float positionX, float positionY, int characterCode)
    {
        Instantiate(prefabAtelierHardBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<AtelierHardBlock>().Initialize(characterCode);
    }

    void CreateAtelierHardestBlock(float positionX, float positionY, int characterCode)
    {
        Instantiate(prefabAtelierHardestBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<AtelierHardestBlock>().Initialize(characterCode);
    }

    void CreateCountBlock(float positionX, float positionY, int colorCode, int breakCount)
    {
        Instantiate(prefabCountBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<CountBlock>().Initialize(colorCode, breakCount);
    }

    void CreateSlidingCountBlock(float positionX, float positionY, int colorCode, int breakCount)
    {
        Instantiate(prefabBlockSlider, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<BlockSlider>().CreateCountBlock(colorCode, breakCount);
    }

    void CreateSilverBlock(float positionX, float positionY)
    {
        Instantiate(prefabSilverBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateGoldBlock(float positionX, float positionY)
    {
        Instantiate(prefabGoldBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateFlashBlock(float positionX, float positionY)
    {
        Instantiate(prefabFlashBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateTransparentBlock(float positionX, float positionY)
    {
        Instantiate(prefabTransparentBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreatePowerUpBlock(float positionX, float positionY)
    {
        Instantiate(prefabPowerUpBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateProtectorBlock(float positionX, float positionY)
    {
        Instantiate(prefabProtectorBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateTrapGuardBlock(float positionX, float positionY)
    {
        Instantiate(prefabTrapGuardBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateBlackBlock(float positionX, float positionY)
    {
        Instantiate(prefabBlackBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }
    void CreateRainbowBlock(float positionX, float positionY)
    {
        Instantiate(prefabRainbowBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }
    void CreateLevelUpBlock(float positionX, float positionY)
    {
        Instantiate(prefabLevelUpBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreatePrecipitateBlock(float positionX, float positionY)
    {
        Instantiate(prefabPrecipitateBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }

    void CreateSteelBlock(float positionX, float positionY)
    {
        Instantiate(prefabSteelBlock, new Vector3(positionX, positionY, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
    }
}
