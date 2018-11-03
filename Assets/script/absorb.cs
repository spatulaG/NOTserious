using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class absorb : MonoBehaviour {
    public bool isImgOn;
    public GameObject img;
    public GameObject player;
    private GameObject color1, color2, color3;
   // public GameObject popUpMenu;
    public Color current;
    //public PopUpMenu pop;

    // Use this for initialization
    void Start () {
        

        color1 = GameObject.Find("Color1");
        color2 = GameObject.Find("Color2");
        color3 = GameObject.Find("Color3");
        
        img.SetActive(false);
        isImgOn = false;
        current = player.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        current = player.GetComponent<SpriteRenderer>().color;
        //absorb
        absorbColor();
        //If Tag player pop-up menu(UI)
        
    }
    

    static public Color[] colorSlot = {
        new Color(233, 233, 233),
        new Color(232, 55, 146),
        new Color(255, 225, 41),
        new Color(252, 107, 24),
        new Color(38, 197, 253),
        new Color(117, 110, 248),
        new Color(176, 238, 49),
        new Color(17, 28, 56),
    };
    /*
    Color white = new Color(233, 233, 233);//white
    Color red = new Color(232, 55, 146);//red
    Color yellow = new Color(255, 225, 41);//yellow
    Color orange = new Color(252, 107, 24);//orange
    Color blue = new Color(38, 197, 253);//blue
    Color purple = new Color(117, 110, 248);//purple
    Color green = new Color(176, 238, 49);//green
    Color black = new Color(17, 28, 56);//black
           */

    private void absorbColor()
    {
        if (Input.GetKey("i") && isImgOn)
        {
            //print("yes");
        //    popUpMenu.SetActive(true);
            player.GetComponent<SpriteRenderer>().color =  this.GetComponent<SpriteRenderer>().color;
            current = player.GetComponent<SpriteRenderer>().color;
            //print(current);


        }
    }
    //PopUpMenu a;
    public string getCurrentColor() {
        string currentColor = "";
        int colorID = 0;
        for (int n = 0; n < 8; n++) {
            if (Math.Abs(current.r * 255 - colorSlot[n].r) <= 0.5)
            {

                colorID = n;

            }
        }
        switch (colorID) {

            case 0:
                currentColor = "white";
            //    Debug.Log("White");
                break;
            case 1:
                currentColor = "red";
            //    Debug.Log("red");
                break;
            case 2:
                currentColor = "yellow";
            //    Debug.Log("yellow");
                break;
            case 3:
                currentColor = "orange";
            //    Debug.Log("orange");
                break;
            case 4:
                currentColor = "blue";
            //    Debug.Log("blue");
                break;
            case 5:
                currentColor = "purple";
            //    Debug.Log("purple");
                break;
            case 6:
                currentColor = "green";
            //    Debug.Log("green");
                break;
            case 7:
                currentColor = "black";
            //    Debug.Log("black");
                break;
        }

        return currentColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //print("trigger");
        //Check Tag
        if(other.tag == "Player") {
            //print("player!");
            if (isImgOn == false)
            {
                img.SetActive(true);
                isImgOn = true;
            }
            
            
        }
            //Switch
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (isImgOn == true)
        {

            img.SetActive(false);
            isImgOn = false;
            
        }
    }
}
