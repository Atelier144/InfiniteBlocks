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
    [SerializeField] GameObject gameObjectThePanels;

    [SerializeField] Button buttonLevelSelect;
    [SerializeField] Button buttonHighScoreReset;
    [SerializeField] Button buttonReturnToTopForLevelSelect;
    [SerializeField] Button buttonReturnToTopForHighScoreReset;
    [SerializeField] Button[] buttonsStart = new Button[31];

    [SerializeField] Text textButtonLevelSelect;
    [SerializeField] Text textButtonHighScoreReset;
    [SerializeField] Text textButtonReturnToTopForLevelSelect;
    [SerializeField] Text textButtonReturnToTopForHighScoreReset;
    [SerializeField] Text[] textsButtonsStart = new Text[31];

    [SerializeField] Text textHighScore;
    [SerializeField] Text textGuestMode;
    [SerializeField] Text textMaxLevel;

    [SerializeField] Font fontForEnglish;
    [SerializeField] Font fontForJapanese;

    [SerializeField] Sprite spriteButtonEnabled;
    [SerializeField] Sprite spriteButtonDisabled;
    [SerializeField] Sprite spriteSmallButtonEnabled;
    [SerializeField] Sprite spriteSmallButtonDisabled;

    public static int highScoreForMainScene = 0;
    public static int gameLevelForMainScene = 0;
    public static int playerIdForMainScene = 0;

    int highScore = 0;
    int maxLevel = 1;
    int playerId = 0;   // 0: Guset Mode
    string languageName = "Japanese";

	// Use this for initialization
	void Start () {
        highScore = 0;
        maxLevel = 25;
        playerId = 1;

        textHighScore.text = "HIGH SCORE : " + highScore.ToString("D6");
        textMaxLevel.text = "MAX LEVEL : " + maxLevel.ToString("D2");

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
                textButtonReturnToTopForLevelSelect.font = fontForJapanese;
                textButtonReturnToTopForLevelSelect.text = "戻る";
                textButtonReturnToTopForHighScoreReset.font = fontForJapanese;
                textButtonReturnToTopForHighScoreReset.text = "戻る";
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushStartButton(int startLevel) 
    {
        Global.highScore = highScore;
        Global.playerId = playerId;
        Global.level = startLevel;
        SceneManager.LoadScene("MainScene");
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

    }

    public void PushReturnToTopButton()
    {
        gameObjectThePanels.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }
}
