using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour {

    Color colorEnabledGreen = new Color(0.0f, 0.5f, 0.125f, 1.0f);
    Color colorDisabledBlack = new Color(0.2f, 0.2f, 0.2f, 0.5f);

    [SerializeField] Image[] imagesResultScoreNumber = new Image[6];
    [SerializeField] Button buttonRecord;
    [SerializeField] Button buttonContinue;
    [SerializeField] Button buttonReturn;

    [SerializeField] Text textButtonRecord;
    [SerializeField] Text textButtonContinue;
    [SerializeField] Text textButtonReturn;

    [SerializeField] Sprite[] spritesResultScoreNumber = new Sprite[10];
    [SerializeField] Sprite spriteButtonEnabled;
    [SerializeField] Sprite spriteButtonDisabled;

    [SerializeField] Font fontForEnglish;
    [SerializeField] Font fontForJapanese;

    int gameScore;
    int highScore;
    int playerId;
    int gameLevel;

    string languageName = "Japanese";

    bool isPerformancePlay;

	// Use this for initialization
	void Start () {
        gameScore = MainManager.gameScoreForEndScene;
        highScore = MainManager.highScoreForEndScene;
        playerId = 1;//MainManager.playerIdForEndScene;
        gameLevel = 2; //MainManager.gameScoreForEndScene;
        isPerformancePlay = false;//MainManager.isPerformancePlayForEndScene;

        if(playerId == 0)
        {
            buttonRecord.interactable = false;
            buttonRecord.image.sprite = spriteButtonDisabled;
        }
        else if (isPerformancePlay)
        {
            buttonRecord.interactable = true;
            buttonRecord.image.sprite = spriteButtonEnabled;
        }
        else
        {
            buttonRecord.interactable = false;
            buttonRecord.image.sprite = spriteButtonDisabled;
        }

        if(languageName == "Japanese")
        {
            textButtonRecord.font = fontForJapanese;
            textButtonRecord.text = "ランキングに登録";
            textButtonContinue.font = fontForJapanese;
            textButtonContinue.text = "コンティニュー";
            textButtonReturn.font = fontForJapanese;
            textButtonReturn.text = "タイトルに戻る";
        }
        DrawResultScore();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DrawResultScore()
    {
        for (int i = 0; i < 6; i++)
        {
            imagesResultScoreNumber[i].sprite = spritesResultScoreNumber[gameScore / (int)Mathf.Pow(10, i) % 10];
        }
    }

    public void PushRecordButton()
    {

    }

    public void PushReturnButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
