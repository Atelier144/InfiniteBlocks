using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBlockBreakEffect : MonoBehaviour {

    Color[] theColors =
    {
        new Color(1.0f, 0.0f,  0.0f,  1.0f), //Red(0)
        new Color(1.0f, 0.5f,  0.0f,  1.0f), //Orange(1)
        new Color(1.0f, 1.0f,  0.0f,  1.0f), //Yellow(2)
        new Color(0.0f, 1.0f,  0.25f, 1.0f), //Green(3)
        new Color(0.0f, 0.75f, 1.0f,  1.0f), //Blue(4)
        new Color(0.0f, 0.0f,  1.0f,  1.0f), //Indigo(5)
        new Color(0.5f, 0.0f,  1.0f,  1.0f),  //Violet(6)
        new Color(0.75f, 1.0f, 0.0f, 1.0f), // YellowGreen(7)
        new Color(0.8f, 0.4f, 0.0f, 1.0f),  //Brown(8)
        new Color(1.0f, 1.0f, 1.0f, 1.0f), // White(9)
        new Color(0.8f, 0.8f, 1.0f, 1.0f), //Silver(10)
        new Color(1.0f, 0.8f, 0.0f, 1.0f), //Gold(11)
        new Color(0.0f, 0.0f, 0.0f, 1.0f), //Black(12)
        new Color(1.0f, 0.0f, 0.75f, 1.0f), //LevelUp(13)
        new Color(1.0f, 0.0f, 1.0f, 1.0f), //Protector(14)
        new Color(0.0f, 0.5f, 1.0f, 1.0f) //TrapGuard(15)
    };

    int fadeCount = 50;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        fadeCount--;
        if(fadeCount > 0)
        {
            Color theColor = this.GetComponent<SpriteRenderer>().color;
            theColor.a = fadeCount * 0.02f;
            this.GetComponent<SpriteRenderer>().color = theColor;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Initialize(int colorCode)
    {
        this.GetComponent<SpriteRenderer>().color = theColors[colorCode];
    }
}
