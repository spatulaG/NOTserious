using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class countDown : MonoBehaviour {
    float timeLeft = 5.0f;
    //public Image img;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        print("timeleft : " + timeLeft);
        if (timeLeft < 0)
        {
            hide();
        }
    }

    private void hide()
    {
        this.gameObject.SetActive(false);
    }
}
