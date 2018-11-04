using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallColorChangeTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.gameObject.tag == "Player"){
			Wall_ChangeColor.canChange = true;
		}
	}
}
