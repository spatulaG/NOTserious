using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
	public int[] slot = {0,0,0};
	public int slotNum = 0;
	private absorb _absorb;
	private bool isAbsorb = false;

	public GameObject absorbPopupMenu;
	private bagAbsorb _bagAbsorb;
	private bag _bag;
	// Use this for initialization
	void Start () {
		_bagAbsorb = gameObject.GetComponent<bagAbsorb>();
		_bag = gameObject.GetComponent<bag>();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetKeyDown(KeyCode.I) && _absorb != null){
		//	Debug.Log("2222");
			slotNum = slotNum % 3;
			slot[slotNum] = CheckColor(_absorb.getCurrentColor());
		//	Debug.Log(slot[slotNum]);
			slotNum++;
		//	Debug.Log("slotNum" + slotNum);
			isAbsorb = false;
		}
		*/

		if(Input.GetKeyDown(KeyCode.I) && _absorb != null){
			
			absorbPopupMenu.SetActive(true);

			isAbsorb = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "ColorGround") {
			_bag.enabled = false;
			_bagAbsorb.enabled = true;
            _absorb = other.gameObject.GetComponent<absorb>();
			isAbsorb = true;
		//	Debug.Log("1111");
        }
    }
	void OnTriggerExit2D(Collider2D other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "ColorGround") {
			_bag.enabled = true;
			_bagAbsorb.enabled = false;
            _absorb = other.gameObject.GetComponent<absorb>();
			isAbsorb = true;
		//	Debug.Log("1111");
        }
    }

	public int ReturnColor(){
		return CheckColor(_absorb.getCurrentColor());
	}

	int CheckColor(string color){
		if(color == "white"){
			return 0;
		}else if(color == "red"){
			return 1;
		}else if(color == "yellow"){
			return 2;
		}else if(color == "orange"){
			return 3;
		}else if(color == "blue"){
			return 4;
		}else if(color == "purple"){
			return 5;
		}else if(color == "green"){
			return 6;
		}else{
			return 7;
		}
	}
    
}
