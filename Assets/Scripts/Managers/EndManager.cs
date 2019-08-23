using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour {

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

    int gameScore;
    int highScore;
    int playerId;
    int gameLevel;

    string languageName = "Japanese";

    bool isPerformancePlay;

	// Use this for initialization
	void Start () {
        gameScore = Global.score;
        highScore = Global.highScore;
        playerId = Global.playerId;
        gameLevel = Global.level;
        isPerformancePlay = Global.isPerformancePlay;

        if(playerId != 0 && isPerformancePlay && gameScore > 0)
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

        if(isPerformancePlay && highScore < gameScore)
        {
            imageScore.sprite = spriteNewRecord;
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
	void Update () {
		
	}

    void DrawResultScore()
    {
        for (int i = 0; i < 6; i++) imagesResultScoreNumber[i].sprite = spritesResultScoreNumber[gameScore / (int)Mathf.Pow(10, i) % 10];
    }

    public void OnClickButtonRecord()
    {
        Debug.Log("UserID:" + playerId + " Score:" + gameScore + " Level:" + gameLevel);
    }

    public void OnClickButtonContinue()
    {
        Global.highScore = highScore;
        Global.playerId = playerId;
        Global.level = gameLevel;
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickButtonReturn()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
