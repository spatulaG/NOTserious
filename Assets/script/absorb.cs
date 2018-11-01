using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class absorb : MonoBehaviour {
    public bool isImgOn;
    public Image img;
    public int color;
    public GameObject player;
    private Renderer rend;
    // Use this for initialization
    void Start () {

        img.enabled = false;
        isImgOn = false;
        rend = player.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check switch if button down
        attack();
        //absorb
        absorbColor();
        //If Tag player pop-up menu(UI)
        
    }

    private void attack()
    {

    }

    private void absorbColor()
    {
        if (Input.GetKey("j") && isImgOn)
        {
            print("ABSORB!!");
            switch (color)
            {
                case 0:
                    rend.material.SetColor("_Color", Color.white);
                    break;
                case 1:
                    rend.material.SetColor("_Color", Color.red);
                    break;
                case 2:
                    rend.material.SetColor("_Color", Color.yellow);
                    break;
                case 3:
                    rend.material.SetColor("_Color", Color.blue);
                    break;
                case 4:
                    rend.material.SetColor("_Color", Color.green);
                    break;
                case 5:
                    rend.material.SetColor("_Color", Color.black);
                    break;
                case 6:
                    rend.material.SetColor("_Color", Color.gray);
                    break;

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("trigger");
        //Check Tag
        if(other.tag == "Player") {
            print("player!");
            if (isImgOn == false)
            {
                img.enabled = true;
                isImgOn = true;
            }
            
            
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
