using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144Sytem : MonoBehaviour {

    MainManager mainManager;
    SignalManager signalManager;
    MusicManager musicManager;
    PrefabCreator prefabCreator;
    Ball ball;

    
    [SerializeField] GameObject gameObjectBody;
    [SerializeField] GameObject gameObjectFace;
    [SerializeField] GameObject gameObjectBigBar;
    [SerializeField] GameObject gameObjectBlockGenerator;
    [SerializeField] GameObject gameObjectPrecipitate;
    [SerializeField] GameObject gameObjectWeakPoint1;
    [SerializeField] GameObject gameObjectWeakPoint2;
    [SerializeField] GameObject gameObjectWeakPoint3;

    [SerializeField] GameObject prefabExplosionEffect;

    [SerializeField] int duration1;
    [SerializeField] int duration2;
    [SerializeField] int duration3;

    [SerializeField] string[] motionTrigger1;
    [SerializeField] string[] motionTrigger2;
    [SerializeField] string[] motionTrigger3;

    string[][] motionTriggers = { new string[] { }, new string[] { }, new string[] { }, new string[] { } };
    int[] durations = new int[4];
    SKL144SystemBody body;
    SKL144SystemFace face;

    bool isLevelUp;
    bool isReturned;
    bool flipReturn;
    bool isIdle;

    int countInvincible;
    int duration;
    int motionCode;
    int phaseCode = 1;

    Coroutine currentCoroutine;

    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        musicManager = GameObject.Find("MusicManager").GetComponent<MusicManager>();
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();
        ball = GameObject.Find("TheBall").GetComponent<Ball>();

        body = gameObjectBody.GetComponent<SKL144SystemBody>();
        face = gameObjectFace.GetComponent<SKL144SystemFace>();

        motionTriggers[1] = motionTrigger1;
        motionTriggers[2] = motionTrigger2;
        motionTriggers[3] = motionTrigger3;

        durations[1] = duration1;
        durations[2] = duration2;
        durations[3] = duration3;

        isIdle = true;

        duration = duration1;
    }
	
	// Update is called once per frame
	void Update () {
        if (countInvincible > 0) countInvincible--;
    }

    private void FixedUpdate()
    {
        if (mainManager.GetDialogStatus() == 0)
        {
            if (!isReturned)
            {
                isReturned = true;
                StopCoroutine(currentCoroutine);
                StartCoroutine("Return");
            }
        }
        if (mainManager.GetDialogStatus() == 2)
        {
            if (isIdle)
            {
                isIdle = false;
                isReturned = false;
                currentCoroutine = StartCoroutine(motionTriggers[phaseCode][motionCode]);

            }
        }
    }

    public bool IsLevelUp()
    {
        return isLevelUp;
    }

    public void Damage(int countDamage)
    {
        if(countInvincible == 0)
        {
            duration -= countDamage;
            if(duration <= 0)
            {
                duration = 0;
                switch (phaseCode)
                {

                    case 1:
                    case 2:
                        isReturned = true;
                        StopCoroutine(currentCoroutine);
                        StartCoroutine("ReturnToNextPhase");
                        break;
                    case 3:
                        isReturned = true;
                        ball.DiminishForLevelUp();
                        musicManager.ChangeMusic(0);
                        StopCoroutine(currentCoroutine);
                        StartCoroutine("ReturnToExplosion");
                        break;
                }

            }
            else
            {
                countInvincible = 60;
                face.DamageFace();
            }
        }
    }

    #region Motions
    public IEnumerator Motion1()
    {
        face.ChangeFace(6);
        for(int i = 0; i < 10; i++)
        {
            float[] vx = { -350.0f, 0.0f, 700.0f, 0.0f, -700.0f, 0.0f, 700.0f, 0.0f, -350.0f, 0.0f };
            float[] vy = { 0.0f, -250.0f, 0.0f, 250.0f, -250.0f, 250.0f, -250.0f, 250.0f, -250.0f, 250.0f };
            body.SetVelocity(vx[i], vy[i]);
            for (int j = 0; j < 50; j++) yield return new WaitForFixedUpdate();
        }
        face.ChangeFace(1);
        body.SetVelocity(0.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator Motion2()
    {
        face.ChangeFace(6);
        for (int i=0; i < 16; i++)
        {
            float[] vx = { -350.0f, 700.0f, 0.0f, -700.0f, 0.0f, 700.0f, 0.0f, -175.0f, 0.0f, -175.0f, 0.0f, -175.0f, 0.0f, -175.0f, 0.0f, 350.0f };
            float[] vy = { 0.0f, -300.0f, 300.0f, 0.0f, -300.0f, 300.0f, -300.0f, 300.0f, -300.0f, 300.0f, -300.0f, 300.0f, -300.0f, 300.0f, -300.0f, 300.0f };
            int[] fc = { 50, 50, 25, 50, 25, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, };
            body.SetVelocity(vx[i], vy[i]);
            for (int j = 0; j < fc[i]; j++) yield return new WaitForFixedUpdate();
        }
        face.ChangeFace(1);
        body.SetVelocity(0.0f, 0.0f);    
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator Motion3()
    {
        face.ChangeFace(6);
        for (int i = 0; i < 9; i++)
        {
            float[] vx = { 1500.0f, -3000.0f, 1500.0f, 1500.0f, -3000.0f, 1500.0f, 1500.0f, -3000.0f, 1500.0f };
            float[] vy = { -1200.0f, 600.0f, 0.0f, 600.0f, 0.0f, -1200.0f, 600.0f, -600.0f, 1200.0f };

            body.SetVelocity(vx[i], vy[i]);
            for (int j = 0; j < 10; j++) yield return new WaitForFixedUpdate();
            body.SetVelocity(0.0f, 0.0f);
            yield return new WaitForSeconds(1.0f);
        }
        face.ChangeFace(1);
        SetIdle();
    }

    public IEnumerator Motion4()
    {
        face.ChangeFace(6);
        for (int i = 0; i < 15; i++)
        {
            float[] vx = { 0.0f, -800.0f, 800.0f, -800.0f, 800.0f, -800.0f, 800.0f, -800.0f, 800.0f, -800.0f, 800.0f, -800.0f, 800.0f, -800.0f, 0.0f };
            float[] vy = { -150.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 150.0f };
            int[] fc = { 100, 25, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, 25, 100 };
            body.SetVelocity(vx[i], vy[i]);
            for (int j = 0; j < fc[i]; j++) yield return new WaitForFixedUpdate();
        }
        face.ChangeFace(1);
        body.SetVelocity(0.0f, 0.0f);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator BigBar1()
    {
        gameObjectBigBar.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 5; i++)
        {

            float[] vx = { -300.0f, 0.0f, 60.0f, 0.0f, -300.0f };
            float[] vy = { 0.0f, -150.0f, 0.0f, 150.0f, 0.0f };
            int[] fc = { 50, 50, 500, 50, 50 };
            body.SetVelocity(vx[i], vy[i]);
            for (int j = 0; j < fc[i]; j++) yield return new WaitForFixedUpdate();
        }

        body.SetVelocity(0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        gameObjectBigBar.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SetIdle();

    }

    public IEnumerator BigBar2()
    {
        gameObjectBigBar.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 7; i++)
        {

            float[] vx = { -250.0f, 0.0f, 250.0f, 0.0f, 250.0f, 0.0f, -250.0f };
            float[] vy = { 0.0f, -120.0f, 240.0f, -120.0f, 240.0f, -120.0f, 240.0f };
            int[] fc = { 50, 100, 50, 100, 50, 100, 50 };
            body.SetVelocity(vx[i], vy[i]);
            for (int j = 0; j < fc[i]; j++) yield return new WaitForFixedUpdate();
        }

        body.SetVelocity(0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        gameObjectBigBar.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator BigBar3()
    {
        gameObjectBigBar.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        body.SetVelocity(0.0f, -500.0f);
        for (int i = 0; i < 20; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 0.0f);
        yield return new WaitForSeconds(2.0f);
        body.SetVelocity(0.0f, 80.0f);
        for (int i = 0; i < 125; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        gameObjectBigBar.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator Guard1()
    {
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(7);
        gameObjectWeakPoint1.SetActive(false);
        gameObjectWeakPoint2.SetActive(false);
        gameObjectWeakPoint3.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        SetIdle();
    }

    public IEnumerator Guard2()
    {
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(7);
        gameObjectWeakPoint1.SetActive(false);
        gameObjectWeakPoint2.SetActive(false);
        gameObjectWeakPoint3.SetActive(false);
        yield return new WaitForSeconds(5.0f);
        SetIdle();
    }

    public IEnumerator Guard3()
    {
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(7);
        gameObjectWeakPoint1.SetActive(false);
        gameObjectWeakPoint2.SetActive(false);
        gameObjectWeakPoint3.SetActive(false);
        yield return new WaitForSeconds(10.0f);
        SetIdle();
    }

    public IEnumerator Block1()
    {

        face.ChangeFace(10);
        DestryAllBlocks();
        yield return new WaitForSeconds(2.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        gameObjectBlockGenerator.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(6);
        body.SetVelocity(-400.0f, 0.0f);

        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, -300.0f);
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(200.0f, 0.0f);

        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();

        body.SetVelocity(0.0f, 80.0f);
        for (int i = 0; i < 25; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(-200.0f, 0.0f);

        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();

        body.SetVelocity(0.0f, 80.0f);
        for (int i = 0; i < 25; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(200.0f, 0.0f);

        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();

        body.SetVelocity(0.0f, 220.0f);
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(-400.0f, 0.0f);
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 0.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        gameObjectBlockGenerator.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator Block2()
    {
        face.ChangeFace(10);
        DestryAllBlocks();
        yield return new WaitForSeconds(2.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        gameObjectBlockGenerator.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(6);
        body.SetVelocity(-375.0f, -300.0f);
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(250.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 100.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(-250.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 100.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(250.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 100.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(-250.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 100.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(250.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 30; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateNormalBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(-375.0f, 220.0f);
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 0.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        gameObjectBlockGenerator.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator Block3()
    {
        face.ChangeFace(10);
        DestryAllBlocks();
        yield return new WaitForSeconds(2.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        gameObjectBlockGenerator.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(6);
        body.SetVelocity(-325.0f, -320.0f);
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(500.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(0.0f, 200.0f);
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(-500.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(0.0f, 200.0f);
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(500.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(0.0f, 200.0f);
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(-500.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(0.0f, 200.0f);
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(500.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(0.0f, 200.0f);
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(-500.0f, 0.0f);
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 15; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();
        body.CreateHardBlocks();
        for (int i = 0; i < 10; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(0.0f, 200.0f);
        for (int i = 0; i < 5; i++) yield return new WaitForFixedUpdate();

        body.SetVelocity(325.0f, 200.0f);
        for (int i = 0; i < 50; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 0.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(1.0f);
        gameObjectBlockGenerator.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator Precipitate1()
    {
        gameObjectPrecipitate.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 9; i++)
        {

            float[] vx = { -300.0f, 0.0f, 120.0f, 0.0f, -120.0f, 0.0f, 120.0f, 0.0f, -300.0f };
            float[] vy = { 0.0f, -100.0f, 0.0f, 100.0f, 0.0f, -100.0f, 0.0f, 100.0f, 0.0f };
            int[] fc = { 50, 100, 250, 50, 250, 50, 250, 100, 50 };
            body.SetVelocity(vx[i], vy[i]);
            for (int j = 0; j < fc[i]; j++) yield return new WaitForFixedUpdate();
        }

        body.SetVelocity(0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        gameObjectPrecipitate.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator Precipitate2()
    {
        gameObjectPrecipitate.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 8; i++)
        {

            float[] vx = { -300.0f, 0.0f, 150.0f, 150.0f, 150.0f, 150.0f, 0.0f, -300.0f };
            float[] vy = { 0.0f, -250.0f, 150.0f, -150.0f, 150.0f, -150.0f, 250.0f, 0.0f };
            body.SetVelocity(vx[i], vy[i]);
            for (int j = 0; j < 50; j++) yield return new WaitForFixedUpdate();
        }

        body.SetVelocity(0.0f, 0.0f);
        yield return new WaitForSeconds(0.5f);
        gameObjectPrecipitate.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        SetIdle();
    }

    public IEnumerator Flash()
    {
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(13);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(12);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(11);
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(0);
        mainManager.StartFlash();
        yield return new WaitForSeconds(1.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(3.0f);
        SetIdle();
    }


    #endregion

    public IEnumerator Return()
    {
        face.ChangeFace(1);

        gameObjectBigBar.SetActive(false);
        gameObjectBlockGenerator.SetActive(false);
        gameObjectPrecipitate.SetActive(false);

        float velocityX = gameObjectBody.transform.position.x / -2.0f;
        float velocityY = (gameObjectBody.transform.position.y - 240.0f) / -2.0f;

        body.SetVelocity(velocityX, velocityY);
        for (int i = 0; i < 100; i++) yield return new WaitForFixedUpdate();
        motionCode--;
        SetIdle();
    }

    public IEnumerator ReturnToNextPhase()
    {
        face.ChangeFace(5);

        gameObjectWeakPoint1.SetActive(false);
        gameObjectWeakPoint2.SetActive(false);
        gameObjectWeakPoint3.SetActive(false);
        gameObjectBigBar.SetActive(false);
        gameObjectBlockGenerator.SetActive(false);
        gameObjectPrecipitate.SetActive(false);

        float velocityX = gameObjectBody.transform.position.x / -2.0f;
        float velocityY = (gameObjectBody.transform.position.y - 240.0f) / -2.0f;

        body.SetVelocity(velocityX, velocityY);
        for (int i = 0; i < 100; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 0.0f);
        gameObjectBody.transform.position = new Vector3(0.0f, 240.0f, 0.0f);
        yield return new WaitForSeconds(2.0f);
        face.ChangeFace(1);
        yield return new WaitForSeconds(2.0f);
        face.ChangeFace(10);
        gameObjectBigBar.SetActive(true);
        gameObjectBlockGenerator.SetActive(true);
        gameObjectPrecipitate.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        gameObjectBigBar.SetActive(false);
        gameObjectBlockGenerator.SetActive(false);
        gameObjectPrecipitate.SetActive(false);

        face.ChangeFace(1);
        yield return new WaitForSeconds(2.0f);
        motionCode = -1;
        phaseCode++;

        duration = durations[phaseCode];
        SetIdle();
    }

    public IEnumerator ReturnToExplosion()
    {
        face.ChangeFace(5);

        gameObjectWeakPoint1.SetActive(false);
        gameObjectWeakPoint2.SetActive(false);
        gameObjectWeakPoint3.SetActive(false);
        gameObjectBigBar.SetActive(false);
        gameObjectBlockGenerator.SetActive(false);
        gameObjectPrecipitate.SetActive(false);

        float velocityX = gameObjectBody.transform.position.x / -2.0f;
        float velocityY = (gameObjectBody.transform.position.y - 240.0f) / -2.0f;

        body.SetVelocity(velocityX, velocityY);
        for (int i = 0; i < 100; i++) yield return new WaitForFixedUpdate();
        body.SetVelocity(0.0f, 0.0f);
        gameObjectBody.transform.position = new Vector3(0.0f, 240.0f, 0.0f);
        yield return new WaitForSeconds(2.0f);
        face.ChangeFace(4);
        for (int i = 0; i < 5; i++) 
        {
            gameObjectBigBar.SetActive(false);
            gameObjectBlockGenerator.SetActive(false);
            gameObjectPrecipitate.SetActive(false);
            yield return new WaitForSeconds(0.3f);
            gameObjectBigBar.SetActive(true);
            gameObjectBlockGenerator.SetActive(true);
            gameObjectPrecipitate.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(3.0f);
        isLevelUp = true;
        gameObjectBody.SetActive(false);
        Instantiate(prefabExplosionEffect, new Vector3(0.0f, 240.0f, 0.0f), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

    }

    void SetIdle()
    {
        face.ChangeFace(1);
        isIdle = true;
        gameObjectBigBar.SetActive(false);
        gameObjectBlockGenerator.SetActive(false);
        gameObjectPrecipitate.SetActive(false);
        switch (phaseCode)
        {
            case 1:
                gameObjectWeakPoint1.SetActive(true);
                gameObjectWeakPoint2.SetActive(true);
                gameObjectWeakPoint3.SetActive(true);
                break;
            case 2:
                gameObjectWeakPoint1.SetActive(true);
                gameObjectWeakPoint2.SetActive(false);
                gameObjectWeakPoint3.SetActive(false);
                break;
            case 3:
                gameObjectWeakPoint1.SetActive(false);
                gameObjectWeakPoint2.SetActive(false);
                gameObjectWeakPoint3.SetActive(true);
                break;
        }
        body.SetVelocity(0.0f, 0.0f);
        gameObjectBody.transform.position = new Vector3(0.0f, 240.0f, 0.0f);

        motionCode++;
        if (motionCode >= motionTriggers[phaseCode].Length) motionCode = 0;
    }

    void DestryAllBlocks()
    {
        GameObject[] gameObjectsBlocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject gameObjectBlock in gameObjectsBlocks) gameObjectBlock.GetComponent<Block>().DestroyBlock(0);
    }
}
