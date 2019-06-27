using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBumper : MonoBehaviour {

    int bumperCode = 2;

    MainManager mainManager;

    [SerializeField] GameObject gameObjectEffect;

    [SerializeField] Sprite[] sprites = new Sprite[3];
    [SerializeField] int[] numbersPoint = new int[3];

    PointBumperEffect effect;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
	// Use this for initialization
	void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();

        effect = gameObjectEffect.GetComponent<PointBumperEffect>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        spriteRenderer.sprite = sprites[bumperCode];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        mainManager.AddGameScore(numbersPoint[bumperCode]);
        effect.SetEffect(bumperCode);
        audioSource.time = 0.0f;
        audioSource.Play();
    }

    public void Initialize(int bumperCode)
    {
        this.bumperCode = bumperCode;
    }
}
