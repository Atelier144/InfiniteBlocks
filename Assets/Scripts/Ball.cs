﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    MainManager mainManager;
    SignalManager signalManager;

    [SerializeField] GameObject[] gameObjectsAfterimage = new GameObject[10];

    [SerializeField] GameObject prefabEffectDiminishForLevelUp;
    [SerializeField] GameObject prefabEffectDiminishForReplay;
    [SerializeField] GameObject prefabEffectDiminishForMissing;

    [SerializeField] Sprite spriteNormalBall;
    [SerializeField] Sprite spritePoweredBall;
    [SerializeField] Sprite spritePrecipitatingNormalBall;
    [SerializeField] Sprite spritePrecipitatingPoweredBall;
    [SerializeField] Sprite[] spritesNormalBallAfterimage = new Sprite[10];
    [SerializeField] Sprite[] spritesPoweredBallAfterimage = new Sprite[10];
    [SerializeField] Sprite[] spritesPrecipitatingNormalBallAfterimage = new Sprite[10];
    [SerializeField] Sprite[] spritesPrecipitatingPoweredBallAfterimage = new Sprite[10];
 
    float baseVelocity = 400.0f;
    float currentVelocity = 0.0f;
    float maxVelocity = 1000.0f;
    float movingAngle = 0.0f;

    int comboBonusScore = 0;

    bool isMoving = false;
    bool isPowered = false;

    bool isSticked;
    bool isPrecipitating;

    float stickedPointX;
    Vector2 stickedVelocity = Vector2.zero;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;

    SpriteRenderer[] spriteRenderersAfterimage = new SpriteRenderer[10];

    Vector3[] previousPositions =
    {
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero,
        Vector3.zero
    };

    Vector3 currentPosition = Vector3.zero;
    Vector3 futurePosition = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        for (int i = 0; i < 10; i++) spriteRenderersAfterimage[i] = gameObjectsAfterimage[i].GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Mathf.Round(Input.mousePosition.x - 512.0f);
        if (mouseX > 500.0f) mouseX = 500.0f;
        if (mouseX < -500.0f) mouseX = -500.0f;

        if (isSticked)
        {
            float positionX = mouseX + stickedPointX;
            this.transform.position = new Vector3(positionX, -180.0f, 0.0f);
        }

        if (mainManager.GetDialogStatus() == 2 && !isSticked && !isPrecipitating && signalManager.IsActiveMagnet())
        {
            if (this.transform.position.x > mouseX) rigidbody2D.AddForce(new Vector2(-1000.0f, 0.0f), ForceMode2D.Force);
            if (this.transform.position.x < mouseX) rigidbody2D.AddForce(new Vector2(1000.0f, 0.0f), ForceMode2D.Force);
        }

        for (int i = 0; i < 10; i++) gameObjectsAfterimage[i].transform.position = previousPositions[i];
    }

    //物理挙動はFixedUpdateメソッドに記述する。
    private void FixedUpdate()
    {
        //残像を描く
        futurePosition = this.transform.position;

        previousPositions[9] = previousPositions[8];
        previousPositions[8] = previousPositions[7];
        previousPositions[7] = previousPositions[6];
        previousPositions[6] = previousPositions[5];
        previousPositions[5] = previousPositions[4];
        previousPositions[4] = previousPositions[3];
        previousPositions[3] = previousPositions[2];
        previousPositions[2] = previousPositions[1];
        previousPositions[1] = previousPositions[0];
        previousPositions[0] = currentPosition;
        currentPosition = futurePosition;

        movingAngle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x);

        //角度補正システム
        float halfSqrt2 = Mathf.Sqrt(2) / 2;
        int correctionCode = GetAngleCorrectionCode(movingAngle);
        if(correctionCode == 1)
        {
            float velocityX = currentVelocity * halfSqrt2;
            float velocityY = currentVelocity * halfSqrt2;
            rigidbody2D.velocity = new Vector2(velocityX, velocityY);
        }
        if(correctionCode == 2)
        {
            float velocityX = currentVelocity * -halfSqrt2;
            float velocityY = currentVelocity * halfSqrt2;
            rigidbody2D.velocity = new Vector2(velocityX, velocityY);
        }
        if(correctionCode == 3)
        {
            float velocityX = currentVelocity * -halfSqrt2;
            float velocityY = currentVelocity * -halfSqrt2;
            rigidbody2D.velocity = new Vector2(velocityX, velocityY);
        }
        if(correctionCode == 4)
        {
            float velocityX = currentVelocity * halfSqrt2;
            float velocityY = currentVelocity * -halfSqrt2;
            rigidbody2D.velocity = new Vector2(velocityX, velocityY);
        }

        movingAngle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        string theTag = collision.gameObject.tag;
        if(theTag == "Racket")
        {
            isPrecipitating = false;
            Draw();
            mainManager.ClearComboBonus();
            Accelerate(2.0f);
            rigidbody2D.velocity = Vector2.zero;

            float theLengthOfRacket = collision.gameObject.GetComponent<Racket>().GetLength();

            int indexVelocity = mainManager.GetLevel();

            float ballPointX = collision.contacts[0].point.x;
            float racketPointX = collision.gameObject.transform.position.x;
            float pointX = ballPointX - racketPointX;
            float degree = (1.0f - pointX / theLengthOfRacket) * Mathf.PI / 2;
            Vector2 reflectionVelocity = new Vector2(currentVelocity * Mathf.Cos(degree), currentVelocity * Mathf.Sin(degree));

            if (signalManager.IsActiveSticky())
            {
                stickedPointX = pointX;
                stickedVelocity = reflectionVelocity;
                isSticked = true;
                rigidbody2D.velocity = Vector2.zero;
            }
            else
            {
                rigidbody2D.velocity = reflectionVelocity;
            }
        }
        if(theTag == "FailZone")
        {
            StopBall();
            StartCoroutine(mainManager.Missing());
        }
        if(theTag == "PrecipitateBlock")
        {
            float collisionPointY = collision.contacts[0].point.y - collision.transform.position.y;
            if (collisionPointY <= 20.0f)
            {
                collision.gameObject.GetComponent<PrecipitateBlock>().StartEffect();
                PrecipitateExtremely();
            }
        }

        if(theTag == "Rigidbody" || theTag == "BlockSupport")
        {
            if (isPrecipitating)
            {
                isPrecipitating = false;
                Draw();
                float rotationAngle = Random.Range(5.0f, 15.0f);
                if (Random.Range(0, 2) == 0) rotationAngle *= 1.0f;
                rigidbody2D.velocity =  Quaternion.Euler(0.0f, 0.0f, rotationAngle) * rigidbody2D.velocity;
            }
        }
        if(theTag == "Protector")
        {
            isPrecipitating = false;
            Draw();
            if (signalManager.IsActiveTrapGuard())
            {
                Vector2 reflectionVelocity = new Vector2(0.0f, currentVelocity);
                rigidbody2D.velocity = reflectionVelocity;
            }
            else
            {
                Accelerate(5.0f);
                float degree = Random.Range(0.7854f, 2.3562f); //45 to 135 degrees
                Vector2 reflectionVelocity = new Vector2(currentVelocity * Mathf.Cos(degree), currentVelocity * Mathf.Sin(degree));
                rigidbody2D.velocity = reflectionVelocity;
            }
        }
    }
    public void StopBall()
    {
        rigidbody2D.velocity = Vector2.zero;
    }

    public void SetBall()
    {
        this.transform.position = new Vector3(0.0f, -130.0f, 0.0f);
    }

    public void MoveBall()
    {
        rigidbody2D.velocity = new Vector2(0.0f, -50.0f);
    }

    public void PowerUp()
    {
        this.gameObject.tag = "PoweredBall";
        Draw();
    }

    public void PowerDown()
    {
        this.gameObject.tag = "Ball";
        Draw();
    }

    public void Precipitate()
    {
        if (!isSticked)
        {
            isPrecipitating = true;
            rigidbody2D.velocity = new Vector2(0.0f, -currentVelocity);
            Draw();
        }

    }

    public void PrecipitateExtremely()
    {
        isPrecipitating = true;
        rigidbody2D.velocity = new Vector2(0.0f, currentVelocity * -3.0f);
    }

    public void Detach()
    {
        if (isSticked && this.transform.position.x < 485.0f && this.transform.position.x > -485.0f)
        {
            isSticked = false;
            rigidbody2D.velocity = stickedVelocity;
        }
    }
    public void VerticalLoop()
    {
        Vector3 thePosition = this.transform.position;
        thePosition.y = -250.0f;
        this.transform.position = thePosition;
    }

    public void Diminish()
    {
        isSticked = false;
        isPrecipitating = false;

        rigidbody2D.velocity = Vector2.zero;
        this.transform.position = new Vector3(10000.0f, 0.0f, 0.0f);
        Draw();
    }

    public void DiminishForLevelUp()
    {
        Instantiate(prefabEffectDiminishForLevelUp, this.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        Diminish();
    }

    public void DiminishForReplay()
    {
        Instantiate(prefabEffectDiminishForReplay, this.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        Diminish();
    }

    public void DiminishForMissing()
    {
        Instantiate(prefabEffectDiminishForMissing, this.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));
        Diminish();
    }

    public void Accelerate(float s)
    {
        currentVelocity += s;
        if (currentVelocity > maxVelocity) currentVelocity = maxVelocity;
    }

    public void ResetVelocity()
    {
        currentVelocity = baseVelocity;
    }

    public void Decelerate()
    {
        currentVelocity = baseVelocity;
        SetVelocity();
    }

    public void Accelerate()
    {
        currentVelocity += 100.0f;
        if (currentVelocity > maxVelocity) currentVelocity = maxVelocity;
        SetVelocity();
    }

    public void PrepareAccelerate()
    {
        Invoke("Accelerate", 0.1f);
    }

    public void MaxSpeed()
    {
        currentVelocity = maxVelocity;
        SetVelocity();
    }

    void SetVelocity()
    {
        if (isSticked)
        {
            movingAngle = Mathf.Atan2(stickedVelocity.y, stickedVelocity.x);
            float velocityX = currentVelocity * Mathf.Cos(movingAngle);
            float velocityY = currentVelocity * Mathf.Sin(movingAngle);
            stickedVelocity = new Vector2(velocityX, velocityY);
        }
        else
        {
            float velocityX = currentVelocity * Mathf.Cos(movingAngle);
            float velocityY = currentVelocity * Mathf.Sin(movingAngle);
            rigidbody2D.velocity = new Vector2(velocityX, velocityY);
        }
    }

    int GetAngleCorrectionCode(float movingAngle)
    {
        float degree = movingAngle * Mathf.Rad2Deg;
        if (degree < 44.9f && degree > 0.0f) return 1;
        if (degree > 135.1f) return 2;
        if (degree < -135.1f) return 3;
        if (degree > -44.9f && degree < 0.0f) return 4;
        return 0;
    }

    private void Draw()
    {
        if(gameObject.tag == "Ball")
        {
            if (isPrecipitating)
            {
                spriteRenderer.sprite = spritePrecipitatingNormalBall;
                for (int i = 0; i < 10; i++) spriteRenderersAfterimage[i].sprite = spritesPrecipitatingNormalBallAfterimage[i];
            }
            else
            {
                spriteRenderer.sprite = spriteNormalBall;
                for (int i = 0; i < 10; i++) spriteRenderersAfterimage[i].sprite = spritesNormalBallAfterimage[i];
            }
        }
        if(gameObject.tag == "PoweredBall")
        {
            if (isPrecipitating)
            {
                spriteRenderer.sprite = spritePrecipitatingPoweredBall;
                for (int i = 0; i < 10; i++) spriteRenderersAfterimage[i].sprite = spritesPrecipitatingPoweredBallAfterimage[i];
            }
            else
            {
                spriteRenderer.sprite = spritePoweredBall;
                for (int i = 0; i < 10; i++) spriteRenderersAfterimage[i].sprite = spritesPoweredBallAfterimage[i];
            }
        }
    }

    public bool IsMovingAbove()
    {
        return rigidbody2D.velocity.y > 0.0f;
    }

    public bool IsMovingBelow()
    {
        return rigidbody2D.velocity.y < 0.0f;
    }
}
