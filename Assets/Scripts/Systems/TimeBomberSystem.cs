using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomberSystem : MonoBehaviour {

    MainManager mainManager;
    SignalManager signalManager;

    Ball ball;

    [SerializeField] GameObject[] gameObjectsNumbers = new GameObject[4];
    [SerializeField] GameObject gameObjectColon;
    [SerializeField] GameObject gameObjectSignal;

    [SerializeField] Sprite[] spritesNumbers = new Sprite[10];
    [SerializeField] Sprite spriteSignalRed;
    [SerializeField] Sprite spriteSignalGreen;
    [SerializeField] Sprite spriteSignalCyaan;
    [SerializeField] Sprite spriteColonOn;
    [SerializeField] Sprite spriteColonOff;

    int bombingCount;

    SpriteRenderer[] spriteRenderersNumbers = new SpriteRenderer[4];
    SpriteRenderer spriteRendererColon;
    SpriteRenderer spriteRendererSignal;

    Color colorRed = new Color(1.0f, 0.2f, 0.2f);
    Color colorGreen = new Color(0.2f, 1.0f, 0.2f);

    AudioSource audioSource;

    int countingCount = 50;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        ball = GameObject.Find("TheBall").GetComponent<Ball>();

        for (int i = 0; i < 4; i++) spriteRenderersNumbers[i] = gameObjectsNumbers[i].GetComponent<SpriteRenderer>();
        spriteRendererColon = gameObjectColon.GetComponent<SpriteRenderer>();
        spriteRendererSignal = gameObjectSignal.GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();

        bombingCount = mainManager.GetTimeBomberCount();
    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderersNumbers[0].sprite = spritesNumbers[bombingCount % 10];
        spriteRenderersNumbers[1].sprite = spritesNumbers[bombingCount / 10 % 6];
        spriteRenderersNumbers[2].sprite = spritesNumbers[bombingCount / 60 % 10];
        spriteRenderersNumbers[3].sprite = spritesNumbers[bombingCount / 600 % 10];
        spriteRendererColon.sprite = countingCount < 25 ? spriteColonOff : spriteColonOn;
        if (mainManager.GetDialogStatus() == 2)
        {
            if (signalManager.IsActiveTrapGuard())
            {
                spriteRendererSignal.sprite = spriteSignalCyaan;
            }
            else
            {
                spriteRendererSignal.sprite = spriteSignalRed;
            }
        }
        else
        {
            spriteRendererSignal.sprite = spriteSignalGreen;
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
Lv18で「144」を揃える：20分
*/