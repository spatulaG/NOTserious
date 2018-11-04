using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMenuActive : MonoBehaviour {

	float tempTime;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(tempTime < 1){
			tempTime = tempTime + Time.deltaTime;
		}if(this.gameObject.transform.localScale.x < 0.13f){
			this.gameObject.transform.localScale += new Vector3(tempTime/50,tempTime/50,tempTime/50);
		}
	}

	private void OnEnable() {
		tempTime = 0;
		this.gameObject.transform.parent.gameObject.GetComponent<bag>().enabled = false;
		
	}

	private void OnDisable() {
		
	}
}
