using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
	public int[] slot = {0,0,0};
	public int slotNum = 0;
	private absorb _absorb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I) && _absorb != null){
			slotNum /= 3;
			slot[slotNum] = CheckColor(_absorb.getCurrentColor());
			Debug.Log(slot[slotNum]);
			slotNum++;
			Debug.Log("slotNum" + slotNum);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "ColorGround") {
            _absorb = GetComponent<absorb>();
        }
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
