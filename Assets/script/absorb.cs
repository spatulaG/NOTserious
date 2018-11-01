using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class absorb : MonoBehaviour {
    public bool isImgOn;
    public Image img;
    public int color;
    public GameObject player;
    status Status;
    // Use this for initialization
    void Start () {

        img.enabled = false;
        isImgOn = false;
        Status = player.GetComponent<status>();
    }
	
	// Update is called once per frame
	void Update () {
		//Check switch if button down
        //absorb
	}
    
    void OnTriggerEnter(Collider other)
    {
        print("trigger");
        //Check Tag
        other.tag = "Player";
        if (isImgOn == false)
        {
            img.enabled = true;
            isImgOn = true;
        }
            //If Tag player pop-up menu(UI)
            if (Input.GetKey("j")) {
                print("ABSORB!!");
            status.absorbColor(color);

        }
            //Switch
    }

    private void OnTriggerExit(Collider other)
    {
        if (isImgOn == true)
        {

            img.enabled = false;
            isImgOn = false;
        }
    }
}
