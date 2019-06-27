using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System.Runtime.InteropServices;

public class TitleManager : MonoBehaviour {

    Color colorEnabledGreen = new Color(0.0f, 0.5f, 0.125f, 1.0f);
    Color colorDisabledBlack = new Color(0.2f, 0.2f, 0.2f, 0.5f);

    //[DllImport("__Internal")]
    //private static extern int GetNumberFromUnity();

    //[DllImport("__Internal")]
    //private static extern void SetNumberFromUnity(int s);
    [SerializeField] GameObject thePanels;
    [SerializeField] Image imageLevelNumber0;
    [SerializeField] Image imageLevelNumber1;

    [SerializeField] Button buttonStart;
    [SerializeField] Button buttonLevelSelect;
    [SerializeField] Button buttonHighScoreReset;
    [SerializeField] Button buttonReturnToTopForLevelSelect;
    [SerializeField] Button buttonReturnToTopForHighScoreReset;
    [SerializeField] Button[] buttonsIntermediateStart = new Button[31];

    [SerializeField] Button buttonLevelUp;
    [SerializeField] Button buttonLevelDown;

    [SerializeField] Text textButtonStart;
    [SerializeField] Text textButtonLevelSelect;
    [SerializeField] Text textButtonHighScoreReset;
    [SerializeField] Text textButtonReturnToTopForLevelSelect;
    [SerializeField] Text textButtonReturnToTopForHighScoreReset;
    [SerializeField] Text[] textsButtonsIntermediateStart = new Text[31];

    [SerializeField] Text textHighScore;
    [SerializeField] Text textGuestMode;
    [SerializeField] Text textMaxLevel;

    [SerializeField] Font fontForEnglish;
    [SerializeField] Font fontForJapanese;

    [SerializeField] Sprite[] spritesBigNumber = new Sprite[10];
    [SerializeField] Sprite spriteButtonEnabled;
    [SerializeField] Sprite spriteButtonDisabled;
    [SerializeField] Sprite spriteButtonLevelUpEnabled;
    [SerializeField] Sprite spriteButtonLevelUpDisabled;
    [SerializeField] Sprite spriteButtonLevelDownEnabled;
    [SerializeField] Sprite spriteButtonLevelDownDisabled;

    public static int highScoreForMainScene = 0;
    public static int gameLevelForMainScene = 1;
    public static int playerIdForMainScene = 0;

    int highScore = 0;
    int maxLevel = 1;
    int selectLevel = 1;
    int playerId = 0;   // 0: Guset Mode
    string languageName = "Japanese";

	// Use this for initialization
	void Start () {
        highScore = 0;
        maxLevel = 30;
        playerId = 1;

        textHighScore.text = "HIGH SCORE : " + highScore.ToString("D6");
        textMaxLevel.text = "MAX LEVEL : " + maxLevel.ToString("D2");
        if (playerId == 0)
        {
            textGuestMode.text = "GUEST MODE";
        }
        else
        {
            textGuestMode.text = "";
        }

        if (highScore > 0)
        {
            buttonHighScoreReset.interactable = true;
            buttonHighScoreReset.image.sprite = spriteButtonEnabled;
            textButtonHighScoreReset.color = colorEnabledGreen;
        }
        else
        {
            buttonHighScoreReset.interactable = false;
            buttonHighScoreReset.image.sprite = spriteButtonDisabled;
            textButtonHighScoreReset.color = colorDisabledBlack;
        }

        if (maxLevel >= 2)
        {
            buttonLevelSelect.interactable = true;
            buttonLevelSelect.image.sprite = spriteButtonEnabled;
            textButtonLevelSelect.color = colorEnabledGreen;
        }
        else
        {
            buttonLevelSelect.interactable = false;
            buttonLevelSelect.image.sprite = spriteButtonDisabled;
            textButtonLevelSelect.color = colorDisabledBlack;
        }

        if(languageName == "Japanese")
        {
            textButtonStart.font = fontForJapanese;
            textButtonStart.text = "スタート";
            textButtonLevelSelect.font = fontForJapanese;
            textButtonLevelSelect.text = "中間レベルから";
            textButtonHighScoreReset.font = fontForJapanese;
            textButtonHighScoreReset.text = "ハイスコアリセット";
            textButtonReturnToTopForLevelSelect.font = fontForJapanese;
            textButtonReturnToTopForLevelSelect.text = "戻る";
            textButtonReturnToTopForHighScoreReset.font = fontForJapanese;
            textButtonReturnToTopForHighScoreReset.text = "戻る";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushStartButton() 
    {
        highScoreForMainScene = highScore;
        gameLevelForMainScene = 1;
        playerIdForMainScene = playerId;
        SceneManager.LoadScene("MainScene");
    }

    public void PushLevelSelectButton()
    {
        selectLevel = maxLevel;
        DrawLevelNumbers();
        thePanels.transform.localPosition = new Vector3(0.0f, -1000.0f, 0.0f);
    }

    public void PushResetHighScoreButton()
    {
        thePanels.transform.localPosition = new Vector3(0.0f, -2000.0f, 0.0f);
    }

    public void PushLevelDownButton()
    {
        selectLevel--;
        DrawLevelNumbers();
    }

    public void PushLevelUpButton()
    {
        selectLevel++;
        DrawLevelNumbers();
    }

    public void PushIntermediateStartButton(int startLevel)
    {
        highScoreForMainScene = highScore;
        gameLevelForMainScene = startLevel;
        playerIdForMainScene = playerId;
        SceneManager.LoadScene("MainScene");
    }

    public void PushAcceptResetHighScoreButton()
    {

    }

    public void PushReturnToTopButton()
    {
        thePanels.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    void DrawLevelNumbers()
    {
        int displayNumber0 = selectLevel % 10;
        int displayNumber1 = selectLevel / 10 % 10;
        imageLevelNumber0.sprite = spritesBigNumber[displayNumber0];
        imageLevelNumber1.sprite = spritesBigNumber[displayNumber1];
        if(selectLevel <= 2)
        {
            buttonLevelDown.interactable = false;
            buttonLevelDown.image.sprite = spriteButtonLevelDownDisabled;
        }
        else
        {
            buttonLevelDown.interactable = true;
            buttonLevelDown.image.sprite = spriteButtonLevelDownEnabled;
        }

        if(selectLevel >= maxLevel)
        {
            buttonLevelUp.interactable = false;
            buttonLevelUp.image.sprite = spriteButtonLevelUpDisabled;
        }
        else
        {
            buttonLevelUp.interactable = true;
            buttonLevelUp.image.sprite = spriteButtonLevelUpEnabled;
        }
    }
}
