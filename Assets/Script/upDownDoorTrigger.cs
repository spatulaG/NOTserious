using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upDownDoorTrigger : MonoBehaviour {

    private GameObject upDownDoor;
	// Use this for initialization
	void Start () {
        //upDownDoor.SetActive(true);
        upDownDoor = GameObject.FindGameObjectWithTag("upDownDoor");
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        upDownDoor.SetActive(false);
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            upDownDoor.SetActive(true);
    }


}
