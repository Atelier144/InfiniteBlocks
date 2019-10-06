using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class GameManager : MonoBehaviour
{

    [DllImport("__Internal")] public static extern int GetUserIdFromJS();
    [DllImport("__Internal")] public static extern string GetLanguageFromJS();
    [DllImport("__Internal")] public static extern void SendInfiniteBlocksRecordFromJS(int userId, int score, int level);
    [DllImport("__Internal")] public static extern int GetInfiniteBlocksHighScoreFromJS();
    [DllImport("__Internal")] public static extern void SetInfiniteBlocksHighScoreFromJS(int highScore);
    [DllImport("__Internal")] public static extern void DestroyInfiniteBlocksHighScoreFromJS();
    [DllImport("__Internal")] public static extern int GetInfiniteBlocksMaxLevelFromJS();
    [DllImport("__Internal")] public static extern void SetInfiniteBlocksMaxLevelFromJS(int maxLevel);
    [DllImport("__Internal")] public static extern void DestroyInfiniteBlocksMaxLevelFromJS();

    static int score;
    static int highScore;
    static int level;
    static int maxLevel;
    static int playerId;
    static bool isPerformancePlay;
    static string languageName;


    virtual protected void Start()
    {
        playerId = GetUserIdFromJS();
        languageName = GetLanguageFromJS();
        maxLevel = GetMaxLevel();
        highScore = GetHighScore();
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

    protected int GetMaxLevel()
    {
        return maxLevel;
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
