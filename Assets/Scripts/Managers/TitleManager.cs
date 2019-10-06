using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//using System.Runtime.InteropServices;

public class TitleManager : GameManager {

    Color colorEnabledGreen = new Color(0.0f, 0.5f, 0.125f, 1.0f);
    Color colorDisabledBlack = new Color(0.2f, 0.2f, 0.2f, 0.5f);

    [SerializeField] GameObject gameObjectThePanels;

    [SerializeField] Button buttonLevelSelect;
    [SerializeField] Button buttonHighScoreReset;
    [SerializeField] Button buttonReturnToTopForLevelSelect;
    [SerializeField] Button buttonReturnToTopForHighScoreReset;
    [SerializeField] Button[] buttonsStart = new Button[31];

    [SerializeField] Text textButtonLevelSelect;
    [SerializeField] Text textButtonHighScoreReset;
    [SerializeField] Text textButtonAcceptHighScoreReset;
    [SerializeField] Text textButtonReturnToTopForLevelSelect;
    [SerializeField] Text textButtonReturnToTopForHighScoreReset;
    [SerializeField] Text[] textsButtonsStart = new Text[31];

    [SerializeField] Text textSelectLevel;
    [SerializeField] Text textHighScoreResetNotice;

    [SerializeField] Text textHighScore;
    [SerializeField] Text textGuestMode;

    [SerializeField] Font fontForEnglish;
    [SerializeField] Font fontForJapanese;

    [SerializeField] Sprite spriteButtonEnabled;
    [SerializeField] Sprite spriteButtonDisabled;
    [SerializeField] Sprite spriteSmallButtonEnabled;
    [SerializeField] Sprite spriteSmallButtonDisabled;


    int highScore;
    int maxLevel;
    int playerId;   // 0: Guset Mode
    string languageName;

	// Use this for initialization
	protected override void Start () {
        base.Start();

        highScore = base.GetHighScore();
        maxLevel = base.GetMaxLevel();
        playerId = base.GetPlayerId();
        languageName = base.GetLanguageName();

        textHighScore.text = "HIGH SCORE : " + highScore.ToString("D6");

        if (playerId == 0) textGuestMode.text = "GUEST MODE";
        else textGuestMode.text = "";

        for (int i = 2; i <= maxLevel && i <= 30; i++)
        {
            buttonsStart[i].image.sprite = spriteSmallButtonEnabled;
            buttonsStart[i].interactable = true;
            textsButtonsStart[i].color = colorEnabledGreen;
        }

        switch (languageName)
        {
            case "Japanese":
                textsButtonsStart[0].font = fontForJapanese;
                textsButtonsStart[0].text = "スタート";
                textButtonLevelSelect.font = fontForJapanese;
                textButtonLevelSelect.text = "中間レベルから";
                textButtonHighScoreReset.font = fontForJapanese;
                textButtonHighScoreReset.text = "ハイスコアリセット";
                textButtonAcceptHighScoreReset.font = fontForJapanese;
                textButtonAcceptHighScoreReset.text = "リセット";
                textButtonReturnToTopForLevelSelect.font = fontForJapanese;
                textButtonReturnToTopForLevelSelect.text = "戻る";
                textButtonReturnToTopForHighScoreReset.font = fontForJapanese;
                textButtonReturnToTopForHighScoreReset.text = "戻る";
                textSelectLevel.font = fontForJapanese;
                textSelectLevel.text = "開始レベルを選択してください";
                textHighScoreResetNotice.font = fontForJapanese;
                textHighScoreResetNotice.lineSpacing = 1.35f;
                textHighScoreResetNotice.text = "本当にハイスコアをリセットしますか？\n(リセットした記録は元に戻せません)";
                break;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public void PushStartButton(int startLevel) 
    {
        base.ChangeSceneToMainScene(startLevel, highScore);
    }

    public void PushLevelSelectButton()
    {
        gameObjectThePanels.transform.localPosition = new Vector3(0.0f, -1000.0f, 0.0f);
    }

    public void PushResetHighScoreButton()
    {
        gameObjectThePanels.transform.localPosition = new Vector3(0.0f, -2000.0f, 0.0f);
    }

    public void PushAcceptResetHighScoreButton()
    {
        DestroyInfiniteBlocksHighScoreFromJS();
        gameObjectThePanels.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void PushReturnToTopButton()
    {
        gameObjectThePanels.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
