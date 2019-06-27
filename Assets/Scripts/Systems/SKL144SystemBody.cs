using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKL144SystemBody : MonoBehaviour {

    MainManager mainManager;
    SignalManager signalManager;
    PrefabCreator prefabCreator;

    Ball ball;
    Racket racket;

    [SerializeField] GameObject gameObjectSKL144System;
    [SerializeField] GameObject gameObjectFace;
    [SerializeField] GameObject gameObjectBlockGenerator;
    [SerializeField] GameObject gameObjectBigBar;
    [SerializeField] GameObject gameObjectPrecipitate;
    [SerializeField] GameObject gameObjectWeakPoint1;
    [SerializeField] GameObject gameObjectWeakPoint2;
    [SerializeField] GameObject gameObjectWeakPoint3;

    [SerializeField] GameObject explosionEffect;

    SKL144Sytem SKL144Sytem;
    SKL144SystemFace face;

    Animator animator;

    [SerializeField] int duration1;
    [SerializeField] int duration2;
    [SerializeField] int duration3;

    [SerializeField] string[] motionTrigger1;
    [SerializeField] string[] motionTrigger2;
    [SerializeField] string[] motionTrigger3;


    string[][] motionTriggers = { new string[] { }, new string[] { }, new string[] { }, new string[] { } };

    int formCode = 1;
    int motionCode = 0;
    int duration;

    bool isDamaged = false;
    bool isReturning = false;

    float returningStartPositionX;
    float returningStartPositionY;

    int returningCount = 0;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
        prefabCreator = GameObject.Find("PrefabCreator").GetComponent<PrefabCreator>();

        ball = GameObject.Find("TheBall").GetComponent<Ball>();
        racket = GameObject.Find("TheRacket").GetComponent<Racket>();

        SKL144Sytem = gameObjectSKL144System.GetComponent<SKL144Sytem>();
        face = gameObjectFace.GetComponent<SKL144SystemFace>();

        animator = GetComponent<Animator>();

        duration = duration1;

        motionTriggers[1] = motionTrigger1;
        motionTriggers[2] = motionTrigger2;
        motionTriggers[3] = motionTrigger3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (isReturning)
        {
            returningCount--;
            float positionX = returningStartPositionX * returningCount * 0.002f;
            float positionY = returningStartPositionY * returningCount * 0.002f + 240.0f;
            transform.position = new Vector3(positionX, positionY, 0.0f);
            if (returningCount == 0) isReturning = false;

        }
        else if(mainManager.GetDialogStatus() != 2)
        {
            Idle();
        }
    }
    public void OnAnimationEndIdle()
    {
        isReturning = false;
        if (mainManager.GetDialogStatus() == 2)
        {
            if (isDamaged)
            {
                switch (formCode)
                {
                    case 1:
                        isDamaged = false;
                        formCode++;
                        motionCode = -1;
                        duration = duration2;
                        animator.SetTrigger("Anger");
                        break;
                    case 2:
                        isDamaged = false;
                        formCode++;
                        motionCode = -1;
                        duration = duration3;
                        animator.SetTrigger("Anger");
                        break;
                    case 3:
                        animator.SetTrigger("Explosion");
                        break;
                }
            }
            else
            {
                animator.SetTrigger(motionTriggers[formCode][motionCode]);
            }
        }

    }

    public void Idle()
    {
        isReturning = true;
        returningCount = 500;
        returningStartPositionX = transform.position.x;
        returningStartPositionY = transform.position.y;

        animator.SetTrigger("Idle");
        face.ChangeFace(formCode);
        UnsetActiveBlockGenerator();
        UnsetActiveBigBar();
        UnsetActivePrecipitate();

        switch (formCode)
        {
            case 1:
                gameObjectWeakPoint1.SetActive(true);
                gameObjectWeakPoint2.SetActive(false);
                gameObjectWeakPoint3.SetActive(false);
                break;
            case 2:
                gameObjectWeakPoint1.SetActive(false);
                gameObjectWeakPoint2.SetActive(true);
                gameObjectWeakPoint3.SetActive(false);
                break;
            case 3:
                gameObjectWeakPoint1.SetActive(false);
                gameObjectWeakPoint2.SetActive(false);
                gameObjectWeakPoint3.SetActive(true);
                break;
        }
        motionCode++;
        if (motionCode >= motionTriggers[formCode].Length) motionCode = 0;
    }

    public void Damage(int damage)
    {
        face.DamageFace();
        duration -= damage;
        if(duration <= 0)
        {
            isDamaged = true;
            switch (formCode)
            {
                case 1:
                    gameObjectWeakPoint1.SetActive(false);
                    gameObjectWeakPoint2.SetActive(false);
                    gameObjectWeakPoint3.SetActive(false);
                    break;
                case 2:
                    gameObjectWeakPoint1.SetActive(false);
                    gameObjectWeakPoint2.SetActive(false);
                    gameObjectWeakPoint3.SetActive(false);
                    break;
                case 3:
                    gameObjectWeakPoint1.SetActive(false);
                    gameObjectWeakPoint2.SetActive(false);
                    gameObjectWeakPoint3.SetActive(false);
                    ball.DiminishForLevelUp();
                    break;
            }
        }
    }

    public void ChangeFace(int faceCode)
    {
        face.ChangeFace(faceCode);
    }

    public void SetActiveBlockGenerator()
    {
        gameObjectBlockGenerator.SetActive(true);
    }

    public void UnsetActiveBlockGenerator()
    {
        gameObjectBlockGenerator.SetActive(false);
    }

    public void SetActiveBigBar()
    {
        gameObjectBigBar.SetActive(true);
    }

    public void UnsetActiveBigBar()
    {
        gameObjectBigBar.SetActive(false);
    }

    public void SetActivePrecipitate()
    {
        gameObjectPrecipitate.SetActive(true);
    }

    public void UnsetActivePrecipitate()
    {
        gameObjectPrecipitate.SetActive(false);
    }

    public void DestroyAllBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks) block.GetComponent<Block>().DestroyBlock(0);
    }

    public void GenerateNormalBlocks()
    {
        Vector3 position1 = this.transform.position + new Vector3(-50.0f, 130.0f, 0.0f);
        Vector3 position2 = this.transform.position + new Vector3(50.0f, 130.0f, 0.0f);

        float positionX1 = Mathf.Round(position1.x / 25.0f) * 25.0f;
        float positionX2 = Mathf.Round(position2.x / 25.0f) * 25.0f;
        float positionY1 = Mathf.Round(position1.y / 10.0f) * 10.0f;
        float positionY2 = Mathf.Round(position2.y / 10.0f) * 10.0f;
        prefabCreator.CreateNormalBlock(positionX1,positionY1, Random.Range(0, 7));
        prefabCreator.CreateNormalBlock(positionX2,positionY2, Random.Range(0, 7));
    }

    public void GenerateHardBlocks()
    {
        Vector3 position1 = this.transform.position + new Vector3(-50.0f, 130.0f, 0.0f);
        Vector3 position2 = this.transform.position + new Vector3(50.0f, 130.0f, 0.0f);

        float positionX1 = Mathf.Round(position1.x / 25.0f) * 25.0f;
        float positionX2 = Mathf.Round(position2.x / 25.0f) * 25.0f;
        float positionY1 = Mathf.Round(position1.y / 10.0f) * 10.0f;
        float positionY2 = Mathf.Round(position2.y / 10.0f) * 10.0f;
        prefabCreator.CreateHardBlock(positionX1, positionY1);
        prefabCreator.CreateHardBlock(positionX2, positionY2);
    }

    public void Guard()
    {
        gameObjectWeakPoint1.SetActive(false);
        gameObjectWeakPoint2.SetActive(false);
        gameObjectWeakPoint3.SetActive(false);
    }

    public void Flash()
    {
        if (signalManager.IsActiveTrapGuard()) Debug.Log("閃光防止成功！");
        else mainManager.StartFlash();
    }

    public void PrepareExplosion1()
    {
        gameObjectBlockGenerator.SetActive(true);
        gameObjectBigBar.SetActive(true);
        gameObjectPrecipitate.SetActive(true);
        gameObjectWeakPoint1.SetActive(true);
        gameObjectWeakPoint2.SetActive(true);
        gameObjectWeakPoint3.SetActive(true);
    }

    public void PrepareExplosion2()
    {
        gameObjectBlockGenerator.SetActive(false);
        gameObjectBigBar.SetActive(false);
        gameObjectPrecipitate.SetActive(false);
        gameObjectWeakPoint1.SetActive(false);
        gameObjectWeakPoint2.SetActive(false);
        gameObjectWeakPoint3.SetActive(false);
    }

    public void Explosion()
    {
        SKL144Sytem.SetLevelUp(true);
        Instantiate(explosionEffect, this.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        Destroy(gameObject);
    }
}
