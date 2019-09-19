using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static int score;
    static int highScore;
    static int level;
    static int playerId;
    static bool isPerformancePlay;
    static string languageName;


    virtual protected void Start()
    {
        playerId = 1;
        languageName = "Japanese";
    }

    virtual protected void Update()
    {

    }

    protected int GetScore()
    {
        return score;
    }

    protected int GetHighScore()
    {
        return highScore;
    }

    protected int GetLevel()
    {
        return level;
    }

    protected int GetPlayerId()
    {
        return playerId;
    }

    protected bool IsPerformancePlay()
    {
        return isPerformancePlay;
    }

    protected string GetLanguageName()
    {
        return languageName;
    }

    protected void ChangeSceneToTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }


    protected void ChangeSceneToMainScene(int level, int highScore)
    {
        GameManager.level = level;
        GameManager.highScore = highScore;
        SceneManager.LoadScene("MainScene");
    }

    protected void ChangeSceneToEndScene(int score, int highScore, int level, bool isPerformancePlay)
    {
        GameManager.score = score;
        GameManager.highScore = highScore;
        GameManager.level = level;
        GameManager.isPerformancePlay = isPerformancePlay;

        SceneManager.LoadScene("EndScene");
    }
}
