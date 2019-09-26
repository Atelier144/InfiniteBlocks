using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class MainManager : GameManager {

    const int DUMMY_NUMBER = 10;
    const int MAX_SCORE = 999999;
    const int MAX_LEVEL = 30;
    const int MAX_REST_OF_BALL = 9;

    [SerializeField] AudioMixer audioMixer;

    StageManager stageManager;
    MusicManager musicManager;
    ItemManager itemManager;
    SystemManager systemManager;
    SignalManager signalManager;

    Ball theBall;
    Racket theRacket;

    [SerializeField] GameObject[] prefabsStage = new GameObject[31];

    [SerializeField] GameObject theProtector;
    [SerializeField] GameObject theFlash;
    [SerializeField] GameObject theGameOver;
    [SerializeField] GameObject theExplosion;

    [SerializeField] GameObject panelMainView;

    [SerializeField] Sprite[] spritesYellowNumber = new Sprite[11];

    [SerializeField] Sprite spriteReturnButtonEnabled;
    [SerializeField] Sprite spriteReturnButtonDisabled;
    [SerializeField] Sprite spriteRetireButtonEnabled;
    [SerializeField] Sprite spriteRetireButtonDisabled;

    [SerializeField] Image[] imagesHighScoreNumber = new Image[6];
    [SerializeField] Image[] imagesGameScoreNumber = new Image[6];
    [SerializeField] Image[] imagesGameLevelNumber = new Image[2];
    [SerializeField] Image imageRestOfBallNumber;

    [SerializeField] GameObject missTelop;
    [SerializeField] GameObject replayTelop;
    [SerializeField] GameObject levelUpTelop;
    [SerializeField] GameObject levelUpBonusTelop;

    [SerializeField] Button buttonReturn;
    [SerializeField] Button buttonRetire;

    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSE;

    [SerializeField] Text textBallStartTelop;
    [SerializeField] Text textReturnDialog;
    [SerializeField] Text textRetireDialog;
    [SerializeField] Text textReturnDialogButtonCancel;
    [SerializeField] Text textReturnDialogButtonOK;
    [SerializeField] Text textRetireDialogButtonCancel;
    [SerializeField] Text textRetireDialogButtonOK;


    [SerializeField] Font fontForEnglish;
    [SerializeField] Font fontForJapanese;

    [SerializeField] Text textRestOfBlocksForDebug;

    [SerializeField] bool isTestMode;
    [SerializeField] int testLevel;

    AudioSource[] audioSources;

    static float currentSliderBGMValue;
    static float currentSliderSEValue;

    int score;
    int level;
    int restOfBlocks = 0;
    int comboBonus = 0;
    int playerId = 0;
    int highScore = 0;
    int restOfBall = 3;

    int dialogStatus = 1;   //0:Uncontrollable 1:Standby 2:BallMoving 3:ReturnDialog 4:RetireDialog

    string languageName = "Japanese";

    int standardBeginnerSaveCount;

    bool isPerformancePlay = false;

    int jackpotScore = 100000;

    bool hasSlot144;

    Stage currentStage;

    // Use this for initialization
    protected override void Start () {
        base.Start();

        theBall = GameObject.Find("TheBall").GetComponent<Ball>();
        theRacket = GameObject.Find("TheRacket").GetComponent<Racket>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        audioSources = GetComponents<AudioSource>();

        highScore = base.GetHighScore();
        level = base.GetLevel();
        playerId = base.GetPlayerId();

        sliderBGM.value = currentSliderBGMValue;
        sliderSE.value = currentSliderSEValue;

        if (level == 0) level = testLevel;

        isPerformancePlay = level == 1;

        if (isPerformancePlay) AddJackpotScore(10000);

        switch (languageName)
        {
            case "Japanese":
                textBallStartTelop.font = fontForJapanese;
                textBallStartTelop.text = "下の枠内をクリックしてスタート";
                textReturnDialog.font = fontForJapanese;
                textReturnDialog.lineSpacing = 1.35f;
                textReturnDialog.text = "ゲームを中止して、タイトル画面に戻りますか？\n（スコアは記録されません）";
                textRetireDialog.font = fontForJapanese;
                textRetireDialog.lineSpacing = 1.35f;
                textRetireDialog.text = "まだボールが残っていますが\nゲームを終了させて結果画面に切り替えますか？";
                textReturnDialogButtonCancel.font = fontForJapanese;
                textReturnDialogButtonCancel.text = "キャンセル";
                textReturnDialogButtonOK.font = fontForJapanese;
                textReturnDialogButtonOK.text = "はい";
                textRetireDialogButtonCancel.font = fontForJapanese;
                textRetireDialogButtonCancel.text = "キャンセル";
                textRetireDialogButtonOK.font = fontForJapanese;
                textRetireDialogButtonOK.text = "はい";
                break;
        }



        standardBeginnerSaveCount = playerId == 0 ? 1000 : 1250;
        signalManager.StartReplayMode(standardBeginnerSaveCount);

        if (playerId == 0) AddJackpotScore(20000);
        else AddJackpotScore(50000);

        StartCoroutine(GenerateStage());

        DrawUIDisplay();
        StartCoroutine(GarbageCollection());

        AddJackpotScore(Random.Range(0, 10));

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        DrawUIDisplay();
    }

    private void FixedUpdate()
    {
        if (dialogStatus == 2 && currentStage.IsLevelUp())
        {
            AddJackpotScore(1000);
            LevelUp();
        }


    }

    IEnumerator GarbageCollection()
    {
        while (true)
        {
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            yield return new WaitForSeconds(10.0f);
        }
    }

    public new int GetLevel()
    {
        return level;
    }

    public int GetRestOfBalls()
    {
        return restOfBall;
    }

    public int GetRestOfBlocks()
    {
        return restOfBlocks;
    }

    public int GetDialogStatus()
    {
        return dialogStatus;
    }

    public Stage GetCurrentStage()
    {
        return currentStage;
    }

    public void SetDialogStatus(int s)
    {
        dialogStatus = s;
        DrawUIDisplay();
    }

    public int GetJackpotScore()
    {
        return jackpotScore;
    }

    public void AddJackpotScore(int s)
    {
        jackpotScore += s;
    }

    public bool HasSlot144()
    {
        return hasSlot144;
    }

    public void SetHasSlot144(bool s)
    {
        hasSlot144 = s;
    }

    public int GetTimeBomberCount()
    {
        if (hasSlot144)
        {
            return 1800;
        }
        if(isPerformancePlay)
        {
            if(playerId == 0)
            {
                return 720;
            }
            return 900;
        }
        return 600;
    }

    //Returnボタン(左矢印)を押した時の呼び出されるメソッド
    public void PushReturnButton()
    {
        SetDialogStatus(3);
        audioSources[0].time = 0.12f;
        audioSources[0].Play();
    }

    //Retireボタン(掌)を押した時に呼び出されるメソッド
    public void PushRetireButton()
    {
        SetDialogStatus(4);
        audioSources[0].time = 0.12f;
        audioSources[0].Play();
    }

    public void OnValueChangedSliderBGM()
    {
        currentSliderBGMValue = sliderBGM.value;
        float sliderBGMValue = currentSliderBGMValue < -49.9f ? -80.0f : currentSliderBGMValue;
        audioMixer.SetFloat("BGM", sliderBGMValue);
    }

    public void OnValueChangedSliderSE()
    {
        currentSliderSEValue = sliderSE.value;
        float sliderSEValue = currentSliderSEValue < -49.9f ? -80.0f : currentSliderSEValue; 
        audioMixer.SetFloat("SE", sliderSEValue);
    }

    //DisplayScoreボタン(見えない)を押した時に呼び出されるメソッド
    public void PushDisplayScoreButton()
    {
        DrawUIDisplay();
    }

    //BallStartボタン(見えない)を押した瞬間に呼び出されるメソッド
    public void PushBallStartButton()
    {
        SetDialogStatus(2);
        theBall.MoveBall();
        theBall.ResetVelocity();
    }

    //ItemUseボタン(見えない)を押した瞬間に呼び出されるメソッド
    public void PushItemUseButton()
    {
        if (signalManager.IsActivePrecipitate()) theBall.Precipitate();

        if (signalManager.IsActiveShooting()) theRacket.ShootBullet();

        theBall.Detach();
    }

    //ReturnダイアログのOKボタンを押した時に呼び出されるメソッド
    public void PushDialogButtonReturn()
    {
        base.ChangeSceneToTitleScene();
    }

    //RetireダイアログのOKボタンを押した時に呼び出されるメソッド
    public void PushDialogButtonRetire()
    {
        MoveToEndScene();
    }

    //ダイアログのCancelボタンを押した時に呼び出されるメソッド
    public void PushDialogButtonCancel()
    {
        SetDialogStatus(1);
        audioSources[1].time = 0.05f;
        audioSources[1].Play();
    }


    //Scoreを加算するメソッド
    public void AddGameScore(int s)
    {
        score += s;
        if (score > MAX_SCORE) score = MAX_SCORE;
        if (score < 0) score = 0;
    }

    public void AddRestOfBall()
    {
        if (restOfBall < MAX_REST_OF_BALL) restOfBall++;
    }

    void DrawUIDisplay()
    {
        float[] positionsXforMainViewPanel = { 2048.0f, 1024.0f, 0.0f, -1024.0f, -2048.0f };

        //High-Score表示部（本番プレイ以外ではハイフン表示となる）
        if (isPerformancePlay)
        {
            int[] highScoreNumbersIndexes = new int[6];
            highScoreNumbersIndexes[0] = highScore % 10;
            highScoreNumbersIndexes[1] = highScore / 10 % 10;
            highScoreNumbersIndexes[2] = highScore / 100 % 10;
            highScoreNumbersIndexes[3] = highScore / 1000 % 10;
            highScoreNumbersIndexes[4] = highScore / 10000 % 10;
            highScoreNumbersIndexes[5] = highScore / 100000 % 10;
            for (int i = 0; i < 6; i++) imagesHighScoreNumber[i].sprite = spritesYellowNumber[highScoreNumbersIndexes[i]];
        }
        else
        {
            for (int i = 0; i < 6; i++) imagesHighScoreNumber[i].sprite = spritesYellowNumber[10];
        }

        for (int i = 0; i < 6; i++) imagesGameScoreNumber[i].sprite = spritesYellowNumber[score / (int)Mathf.Pow(10, i) % 10];

        imagesGameLevelNumber[0].sprite = spritesYellowNumber[level % 10];
        imagesGameLevelNumber[1].sprite = spritesYellowNumber[level / 10 % 10];

        imageRestOfBallNumber.sprite = spritesYellowNumber[restOfBall % 10];

        panelMainView.transform.localPosition = new Vector3(positionsXforMainViewPanel[dialogStatus], 72.0f, 0.0f);

        if(dialogStatus == 1)
        {
            buttonReturn.image.sprite = spriteReturnButtonEnabled;
            buttonReturn.interactable = true;
            buttonRetire.image.sprite = spriteRetireButtonEnabled;
            buttonRetire.interactable = true;
        }
        else
        {
            buttonReturn.image.sprite = spriteReturnButtonDisabled;
            buttonReturn.interactable = false;
            buttonRetire.image.sprite = spriteRetireButtonDisabled;
            buttonRetire.interactable = false;
        }
    }

    public void MissingHostage300()
    {
        StartCoroutine(Missing());
    }

    public IEnumerator Missing()
    {
        DestroyAllItems();
        DestroyAllBullets();
        yield return new WaitForSeconds(0.05f);

        signalManager.StopAllSignalsWithoutReplayMode();
        SetDialogStatus(0);
        if (signalManager.IsActiveReplayMode())
        {
            replayTelop.SetActive(true);
            theBall.DiminishForReplay();
            AddJackpotScore(10);
        }
        else if (currentStage.IsLevelUpFailZone())
        {
            LevelUp();
        }
        else
        {
            AddJackpotScore(50);
            missTelop.SetActive(true);
            theBall.DiminishForMissing();
        }

    }

    public IEnumerator GenerateStage()
    {
        GameObject[] gameObjectsStage = GameObject.FindGameObjectsWithTag("Stage");
        foreach (GameObject gameObjectStage in gameObjectsStage) Destroy(gameObjectStage);
        yield return null;

        Instantiate(prefabsStage[level], new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        currentStage = GameObject.FindWithTag("Stage").GetComponent<Stage>();
        DestroyAllBlocks();
        DestroyAllSystems();
        yield return null;

        currentStage.GenerateStage();

        SetDialogStatus(1);
        theBall.SetBall();
        theBall.StopBall();
        theBall.ResetVelocity();
        musicManager.ChangeMusic(currentStage.GetMusicChannel());
    }

    //画面上にある全てのブロックを消去するメソッド
    public void DestroyAllBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        GameObject[] blockSupports = GameObject.FindGameObjectsWithTag("BlockSupport");
        GameObject[] precipitateBlocks = GameObject.FindGameObjectsWithTag("PrecipitateBlock");

        foreach (GameObject block in blocks) Destroy(block);
        foreach (GameObject blockSupport in blockSupports) Destroy(blockSupport);
        foreach (GameObject precipitateBlock in precipitateBlocks) Destroy(precipitateBlock);
    }

    public void DestroyAllSystems()
    {
        GameObject[] systems = GameObject.FindGameObjectsWithTag("System");
        foreach (GameObject system in systems) Destroy(system);
    }

    public void DestroyAllItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject item in items) Destroy(item);
    }

    public void DestroyAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) Destroy(bullet);
    }



    public void LevelUp()
    {
        DestroyAllItems();
        DestroyAllBullets();
        signalManager.StopAllSignalsWithoutReplayMode();

        SetDialogStatus(0);
        theBall.DiminishForLevelUp();

        int bonusScore = currentStage.GetLevelUpBonus();

        if (bonusScore > 0) levelUpBonusTelop.GetComponent<LevelUpBonusTelop>().SetBonusScore(bonusScore);
        AddGameScore(bonusScore);

        levelUpTelop.SetActive(true);
        if (bonusScore > 0) levelUpBonusTelop.SetActive(true);

        missTelop.SetActive(false);
        replayTelop.SetActive(false);
    }

    public void MoveToEndScene()
    {
        base.ChangeSceneToEndScene(score, highScore, level, isPerformancePlay);
    }

    public void OnAnimationEndFromLevelUpTelop()
    {
        levelUpTelop.SetActive(false);
        levelUpBonusTelop.SetActive(false);

        theRacket.SetStepOfLength(3);
        level++;
        StartCoroutine(GenerateStage());
    }

    public void OnAnimationEndFromMissTelop()
    {
        missTelop.SetActive(false);
        if (restOfBall > 0)
        {
            restOfBall--;
            SetDialogStatus(1);
            theBall.StopBall();
            theBall.SetBall();
            signalManager.StartReplayMode(standardBeginnerSaveCount);
            theRacket.ReboundExpand();
        }
        else
        {
            MoveToEndScene();
        }
    }

    public void OnAnimationEndFromReplayTelop()
    {
        replayTelop.SetActive(false);
        SetDialogStatus(1);
        theBall.StopBall();
        theBall.SetBall();
        theRacket.ReboundShrink();
    }

    public void OnAnimationEndFromExplosion()
    {
        MoveToEndScene();
    }

    public void AddComboBonus(int value)
    {
        if(comboBonus < 20)
        {
            comboBonus += value;
        }
    }

    public void ClearComboBonus()
    {
        comboBonus = 0;
    }

    public void AddGameScoreByComboBonus()
    {
        AddGameScore(comboBonus);
    }

    //閃光を起こすメソッド（トラップガードで防ぐ）
    public void StartFlash()
    {
        theFlash.SetActive(false);
        theFlash.SetActive(true);
    }

    public void StartGameOver()
    {
        SetDialogStatus(0);
        musicManager.HaltMusic();
        theBall.Diminish();
        theGameOver.SetActive(true);
    }

    public void StartExplosion()
    {
        SetDialogStatus(0);
        musicManager.HaltMusic();
        theBall.Diminish();
        theExplosion.SetActive(true);
    }

    public int[] GetRandomPatterns(int length)
    {
        int[] retval = new int[length];
        for (int i = 0; i < retval.Length; i++) retval[i] = i;
        int a = retval.Length;
        while(a > 0)
        {
            int i = a - 1;
            int j = Random.Range(0, a);
            int tmp = retval[i];
            retval[i] = retval[j];
            retval[j] = tmp;
            a--; 
        }
        return retval;
    }
}