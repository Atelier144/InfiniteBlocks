using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour {

    GameObject mainManager;
    Ball ball;
    EdgeCollider2D edgeCollider2D;

    [SerializeField] Sprite[] spritesRacket = new Sprite[7];
    [SerializeField] Sprite[] spritesRacketNormal = new Sprite[7];
    [SerializeField] Sprite[] spritesRacketPrecipitate = new Sprite[7];
    [SerializeField] Sprite[] spritesMagnetEffect = new Sprite[7];
    [SerializeField] Sprite[] spritesStickyEffect = new Sprite[7];
 
    [SerializeField] float[] lengthesOfRacket;

    [SerializeField] GameObject prefabBullet;
    [SerializeField] GameObject bulletShooter;
    [SerializeField] GameObject shooterSignal;
    [SerializeField] GameObject shotEffect;
    [SerializeField] GameObject magnetEffect;
    [SerializeField] GameObject stickyEffect;

    AudioSource[] audioSources;

    int stepOfLengthOfRacket = 3;

    bool isAttachedBulletShooter = false;

	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager");
        ball = GameObject.Find("TheBall").GetComponent<Ball>();

        edgeCollider2D = GetComponent<EdgeCollider2D>();
        audioSources = GetComponents<AudioSource>();

        Transform();
    }
	
	// Update is called once per frame
	void Update () {
        float mouseX = Mathf.Round(Input.mousePosition.x - 512.0f);
        if (mouseX > 500.0f) mouseX = 500.0f;
        if (mouseX < -500.0f) mouseX = -500.0f;
        this.transform.position = new Vector3(mouseX, -195.5f, 0.0f);
    }

    private void FixedUpdate()
    {
        edgeCollider2D.isTrigger = ball.IsMovingAbove();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" || collision.gameObject.tag == "PoweredBall")
        {
            float ballPointX = collision.contacts[0].point.x;
            float racketPointX = collision.gameObject.transform.position.x;
            Vector2 collisionPoint = collision.contacts[0].point;
            float effectPointX = ballPointX - racketPointX;

            audioSources[0].time = 0.2f;
            audioSources[0].Play();
        }
    }

    public void Expand()
    {
        if (stepOfLengthOfRacket < 6)
        {
            stepOfLengthOfRacket++;
            audioSources[1].time = 0.1f;
            audioSources[1].Play();
            Transform();
        }
    }

    public void Shrink()
    {
        if (stepOfLengthOfRacket > 0)
        {
            stepOfLengthOfRacket--;
            audioSources[2].time = 0.1f;
            audioSources[2].Play();
            Transform();
        }
    }

    public void Rebound()
    {
        if (stepOfLengthOfRacket < 3)
        {
            stepOfLengthOfRacket = 3;
            Transform();
        }
    }

    public void SetStepOfLength(int step)
    {
        stepOfLengthOfRacket = step;
        Transform();
    }

    public void ChangeToNormal()
    {
        spritesRacket = spritesRacketNormal;
        Transform();
    }

    public void ChangeToPrecipitate()
    {
        spritesRacket = spritesRacketPrecipitate;
        Transform();
        audioSources[3].time = 0.1f;
        audioSources[3].Play();
    }

    public void AttachBulletShooter()
    {
        isAttachedBulletShooter = true;
        bulletShooter.SetActive(false);
        bulletShooter.SetActive(true);
    }

    public void DetachBulletShooter()
    {
        isAttachedBulletShooter = false;
        bulletShooter.SetActive(false);
    }

    public void StartMagnetEffect()
    {
        magnetEffect.SetActive(false);
        magnetEffect.SetActive(true);
    }

    public void StopMagnetEffect()
    {
        magnetEffect.SetActive(false);
    }

    public void StartStickyEffect()
    {
        stickyEffect.SetActive(false);
        stickyEffect.SetActive(true);
    }

    public void StopStickyEffect()
    {
        stickyEffect.SetActive(false);
    }
    public void ShootBullet()
    {
        if (isAttachedBulletShooter)
        {
            Vector3 shootingPosition = this.transform.position;
            shootingPosition += new Vector3(0.0f, 20.0f, 0.0f);
            Instantiate(prefabBullet, shootingPosition, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f));

            shotEffect.SetActive(false);
            shotEffect.SetActive(true);
            shooterSignal.SetActive(false);
            shooterSignal.SetActive(true);
        }
    }

    public void EnableCollider()
    {
        this.GetComponent<EdgeCollider2D>().enabled = true;
    }

    public void DisableCollider()
    {
        this.GetComponent<EdgeCollider2D>().enabled = false;
    }

    public float GetLength()
    {
        return lengthesOfRacket[stepOfLengthOfRacket];
    }

    void Transform()
    {

        float pointX0 = lengthesOfRacket[stepOfLengthOfRacket] / -2.0f;
        float pointX1 = lengthesOfRacket[stepOfLengthOfRacket] / 2.0f;

        Vector2[] points = { new Vector2(pointX0, 7.5f), new Vector2(pointX1, 7.5f)};

        this.GetComponent<EdgeCollider2D>().points = points;
        this.GetComponent<SpriteRenderer>().sprite = spritesRacket[stepOfLengthOfRacket];

        magnetEffect.GetComponent<SpriteRenderer>().sprite = spritesMagnetEffect[stepOfLengthOfRacket];
        stickyEffect.GetComponent<SpriteRenderer>().sprite = spritesStickyEffect[stepOfLengthOfRacket];
    }
}
