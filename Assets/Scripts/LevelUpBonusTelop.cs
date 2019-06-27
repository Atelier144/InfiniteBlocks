using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBonusTelop : MonoBehaviour {

    [SerializeField] GameObject bonusTelop;
    [SerializeField] GameObject numbersTelop;
    [SerializeField] GameObject numberTelop0;
    [SerializeField] GameObject numberTelop1;
    [SerializeField] GameObject numberTelop2;
    [SerializeField] GameObject numberTelop3;
    [SerializeField] GameObject numberTelop4;

    [SerializeField] Sprite[] spritesNumbers = new Sprite[10];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBonusScore(int bonusScore)
    {
        if(bonusScore >= 100000)
        {
            Debug.Log("ERROR:100000以上のボーナススコアは設定できません");
        }
        else if(bonusScore >= 10000)
        {
            int number0 = bonusScore % 10;
            int number1 = bonusScore / 10 % 10;
            int number2 = bonusScore / 100 % 10;
            int number3 = bonusScore / 1000 % 10;
            int number4 = bonusScore / 10000 % 10;

            numberTelop0.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number0];
            numberTelop1.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number1];
            numberTelop2.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number2];
            numberTelop3.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number3];
            numberTelop4.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number4];
        }
        else if(bonusScore >= 1000)
        {
            int number0 = bonusScore % 10;
            int number1 = bonusScore / 10 % 10;
            int number2 = bonusScore / 100 % 10;
            int number3 = bonusScore / 1000 % 10;

            numberTelop0.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number0];
            numberTelop1.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number1];
            numberTelop2.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number2];
            numberTelop3.GetComponent<SpriteRenderer>().sprite = spritesNumbers[number3];
            numberTelop4.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.Log("ERROR:1000未満のボーナススコアは設定できません");
        }
    }
}
