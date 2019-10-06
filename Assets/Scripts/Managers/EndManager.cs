using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : GameManager {

    Color colorEnabledGreen = new Color(0.0f, 0.5f, 0.125f, 1.0f);
    Color colorDisabledBlack = new Color(0.2f, 0.2f, 0.2f, 0.5f);

    [SerializeField] Image[] imagesResultScoreNumber = new Image[6];
    [SerializeField] Image imageScore;

    [SerializeField] Button buttonRecord;
    [SerializeField] Button buttonContinue;
    [SerializeField] Button buttonReturn;

    [SerializeField] Text textButtonRecord;
    [SerializeField] Text textButtonContinue;
    [SerializeField] Text textButtonReturn;

    [SerializeField] Sprite[] spritesResultScoreNumber = new Sprite[10];
    [SerializeField] Sprite spriteButtonEnabled;
    [SerializeField] Sprite spriteButtonDisabled;

    [SerializeField] Sprite spriteNewRecord;

    [SerializeField] Font fontForEnglish;
    [SerializeField] Font fontForJapanese;

    int score;
    int highScore;
    int playerId;
    int level;
    int maxLevel;

    string languageName;
    bool isPerformancePlay;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        score = base.GetScore();
        highScore = base.GetHighScore();
        playerId = base.GetPlayerId();
        level = base.GetLevel();
        maxLevel = base.GetMaxLevel();
        languageName = base.GetLanguageName();
        isPerformancePlay = base.IsPerformancePlay();

        if(playerId != 0 && isPerformancePlay && score > 0)
        {
            buttonRecord.interactable = true;
            buttonRecord.image.sprite = spriteButtonEnabled;
            textButtonRecord.color = colorEnabledGreen;
        }
        else
        {
            buttonRecord.interactable = false;
            buttonRecord.image.sprite = spriteButtonDisabled;
            textButtonRecord.color = colorDisabledBlack;
        }

        if(isPerformancePlay && score > highScore)
        {
            SetInfiniteBlocksHighScoreFromJS(score);
            imageScore.sprite = spriteNewRecord;
        }

        if (level > maxLevel)
        {
            SetInfiniteBlocksMaxLevelFromJS(level);
        }

        switch (languageName)
        {
            case "Japanese":
                textButtonRecord.font = fontForJapanese;
                textButtonRecord.text = "ランキングに登録";
                textButtonContinue.font = fontForJapanese;
                textButtonContinue.text = "コンティニュー";
                textButtonReturn.font = fontForJapanese;
                textButtonReturn.text = "タイトルに戻る";
                break;
        }
        DrawResultScore();
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void DrawResultScore()
    {
        for (int i = 0; i < 6; i++) imagesResultScoreNumber[i].sprite = spritesResultScoreNumber[score / (int)Mathf.Pow(10, i) % 10];
    }

    public void OnClickButtonRecord()
    {
        SendInfiniteBlocksRecordFromJS(playerId, score, level);
    }

    public void OnClickButtonContinue()
    {
        base.ChangeSceneToMainScene(level, highScore);
    }

    public void OnClickButtonReturn()
    {
        base.ChangeSceneToTitleScene();
    }
}
