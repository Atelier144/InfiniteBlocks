using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotChallengeSystem : MonoBehaviour {

    MainManager mainManager;

    [SerializeField] GameObject[] gameObjectsJackpotNumbers = new GameObject[6];
    [SerializeField] GameObject[] gameObjectsDisplayNumbers = new GameObject[2];
    [SerializeField] GameObject[] gameObjectsStepSignals = new GameObject[21];
    [SerializeField] GameObject[] gameObjectsSwitchSignals = new GameObject[8];
    [SerializeField] GameObject gameObjectDisplayItemL;
    [SerializeField] GameObject gameObjectDisplayItemR;
    [SerializeField] GameObject[] gameObjectsJackpotSwitches = new GameObject[8];
    [SerializeField] GameObject gameObjectJackpotBoard;
    [SerializeField] GameObject gameObjectSwitchSignalsJackpot;

    [SerializeField] Sprite[] spritesNumbers = new Sprite[10];
    [SerializeField] Sprite[] spritesItems = new Sprite[24];
    [SerializeField] Sprite[] spritesSwitchSignals = new Sprite[15];
    [SerializeField] Sprite spriteStepSignalNormalOff;
    [SerializeField] Sprite spriteStepSignalNormalOn;
    [SerializeField] Sprite spriteStepSignalJackpotOff;
    [SerializeField] Sprite spriteStepSignalJackpotOn;

    [SerializeField] GameObject prefabItem;

    GameObject gameObjectLevelUpTelop;

    SpriteRenderer[] spriteRenderersJackpotNumbers = new SpriteRenderer[6];
    SpriteRenderer[] spriteRenderersDisplayNumbers = new SpriteRenderer[2];
    SpriteRenderer[] spriteRenderersStepSignals = new SpriteRenderer[21];
    SpriteRenderer[] spriteRenderersSwitchSignals = new SpriteRenderer[8];
    SpriteRenderer spriteRendererDisplayItemL;
    SpriteRenderer spriteRendererDisplayItemR;

    JackpotChallengeSystemSwitch[] jackpotSwitches = new JackpotChallengeSystemSwitch[8];
    JackpotChallengeSystemBoard jackpotBoard;

    AudioSource[] audioSources;

    int switchCount;

    int jackpotScore;
    int jackpotStep;
    int[] currentSwitchSignals = new int[8];

    bool hasGottenJackpot;

    int[][] switchSignalsPatterns =
    {
        // Step 0
        new int[] {1, 1, 2, 2, 3, 3, 4, 4 },
        // Step 1
        new int[] {1, 1, 2, 2, 3, 4, 5, 11},
        // Step 2
        new int[] {1, 2, 2, 3, 4, 5, 5, 11},
        // Step 3
        new int[] {1, 2, 2, 3, 5, 6, 10, 11},
        // Step 4
        new int[] {1, 1, 2, 3, 5, 5, 5, 10},
        // Step 5
        new int[] {1, 1, 1, 1, 1, 5, 6, 6},
        // Step 6
        new int[] {1, 2, 3, 6, 6, 10, 11, 11},
        // Step 7
        new int[] {2, 2, 5, 7, 11, 11, 11, 11},
        // Step 8
        new int[] {1, 1, 1, 2, 6, 10, 11, 12},
        // Step 9
        new int[] {1, 1, 1, 1, 7, 10, 11, 12},
        // Step 10
        new int[] {1, 1, 1, 1, 5, 8, 10, 12},
        // Step 11
        new int[] {1, 1, 1, 1, 6, 8, 10, 12},
        // Step 12
        new int[] {1, 1, 2, 5, 6, 10, 12, 12},
        // Step 13
        new int[] {1, 1, 2, 5, 5, 5, 10, 13},
        // Step 14
        new int[] {1, 1, 2, 6, 7, 8, 10, 13},
        // Step 15
        new int[] {1, 3, 5, 5, 5, 6, 10, 13},
        // Step 16
        new int[] {1, 1, 3, 5, 6, 6, 7, 10 },
        // Step 17
        new int[] {1, 2, 5, 6, 7, 8, 10, 14},
        // Step 18
        new int[] {1, 2, 7, 9, 9, 10, 13, 13},
        // Step 19
        new int[] {1, 6, 7, 8, 9, 9, 10, 14},
        // Step 20
        new int[] {1, 7, 8, 8, 9, 9, 9, 10},
        // Step 21
        new int[] {0, 0, 0, 0, 0, 0, 0, 0 }
    };

    //                   0  1  2  3  4  5  6  7  8  9  10  11  12  13  14  15  16  17  18  19  20  JP
    int[] itemCodesL = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  1,  2,  3,  3,  4,  4,  9, 21,  5, 20,  7,  0 };
    int[] itemCodesR = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,  0,  0,  0,  2,  0,  4, 14,  9, 21,  5,  0,  0 };

    int itemCodeL;
    int itemCodeR;

    // Use this for initialization
    void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        for (int i = 0; i < 6; i++) spriteRenderersJackpotNumbers[i] = gameObjectsJackpotNumbers[i].GetComponent<SpriteRenderer>();
        for (int i = 0; i < 2; i++) spriteRenderersDisplayNumbers[i] = gameObjectsDisplayNumbers[i].GetComponent<SpriteRenderer>();
        for (int i = 0; i < 21; i++) spriteRenderersStepSignals[i] = gameObjectsStepSignals[i].GetComponent<SpriteRenderer>();
        for (int i = 0; i < 8; i++) spriteRenderersSwitchSignals[i] = gameObjectsSwitchSignals[i].GetComponent<SpriteRenderer>();
        spriteRendererDisplayItemL = gameObjectDisplayItemL.GetComponent<SpriteRenderer>();
        spriteRendererDisplayItemR = gameObjectDisplayItemR.GetComponent<SpriteRenderer>();

        for (int i = 0; i < 8; i++) jackpotSwitches[i] = gameObjectsJackpotSwitches[i].GetComponent<JackpotChallengeSystemSwitch>();
        jackpotBoard = gameObjectJackpotBoard.GetComponent<JackpotChallengeSystemBoard>();

        gameObjectLevelUpTelop = GameObject.Find("Telops").transform.Find("LevelUpTelop").gameObject;   //非アクティブなGameObjectを取得するための手段

        audioSources = GetComponents<AudioSource>();

        jackpotScore = mainManager.GetJackpotScore();
        StartCoroutine(Initialize());
    }
	

	// Update is called once per frame
	void Update () {
        if (gameObjectLevelUpTelop.activeSelf) jackpotBoard.SetTrigger("Finish");
        Draw();
	}

    public void Draw()
    {

        int[] jackpotNumbersIndexes = new int[6];
        jackpotNumbersIndexes[0] = jackpotScore % 10;
        jackpotNumbersIndexes[1] = jackpotScore / 10 % 10;
        jackpotNumbersIndexes[2] = jackpotScore / 100 % 10;
        jackpotNumbersIndexes[3] = jackpotScore / 1000 % 10;
        jackpotNumbersIndexes[4] = jackpotScore / 10000 % 10;
        jackpotNumbersIndexes[5] = jackpotScore / 100000 % 10;
        for (int i = 0; i < 6; i++) spriteRenderersJackpotNumbers[i].sprite = spritesNumbers[jackpotNumbersIndexes[i]];

        int[] displayNumbersIndexes = new int[2];
        displayNumbersIndexes[0] = jackpotStep % 10;
        displayNumbersIndexes[1] = jackpotStep / 10 % 10;
        for (int i = 0; i < 2; i++) spriteRenderersDisplayNumbers[i].sprite = spritesNumbers[displayNumbersIndexes[i]];

        for (int i = 0; i < 21; i++)
        {
            if (jackpotStep > i)
            {
                if (i < 20) spriteRenderersStepSignals[i].sprite = spriteStepSignalNormalOn;
                else spriteRenderersStepSignals[i].sprite = spriteStepSignalJackpotOn;
            }
            else
            {
                if (i < 20) spriteRenderersStepSignals[i].sprite = spriteStepSignalNormalOff;
                else spriteRenderersStepSignals[i].sprite = spriteStepSignalJackpotOff;
            }
        }

        spriteRendererDisplayItemL.sprite = spritesItems[itemCodeL];
        spriteRendererDisplayItemR.sprite = spritesItems[itemCodeR];
    }

    public void ProcessSwitches()
    {

        int[] switchSignalsPatternsByStep = switchSignalsPatterns[jackpotStep];

        int a = switchSignalsPatternsByStep.Length;
        while (a > 0)
        {
            int i = a - 1;
            int j = Random.Range(0, a);
            int tmp = switchSignalsPatternsByStep[i];
            switchSignalsPatternsByStep[i] = switchSignalsPatternsByStep[j];
            switchSignalsPatternsByStep[j] = tmp;
            a--;
        }

        switchCount++;
        itemCodeL = switchCount % 2 == 0 ? itemCodesL[jackpotStep] : itemCodesR[jackpotStep];
        itemCodeR = switchCount % 2 == 0 ? itemCodesR[jackpotStep] : itemCodesL[jackpotStep];

        currentSwitchSignals = switchSignalsPatternsByStep;
        Draw();
        ChangeSwtichesColor();
    }

    public void ChangeSwtichesColor()
    {
        int[] switchSignalCodesToColorCodes = { 0, 4, 4, 4, 4, 2, 2, 2, 2, 2, 1, 3, 3, 3, 3 };
        string[] colorCodesToNames = { "Black", "Red", "Orange", "Yellow", "Green" };
        for (int i = 0; i < 8; i++)
        {
            spriteRenderersSwitchSignals[i].sprite = spritesSwitchSignals[currentSwitchSignals[i]];
            jackpotSwitches[i].ChangeColor(colorCodesToNames[switchSignalCodesToColorCodes[currentSwitchSignals[i]]]);
        }
    }

    public void OnJackpotSwitchOn(int switchCode)
    {
        float[] audioTimes = { 0.05f, 0.02f, 0.02f };
        int targetCode = currentSwitchSignals[switchCode];
        switch (targetCode)
        {
            case 1:
                jackpotStep += 1;
                audioSources[0].time = audioTimes[0];
                audioSources[0].Play();
                break;
            case 2:
                jackpotStep += 2;
                audioSources[0].time = audioTimes[0];
                audioSources[0].Play();
                break;
            case 3:
                jackpotStep += 3;
                audioSources[0].time = audioTimes[0];
                audioSources[0].Play();
                break;
            case 4:
                jackpotStep += 4;
                audioSources[0].time = audioTimes[0];
                audioSources[0].Play();
                break;
            case 5:
                jackpotStep -= 1;
                audioSources[2].time = audioTimes[2];
                audioSources[2].Play();
                break;
            case 6:
                jackpotStep -= 2;
                audioSources[2].time = audioTimes[2];
                audioSources[2].Play();
                break;
            case 7:
                jackpotStep -= 3;
                audioSources[2].time = audioTimes[2];
                audioSources[2].Play();
                break;
            case 8:
                jackpotStep -= 4;
                audioSources[2].time = audioTimes[2];
                audioSources[2].Play();
                break;
            case 9:
                jackpotStep -= 5;
                audioSources[2].time = audioTimes[2];
                audioSources[2].Play();
                break;
            case 10:
                jackpotStep = 0;
                if (GenerateItems())
                {
                    audioSources[4].time = 0.1f;
                    audioSources[4].Play();
                }
                else
                {
                    audioSources[3].time = 0.1f;
                    audioSources[3].Play();
                }
                break;
            case 11:
                jackpotScore += 100;
                audioSources[1].time = audioTimes[1];
                audioSources[1].Play();
                break;
            case 12:
                jackpotScore += 200;
                audioSources[1].time = audioTimes[1];
                audioSources[1].Play();
                break;
            case 13:
                jackpotScore += 500;
                audioSources[1].time = audioTimes[1];
                audioSources[1].Play();
                break;
            case 14:
                jackpotScore += 1000;
                audioSources[1].time = audioTimes[1];
                audioSources[1].Play();
                break;
        }
        if (jackpotStep < 0) jackpotStep = 0;
        if (jackpotStep >= 21 && !hasGottenJackpot)
        {
            jackpotStep = 21;
            mainManager.AddGameScore(jackpotScore);
            hasGottenJackpot = true;
            jackpotBoard.SetTrigger("Congratulations");
            gameObjectSwitchSignalsJackpot.SetActive(true);
            for (int i = 0; i < jackpotSwitches.Length; i++) jackpotSwitches[i].ChangeColor("Rainbow");
            audioSources[5].time = 0.2f;
            audioSources[5].Play();
        }

        ProcessSwitches();
    }

    public bool HasGottenJackpot()
    {
        return hasGottenJackpot;
    }

    public bool GenerateItems()
    {
        Vector3 appearPositionLeft = new Vector3(-450.0f, 107.0f, 0.0f);
        Vector3 appearPositionRight = new Vector3(450.0f, 107.0f, 0.0f);
        Instantiate(prefabItem, appearPositionLeft, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Item>().Initialize(-itemCodeL);
        Instantiate(prefabItem, appearPositionRight, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Item>().Initialize(-itemCodeR);
        return itemCodeL != 0 || itemCodeR != 0;
    }

    IEnumerator TestStepSignals()
    {
        for(int i = 0; i <= 21; i++)
        {
            jackpotStep = i;
            Draw();
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator TestSwitchSignals()
    {
        while (true)
        {
            for (int i = 0; i < 8; i++) currentSwitchSignals[i] = Random.Range(0, 15);
            Draw();
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator Initialize()
    {
        yield return new WaitForFixedUpdate();
        ProcessSwitches();
    }
}
