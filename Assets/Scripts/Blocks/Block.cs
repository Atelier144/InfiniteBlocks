using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    protected MainManager mainManager;
    protected SignalManager signalManager;

    [SerializeField] protected GameObject prefabItem;
    [SerializeField] protected GameObject prefabBlockBreakEffect;

    public int itemCode;
    public string triggerName;
    public int breakScore;

	// Use this for initialization
	protected virtual void Start () {
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        signalManager = GameObject.Find("SignalManager").GetComponent<SignalManager>();
	}

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    protected virtual void FixedUpdate()
    {

    }

    public virtual void DestroyBlock(int comboBonus)
    {
        mainManager.AddGameScore(breakScore);
        mainManager.AddGameScoreByComboBonus();
        mainManager.AddComboBonus(comboBonus);
        Instantiate(prefabBlockBreakEffect, this.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<BlockBreakEffect>().Initialize(triggerName);
        Instantiate(prefabItem, this.transform.position, new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)).GetComponent<Item>().Initialize(itemCode);
        Destroy(this.gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") DestroyBlock(1);
        if (collision.gameObject.tag == "PoweredBall") DestroyBlock(0);
        if (collision.gameObject.tag == "Bullet") DestroyBlock(0);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PoweredBall") DestroyBlock(0);
        if (collision.gameObject.tag == "Bullet") DestroyBlock(0);
    }
}
