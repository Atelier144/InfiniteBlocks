using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBomberSystem : MonoBehaviour {

    MainManager mainManager;
    Ball ball;

    [SerializeField] GameObject[] gameObjectsNumbers = new GameObject[4];
    [SerializeField] GameObject gameObjectColon;
    [SerializeField] GameObject gameObjectSignal;

    [SerializeField] Sprite[] spritesNumbers = new Sprite[10];
    [SerializeField] Sprite spriteColonOn;
    [SerializeField] Sprite spriteColonOff;

    [SerializeField] int bombingCount;

    SpriteRenderer[] spriteRenderersNumbers = new SpriteRenderer[4];
    SpriteRenderer spriteRendererColon;
    SpriteRenderer spriteRendererSignal;

    Color colorRed = new Color(1.0f, 0.2f, 0.2f);
    Color colorGreen = new Color(0.2f, 1.0f, 0.2f);

    int countingCount = 50;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        ball = GameObject.Find("TheBall").GetComponent<Ball>();

        for (int i = 0; i < 4; i++) spriteRenderersNumbers[i] = gameObjectsNumbers[i].GetComponent<SpriteRenderer>();
        spriteRendererColon = gameObjectColon.GetComponent<SpriteRenderer>();
        spriteRendererSignal = gameObjectSignal.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        spriteRenderersNumbers[0].sprite = spritesNumbers[bombingCount % 10];
        spriteRenderersNumbers[1].sprite = spritesNumbers[bombingCount / 10 % 6];
        spriteRenderersNumbers[2].sprite = spritesNumbers[bombingCount / 60 % 10];
        spriteRenderersNumbers[3].sprite = spritesNumbers[bombingCount / 600 % 10];
        spriteRendererColon.sprite = countingCount < 25 ? spriteColonOff : spriteColonOn;
        spriteRendererSignal.color = mainManager.GetDialogStatus() == 2 ? colorRed : colorGreen;
    }

    private void FixedUpdate()
    {

        if (mainManager.GetDialogStatus() == 2)
        {
            if (countingCount > 0) countingCount--;
            else
            {
                countingCount = 50;
                if (bombingCount > 0) bombingCount--;
                else mainManager.StartExplosion();

            }
        }
        else countingCount = 50;
    }
}

/*時限爆弾の初期時間
 * 途中レベルからスタート：8分追加、正統派プレイ：10分追加
 * メンバーモード：2分追加
 * 全てのブロックを壊してレベルアップ：1分追加
 * エクストラボールを取り逃がす：5分追加
 * 有効なアイテムをキャンセル：10秒追加
 * プロテクターに触れる：10秒削減
 * リプレイタイム以内のボール落下：30秒削減
 * 
 * 最小時間：5分
 * 最大時間：30分
*/