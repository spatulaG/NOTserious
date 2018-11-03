using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_GoThrough : MonoBehaviour {

	public GameObject player;
	private Collider2D _coll;

	// Use this for initialization
	void Start () {
		_coll = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(this.GetComponent<SpriteRenderer>().color.r*255 - player.GetComponent<SpriteRenderer>().color.r*255) < 0.5f)
			_coll.isTrigger = true;
		else
			_coll.isTrigger = false;

	}
}
