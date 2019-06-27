using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketBounceEffect : MonoBehaviour {

    [SerializeField] float soundStart;

    float effectPosition;

	// Use this for initialization
	void Start () {
        this.GetComponent<AudioSource>().time = soundStart;
	}
	
	// Update is called once per frame
	void Update () {
        float mouseX = Mathf.Round(Input.mousePosition.x - 512.0f);
        if (mouseX > 500.0f)
        {
            mouseX = 500.0f;
        }
        else if (mouseX < -500.0f)
        {
            mouseX = -500.0f;
        }
        this.transform.position = new Vector3(mouseX + effectPosition, -188.0f, 0.0f);
    }

    public void OnAnimationEnd()
    {
        Destroy(this.gameObject);
    }

    public void SetEffectPosition(float effectPosition)
    {
        this.effectPosition = effectPosition;
    }
}
