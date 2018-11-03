using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_ChangeColor : MonoBehaviour {

	public GameObject player;
	public Color current;
	private bool _isChangeOn = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(_isChangeOn){
			player.GetComponent<SpriteRenderer>().color =  this.GetComponent<SpriteRenderer>().color;
        	current = player.GetComponent<SpriteRenderer>().color;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isChangeOn = true;
        }
    }
	private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _isChangeOn = false;
        }
    }
}
