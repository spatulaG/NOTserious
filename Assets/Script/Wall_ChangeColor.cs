using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_ChangeColor : MonoBehaviour {

	public Color startColor;
	public Color targetColor;

	public float duration = 5.0f;
	public float smoothness = 0.1f;

	//add some conditions
	public static bool canChange = false;
	private SpriteRenderer _spriteRenderer;
	// Use this for initialization
	void Start () {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(canChange){/*
			_spriteRenderer.color = Color.Lerp(startColor, targetColor, 0.1f);
			if(_spriteRenderer.color == targetColor)
				canChange = false;
			*/
			StartCoroutine("LerpColor");
			canChange = false;
		}
	}
	
	IEnumerator LerpColor(){
		float progress = 0; 
		float increment = smoothness/duration;
		while(progress < 1){
			_spriteRenderer.color = Color.Lerp(startColor, targetColor, progress);
			progress += increment;
			yield return new WaitForSeconds(smoothness);
		}
	}
}
