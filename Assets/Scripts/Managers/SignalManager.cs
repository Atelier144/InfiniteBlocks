﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalManager : MonoBehaviour {

    [SerializeField] Sprite spriteBackgroundNormal;
    [SerializeField] Sprite spriteBackgroundTrapGuard;

    [SerializeField] Image imageSignalReplayMode;
    [SerializeField] Image imageSignalPowerUp;
    [SerializeField] Image imageSignalProtector;
    [SerializeField] Image imageSignalTrapGuard;
    [SerializeField] Image imageSignalPrecipitate;
    [SerializeField] Image imageSignalShooting;
    [SerializeField] Image imageSignalMagnet;
    [SerializeField] Image imageSignalSticky;

    [SerializeField] GameObject background;
    [SerializeField] GameObject protector;

    [SerializeField] bool signalTestMode;

    MainManager mainManager;
    SystemManager systemManager;
    ScreenNavigationManager screenNavigationManager;
    Aurora aurora;

    Ball theBall;
    Racket theRacket;

    AudioSource audioSource;

    int activeCountReplayMode;
    int activeCountPowerUp;
    int activeCountProtector;
    int activeCountTrapGuard;
    int activeCountPrecipitate;
    int activeCountShooting;
    int activeCountMagnet;
    int activeCountSticky;

    // Use this for initialization
    void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        systemManager = GameObject.Find("SystemManager").GetComponent<SystemManager>();
        screenNavigationManager = GameObject.Find("ScreenNavigationManager").GetComponent<ScreenNavigationManager>();

        aurora = GameObject.Find("Aurora").GetComponent<Aurora>();

        theBall = GameObject.Find("TheBall").GetComponent<Ball>();
        theRacket = GameObject.Find("TheRacket").GetComponent<Racket>();

        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        imageSignalReplayMode.enabled = IsEnabledSignal(activeCountReplayMode);
        imageSignalPowerUp.enabled = IsEnabledSignal(activeCountPowerUp);
        imageSignalProtector.enabled = IsEnabledSignal(activeCountProtector);
        imageSignalTrapGuard.enabled = IsEnabledSignal(activeCountTrapGuard);
        imageSignalPrecipitate.enabled = IsEnabledSignal(activeCountPrecipitate);
        imageSignalShooting.enabled = IsEnabledSignal(activeCountShooting);
        imageSignalMagnet.enabled = IsEnabledSignal(activeCountMagnet);
        imageSignalSticky.enabled = IsEnabledSignal(activeCountSticky);
        if (activeCountReplayMode > 0 && mainManager.GetDialogStatus() != 2) imageSignalReplayMode.enabled = true;
    }

    private void FixedUpdate()
    {
        //Beginner Save
        if (activeCountReplayMode> 1 && mainManager.GetDialogStatus() == 2)
        {
            activeCountReplayMode--;
        }
        else if(activeCountReplayMode == 1 && mainManager.GetDialogStatus() == 2)
        {
            activeCountReplayMode = 0;
            StopReplayMode();
        }

        //Powered Ball
        if (activeCountPowerUp > 1)
        {
            activeCountPowerUp--;
        }
        else if (activeCountPowerUp == 1)
        {
            activeCountPowerUp = 0;
            StopPoweredBall();
        }

        //Protector
        if (activeCountProtector > 1)
        {
            activeCountProtector--;
        }
        else if (activeCountProtector == 1)
        {
            activeCountProtector = 0;
            StopProtector();
        }

        //Trap Guard
        if (activeCountTrapGuard > 1)
        {
            activeCountTrapGuard--;
        }
        else if(activeCountTrapGuard == 1)
        {
            activeCountTrapGuard = 0;
            StopTrapGuard();
        }

        //Precipitate
        if (activeCountPrecipitate > 1)
        {
            activeCountPrecipitate--;
        }
        else if(activeCountPrecipitate == 1)
        {
            activeCountPrecipitate = 0;
            StopPrecipitate();
        }

        //Shooting
        if (activeCountShooting > 1)
        {
            activeCountShooting--;
        }
        else if(activeCountShooting == 1)
        {
            activeCountShooting = 0;
            StopShooting();
        }

        //Magnet
        if (activeCountMagnet > 1)
        {
            activeCountMagnet--;
        }
        else if(activeCountMagnet == 1)
        {
            activeCountMagnet = 0;
            StopMagnet();
        }

        //Sticky
        if (activeCountSticky > 1)
        {
            activeCountSticky--;
        }
        else if(activeCountSticky == 1)
        {
            activeCountSticky = 0;
            StopSticky();
        }
    }

    //ボタン系
    public void PushSignalReplayModeButton()
    {
        if (activeCountReplayMode > 0)
        {
            activeCountReplayMode = 0;
            StopReplayMode();
            mainManager.AddJackpotScore(5000);
        }
        else if (signalTestMode)
        {
            StartReplayMode(int.MaxValue);
        }
    }

    public void PushSignalPowerUpButton()
    {
        if (activeCountPowerUp > 0)
        {
            activeCountPowerUp = 0;
            StopPoweredBall();
            mainManager.AddJackpotScore(1000);
        }
        else if (signalTestMode)
        {
            StartPoweredBall(int.MaxValue);
        }
    }

    public void PushSignalProtectorButton()
    {
        if (activeCountProtector > 0)
        {
            activeCountProtector = 0;
            StopProtector();
            mainManager.AddJackpotScore(1000);
        }
        else if (signalTestMode)
        {
            StartProtector(int.MaxValue);
        }
    }

    public void PushSignalTrapGuardButton()
    {
        if (activeCountTrapGuard > 0)
        {
            activeCountTrapGuard = 0;
            StopTrapGuard();
            mainManager.AddJackpotScore(1000);
        }
        else if (signalTestMode)
        {
            StartTrapGuard(int.MaxValue);
        }
    }

    public void PushSignalPrecipitateButton()
    {
        if (activeCountPrecipitate > 0)
        {
            activeCountPrecipitate = 0;
            StopPrecipitate();
            mainManager.AddJackpotScore(1000);
        }
        else if (signalTestMode)
        {
            StartPrecipitate(int.MaxValue);
        }
    }

    public void PushSignalShootingButton()
    {
        if (activeCountShooting > 0)
        {
            activeCountShooting = 0;
            StopShooting();
            mainManager.AddJackpotScore(1000);
        }
        else if (signalTestMode)
        {
            StartShooting(int.MaxValue);
        }
    }

    public void PushSignalMagnetButton()
    {
        if (activeCountMagnet > 0)
        {
            activeCountMagnet = 0;
            StopMagnet();
            mainManager.AddJackpotScore(1000);
        }
        else if (signalTestMode)
        {
            StartMagnet(int.MaxValue);
        }
    }

    public void PushSignalStickyButton()
    {
        if (activeCountSticky > 0)
        {
            activeCountSticky = 0;
            StopSticky();
            mainManager.AddJackpotScore(1000);
        }
        else if (signalTestMode)
        {
            StartSticky(int.MaxValue);
        }
    }
    //
    public void StartReplayMode(int activeCount)
    {
        activeCountReplayMode = activeCount;
        aurora.SetActiveReplayModeAurora(true);
    }

    //リプレイモードを無効にするメソッド
    public void StopReplayMode()
    {
        activeCountReplayMode = 0;
        aurora.SetActiveReplayModeAurora(false);
    }

    //ボールを強化するメソッド
    public void StartPoweredBall(int activeCount)
    {
        activeCountPowerUp = activeCount;
        theBall.PowerUp();
    }

    //ボールを弱化させるメソッド
    public void StopPoweredBall()
    {
        activeCountPowerUp = 0;
        theBall.PowerDown();
    }

    //プロテクターを作動させるメソッド
    public void StartProtector(int activeCount)
    {
        activeCountProtector = activeCount;
        protector.SetActive(false);
        protector.SetActive(true);
        aurora.SetActiveProtector(true);
    }

    //プロテクターを停止させるメソッド
    public void StopProtector()
    {
        activeCountProtector = 0;
        protector.SetActive(false);
        aurora.SetActiveProtector(false);
    }

    public void StartTrapGuard(int activeCount)
    {
        activeCountTrapGuard = activeCount;
        background.GetComponent<SpriteRenderer>().sprite = spriteBackgroundTrapGuard;
        audioSource.time = 0.1f;
        audioSource.Play();
    }

    public void StopTrapGuard()
    {
        activeCountTrapGuard = 0;
        background.GetComponent<SpriteRenderer>().sprite = spriteBackgroundNormal;
    }

    public void StartPrecipitate(int activeCount)
    {
        activeCountPrecipitate = activeCount;
        theRacket.ChangeToPrecipitate();
        screenNavigationManager.SetPrecipitate(true);
    }

    public void StopPrecipitate()
    {
        activeCountPrecipitate = 0;
        theRacket.ChangeToNormal();
        screenNavigationManager.SetPrecipitate(false);
    }

    public void StartShooting(int activeCount)
    {
        activeCountShooting = activeCount;
        theRacket.AttachBulletShooter();
        screenNavigationManager.SetShooting(true);
    }

    public void StopShooting()
    {
        theRacket.DetachBulletShooter();
        activeCountShooting = 0;
        screenNavigationManager.SetShooting(false);
    }

    public void StartMagnet(int activeCount)
    {
        activeCountMagnet = activeCount;
        theRacket.StartMagnetEffect();
    }

    public void StopMagnet()
    {
        activeCountMagnet = 0;
        theRacket.StopMagnetEffect();
    }

    public void StartSticky(int activeCount)
    {
        activeCountSticky = activeCount;
        theRacket.StartStickyEffect();
    }

    public void StopSticky()
    {
        activeCountSticky = 0;
        theRacket.StopStickyEffect();
        theBall.Detach();
    }

    //ReplayMode以外の特殊効果を無効にするメソッド
    public void StopAllSignalsWithoutReplayMode()
    {
        StopPoweredBall();
        StopProtector();
        StopTrapGuard();
        StopPrecipitate();
        StopShooting();
        StopMagnet();
        StopSticky();
    }

    //全ての特殊効果を停止させるメソッド
    public void StopAllSignals()
    {
        StopReplayMode();
        StopPoweredBall();
        StopProtector();
        StopTrapGuard();
        StopPrecipitate();
        StopShooting();
        StopMagnet();
        StopSticky();
    }

    //ゲッター系
    public bool IsActiveReplayMode()
    {
        return activeCountReplayMode > 0;
    }

    public bool IsActivePowerUp()
    {
        return activeCountPowerUp > 0;
    }

    public bool IsActiveProtector()
    {
        return activeCountProtector > 0;
    }

    public bool IsActiveTrapGuard()
    {
        return activeCountTrapGuard > 0;
    }

    public bool IsActivePrecipitate()
    {
        return activeCountPrecipitate > 0;
    }

    public bool IsActiveShooting()
    {
        return activeCountShooting > 0;
    }

    public bool IsActiveMagnet()
    {
        return activeCountMagnet > 0;
    }

    public bool IsActiveSticky()
    {
        return activeCountSticky > 0;
    }

    //その他
    bool IsEnabledSignal(int activeCount)
    {
        if (activeCount >= 500)
        {
            return true;
        }
        if (activeCount > 0)
        {
            if (activeCount / 25 >= 6)
            {
                if (activeCount / 25 % 2 == 1)
                {
                    return false;
                }
                return true;
            }
            if (activeCount % 50 / 8 % 2 == 1)
            {
                return false;
            }
            return true;
        }
        return false;
    }
}
