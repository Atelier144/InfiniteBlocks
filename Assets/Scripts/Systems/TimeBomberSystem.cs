using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomberSystem : MonoBehaviour {

    MainManager mainManager;
    SignalManager signalManager;

    Ball ball;

    [SerializeField] GameObject[] gameObjectsNumbers = new GameObject[4];
    [SerializeField] GameObject gameObjectColon;

    [SerializeField] Sprite[] spritesNumbersRed = new Sprite[10];
    [SerializeField] Sprite[] spritesNumbersGreen = new Sprite[10];
    [SerializeField] Sprite[] spritesNumbersCyaan = new Sprite[10];

    [SerializeField] Sprite spriteColonRed;
    [SerializeField] Sprite spriteColonGreen;
    [SerializeField] Sprite spriteColonCyaan;
    [SerializeField] Sprite spriteColonOff;

    int bombingCount;

    SpriteRenderer[] spriteRenderersNumbers = new SpriteRenderer[4];
    SpriteRenderer spriteRendererColon;

    AudioSource audioSource;

    int countingCount = 50;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        ball = GameObject.Find("TheBall").GetComponent<Ball>();

        for (int i = 0; i < 4; i++) spriteRenderersNumbers[i] = gameObjectsNumbers[i].GetComponent<SpriteRenderer>();
        spriteRendererColon = gameObjectColon.GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        bombingCount = mainManager.GetTimeBomberCount();
    }
	
	// Update is called once per frame
	void Update () {


        if (mainManager.GetDialogStatus() == 2)
        {
            if (signalManager.IsActiveTrapGuard())
            {
                spriteRenderersNumbers[0].sprite = spritesNumbersCyaan[bombingCount % 10];
                spriteRenderersNumbers[1].sprite = spritesNumbersCyaan[bombingCount / 10 % 6];
                spriteRenderersNumbers[2].sprite = spritesNumbersCyaan[bombingCount / 60 % 10];
                spriteRenderersNumbers[3].sprite = spritesNumbersCyaan[bombingCount / 600 % 10];
                spriteRendererColon.sprite = spriteColonCyaan;
            }
            else
            {
                spriteRenderersNumbers[0].sprite = spritesNumbersRed[bombingCount % 10];
                spriteRenderersNumbers[1].sprite = spritesNumbersRed[bombingCount / 10 % 6];
                spriteRenderersNumbers[2].sprite = spritesNumbersRed[bombingCount / 60 % 10];
                spriteRenderersNumbers[3].sprite = spritesNumbersRed[bombingCount / 600 % 10];
                spriteRendererColon.sprite = countingCount < 25 ? spriteColonOff : spriteColonRed;
            }
        }
        else
        {
            spriteRenderersNumbers[0].sprite = spritesNumbersGreen[bombingCount % 10];
            spriteRenderersNumbers[1].sprite = spritesNumbersGreen[bombingCount / 10 % 6];
            spriteRenderersNumbers[2].sprite = spritesNumbersGreen[bombingCount / 60 % 10];
            spriteRenderersNumbers[3].sprite = spritesNumbersGreen[bombingCount / 600 % 10];
            spriteRendererColon.sprite = spriteColonGreen;
        }
    }

    private void FixedUpdate()
    {

        if (mainManager.GetDialogStatus() == 2 && !signalManager.IsActiveTrapGuard())
        {
            if (countingCount > 0) countingCount--;
            else
            {
                countingCount = 50;
                if (bombingCount > 0)
                {
                    bombingCount--;
                    audioSource.time = 0.1f;
                    audioSource.Play();
                }
                else mainManager.StartExplosion();

            }
        }
        else countingCount = 50;
    }
}

/*時限爆弾の初期時間
途中レベルからのプレイ：10分
正規プレイ（ゲストモード）：12分
正規プレイ（メンバーモード）：15分
Lv18で「144」を揃える：30分
*/