using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenNavigationManager : MonoBehaviour
{

    [SerializeField] Color colorPrecipitate;
    [SerializeField] Color colorShooting;
    [SerializeField] Color colorSticky;

    [SerializeField] Text textScreenNavigation1;
    [SerializeField] Text textScreenNavigation2;

    [SerializeField] Font fontForJapanese;

    string languageName = "Japanese";

    string messageShooting = "CLICK ON THE SCREEN TO SHOOT A BULLET";
    string messagePrecipitate = "CLICK ON THE SCREEN TO DROP THE BALL";
    string messageSticky = "CLICK ON THE SCREEN TO DETACH THE BALL";

    string currentTextScreenNavigation1 = "";
    string currentTextScreenNavigation2 = "";

    bool isPrecipitate;
    bool isShooting;
    bool isSticky;

    int frameCount;
    // Start is called before the first frame update
    void Start()
    {
        switch (languageName)
        {
            case "Japanese":
                messagePrecipitate = "画面をクリックしてボールを急降下";
                messageShooting = "画面をクリックして弾を発射";
                messageSticky = "画面をクリックしてボールを反射";
                textScreenNavigation1.font = fontForJapanese;
                textScreenNavigation2.font = fontForJapanese;
                break;
        }
        DrawUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(frameCount % 120 == 0)
        {
            textScreenNavigation1.text = currentTextScreenNavigation1;
            textScreenNavigation2.text = currentTextScreenNavigation2;
        }
        if (frameCount % 120 == 60)
        {
            textScreenNavigation1.text = "";
            textScreenNavigation2.text = "";
        }
        frameCount++;
    }

    public void SetPrecipitate(bool s)
    {
        isPrecipitate = s;
        DrawUI();
    }
    public void SetShooting(bool s)
    {
        isShooting = s;
        DrawUI();
    }

    public void SetSticky(bool s)
    {
        isSticky = s;
        DrawUI();
    }

    void DrawUI()
    {
        frameCount = 0;
        if (isSticky && isShooting)
        {
            currentTextScreenNavigation1 = messageSticky;
            currentTextScreenNavigation2 = messageShooting;
            textScreenNavigation1.color = colorSticky;
            textScreenNavigation2.color = colorShooting;
        }
        else if (isSticky)
        {
            currentTextScreenNavigation1 = messageSticky;
            currentTextScreenNavigation2 = "";
            textScreenNavigation1.color = colorSticky;
        }
        else if(isPrecipitate & isShooting)
        {
            currentTextScreenNavigation1 = messagePrecipitate;
            currentTextScreenNavigation2 = messageShooting;
            textScreenNavigation1.color = colorPrecipitate;
            textScreenNavigation2.color = colorShooting;
        }
        else if (isPrecipitate)
        {
            currentTextScreenNavigation1 = messagePrecipitate;
            currentTextScreenNavigation2 = "";
            textScreenNavigation1.color = colorPrecipitate;
        }
        else if (isShooting)
        {
            currentTextScreenNavigation1 = messageShooting;
            currentTextScreenNavigation2 = "";
            textScreenNavigation1.color = colorShooting;
        }
        else
        {
            currentTextScreenNavigation1 = "";
            currentTextScreenNavigation2 = "";
        }
    }
}
