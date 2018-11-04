using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepWithParentColor : MonoBehaviour {

	private SpriteRenderer _monsterSpriteRenderer;
	private SpriteRenderer _viceRenderer;
	// Use this for initialization
	void Start () {
		_monsterSpriteRenderer = gameObject.transform.parent.gameObject.GetComponent<SpriteRenderer>();
		_viceRenderer = GetComponent<SpriteRenderer>();
		
	}
	
	// Update is called once per frame
	void Update () {
		_viceRenderer.color = _monsterSpriteRenderer.color;
	}
}
