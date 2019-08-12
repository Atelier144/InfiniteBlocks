using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class MainManager : MonoBehaviour {

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

    public static int gameScoreForEndScene;
    public static int highScoreForEndScene;
    public static int playerIdForEndScene;
    public static int gameLevelForEndScene;
    public static bool isPerformancePlayForEndScene;

    static float currentSliderBGMValue;
    static float currentSliderSEValue;

    int gameScore = 0;
    int gameLevel = 1;
    int restOfBlocks = 0;
    int comboBonus = 0;
    int playerId = 0;
    int highScore = 0;
    int restOfBall = 3;
    int displayGameScore = 0;
    int displayHighScore = 0;
    int dialogStatus = 1;   //0:Uncontrollable 1:Standby 2:BallMoving 3:ReturnDialog 4:RetireDialog

    string languageName = "Japanese";

    int standardBeginnerSaveCount;

    bool isMusicOn = true;
    bool isSoundOn = true;
    bool isPerformancePlay = false;

    int jackpotScore = 100000;

    Stage currentStage;

    // Use this for initialization
    void Start () {

        theBall = GameObject.Find("TheBall").GetComponent<Ball>();
        theRacket = GameObject.Find("TheRacket").GetComponent<Racket>();
        stageManager = GameObject.Find("StageManager").GetComponent<StageManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        audioSources = GetComponents<AudioSource>();

        highScore = TitleManager.highScoreForMainScene;
        gameLevel = TitleManager.gameLevelForMainScene;
        playerId = TitleManager.playerIdForMainScene;

        sliderBGM.value = currentSliderBGMValue;
        sliderSE.value = currentSliderSEValue;

        if (isTestMode) gameLevel = testLevel;

        isPerformancePlay = gameLevel == 1;

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

        displayGameScore = gameScore;
        displayHighScore = highScore;


        standardBeginnerSaveCount = playerId == 0 ? 1000 : 1250;
        signalManager.StartReplayMode(standardBeginnerSaveCount);

        StartCoroutine(GenerateStage());

        DrawUIDisplay();
    }
	
	// Update is called once per frame
	void Update () {

        if (gameScore > displayGameScore) displayGameScore++;
        else if (gameScore < displayGameScore) displayGameScore--;

        if (displayGameScore >= displayHighScore) displayHighScore = displayGameScore;


        displayGameScore = gameScore;
        displayHighScore = highScore;

        DrawUIDisplay();
	}

    private void FixedUpdate()
    {
        if (dialogStatus == 2 && currentStage.IsLevelUp()) LevelUp();
    }

    public int GetLevel()
    {
        return gameLevel;
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
        audioMixer.SetFloat("BGM", currentSliderBGMValue);
    }

    public void OnValueChangedSliderSE()
    {
        currentSliderSEValue = sliderSE.value;
        audioMixer.SetFloat("SE", currentSliderSEValue);
    }

    //DisplayScoreボタン(見えない)を押した時に呼び出されるメソッド
    public void PushDisplayScoreButton()
    {
        displayGameScore = gameScore;
        displayHighScore = highScore;
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
        SceneManager.LoadScene("TitleScene");
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
        gameScore += s;
        if (gameScore > MAX_SCORE)
        {
            gameScore = MAX_SCORE;
        }
        else if (gameScore < 0)
        {
            gameScore = 0;
        }

        if (gameScore > highScore)
        {
            highScore = gameScore;
        }
        else if (highScore < 0)
        {
            highScore = 0;
        }
    }

    public void AddRestOfBall()
    {
        if (restOfBall < MAX_REST_OF_BALL) restOfBall++;
    }

    void DrawUIDisplay()
    {
        float[] positionsXforMainViewPanel = { 2048.0f, 1024.0f, 0.0f, -1024.0f, -2048.0f };

        //High-Score表示部（本番プレイ以外ではハイフン表示となる）
        if (isPerformancePlay) for (int i = 0; i < 6; i++) imagesHighScoreNumber[i].sprite = spritesYellowNumber[displayHighScore / (int)Mathf.Pow(10, i) % 10];
        else for (int i = 0; i < 6; i++) imagesHighScoreNumber[i].sprite = spritesYellowNumber[10];

        for (int i = 0; i < 6; i++) imagesGameScoreNumber[i].sprite = spritesYellowNumber[displayGameScore / (int)Mathf.Pow(10, i) % 10];

        imagesGameLevelNumber[0].sprite = spritesYellowNumber[gameLevel % 10];
        imagesGameLevelNumber[1].sprite = spritesYellowNumber[gameLevel / 10 % 10];

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
        }
        else if (currentStage.IsLevelUpFailZone())
        {
            LevelUp();
        }
        else
        {
            missTelop.SetActive(true);
            theBall.DiminishForMissing();
        }

    }

    public IEnumerator GenerateStage()
    {
        GameObject[] gameObjectsStage = GameObject.FindGameObjectsWithTag("Stage");
        foreach (GameObject gameObjectStage in gameObjectsStage) Destroy(gameObjectStage);
        yield return null;

        Instantiate(prefabsStage[gameLevel], new Vector3(0.0f, 0.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
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
    }

    public void MoveToEndScene()
    {
        gameScoreForEndScene = gameScore;
        highScoreForEndScene = highScore;
        playerIdForEndScene = playerId;
        gameLevelForEndScene = gameLevel;
        isPerformancePlayForEndScene = isPerformancePlay;
        SceneManager.LoadScene("EndScene");
    }

    public void OnAnimationEndFromLevelUpTelop()
    {
        levelUpTelop.SetActive(false);
        levelUpBonusTelop.SetActive(false);

        theRacket.SetStepOfLength(3);
        gameLevel++;
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
            theRacket.Rebound();
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
        theRacket.Rebound();
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