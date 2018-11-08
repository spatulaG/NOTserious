using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorPanelActive_2 : MonoBehaviour {

	float tempTime;
	private bool isLast = false;
	public Transform[] color;
	public bool[] flag = {false,false,false};
	public GameObject popupMenu;
	public GameObject absorbMenu;
	public bag _bag;
	public bagAbsorb _bagAbsorb;
//	public static bool isDisableMenu = false;
	
	// Use this for initialization
	void Start () {
		color = GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
//		if(isDisableMenu){
//			popupMenu.SetActive(false);
//		}
		if(tempTime < 4){
			tempTime = tempTime + Time.deltaTime;

		}
		
		if(this.gameObject.transform.localScale.x < 0.07f && !flag[0]){
			this.gameObject.transform.localScale += new Vector3(tempTime/20,tempTime/20,tempTime/20);
		//	Debug.Log("expand!!!!!!!!");
		}else{
			flag[0] = true;
		}
		/*
		if(color[0].localScale.x < 0.15f && !flag[0]){
			float temp = tempTime -0.1f;
		//	color[0].localScale += new Vector3(temp/20,temp/20,temp/20);
		//	Debug.Log("expand!!!!!!!!");
			color[0].localScale += new Vector3(tempTime/20,tempTime/20,tempTime/20);
		}else if(color[0].localScale.x >= 0.15f && flag[0]){
			flag[1] = true;
			
		}*/

		if(color[1].localScale.x < 0.3f && flag[0]){
			float temp = tempTime -0.3f;
			color[1].localScale += new Vector3(temp/20,temp/20,temp/20);
		//	Debug.Log(color[1].localScale);
		//	Debug.Log("expand!!!!!!!!");
		//	color[1].localScale += new Vector3(tempTime/20,tempTime/20,tempTime/20);
		}else if(color[1].localScale.x >= 0.13f && flag[0]){
			flag[1] = true;
		}

		if(color[2].localScale.x < 0.3f && flag[1]){
			float temp = tempTime-0.3f;
			color[2].localScale += new Vector3(temp/20,temp/20,temp/20);
		//	color[2].localScale += new Vector3(tempTime/20,tempTime/20,tempTime/20);
		}else if(color[2].localScale.x >= 0.13f && flag[1]){
			flag[2] = true;
		}
		if(color[3].localScale.x < 0.3f && flag[2]){
			float temp = tempTime-0.5f;
			color[3].localScale += new Vector3(temp/20,temp/20,temp/20);	
		//	color[3].localScale += new Vector3(tempTime/20,tempTime/20,tempTime/20);
		}else if(color[3].localScale.x >= 0.13f && flag[2]){
			isLast = true;
		}

		if(isLast){
			this.gameObject.transform.parent.gameObject.GetComponent<bagAbsorb>().enabled = true;
			for(int i = 0; i < flag.Length; i++)
				flag[i] = false;
			isLast = false;
			_bag.enabled = false;
		}
	}

	private void OnEnable() {
		this.gameObject.transform.parent.gameObject.GetComponent<bag>().enabled = false;
		for(int i = 0; i < flag.Length; i++)
			flag[i] = false;
		tempTime = 0;
		isLast = false;
		_bagAbsorb.enabled = true;
//		isDisableMenu = true;
        
		
	}

	private void OnDisable() {
		for(int i = 0; i < flag.Length; i++)
			flag[i] = false;
		tempTime = 0;
		isLast = false;
		this.gameObject.transform.localScale = new Vector3(0,0,0);
		for(int i = 1; i < color.Length; i++){
			color[i].localScale = new Vector3(0,0,0);
		}
		this.gameObject.transform.parent.gameObject.GetComponent<bag>().enabled = false;
//		isDisableMenu = false;
	//	this.SetActive(false);
	}
}
