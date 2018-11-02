using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class absorb : MonoBehaviour {
    public bool isImgOn;
    public GameObject img;
    public int color;
    public GameObject player;
    private GameObject color1, color2, color3;
    private Renderer rend;
    public GameObject popUpMenu;

    // Use this for initialization
    void Start () {
        color1 = GameObject.Find("Color1");
        color2 = GameObject.Find("Color2");
        color3 = GameObject.Find("Color3");
        
        img.SetActive(false);
        isImgOn = false;
        rend = player.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check switch if button down tsu ma ra nai
        attack();
        //absorb
        absorbColor();
        //If Tag player pop-up menu(UI)
        
    }

    Color white = new Color(233, 233, 233);//white
    Color red = new Color(232, 55, 146);//red
    Color yellow = new Color(255, 225, 41);//yellow
    Color orange = new Color(252, 107, 24);//orange
    Color blue = new Color(38, 197, 253);//blue
    Color purple = new Color(117, 110, 248);//purple
    Color green = new Color(176, 238, 49);//green
    Color black = new Color(17, 28, 56);//black
         
         
    private void attack()
    {

    }

    private void absorbColor()
    {
        if (Input.GetKey("j") && isImgOn)
        {
            popUpMenu.SetActive(true);
            rend.material.SetColor("_Color", this.GetComponent<Renderer>().material.GetColor("_Color"));
            


        }
    }

    public string getCurrentColor() {
        string currentColor = "";
        rend.material.GetColor("_Color");









        return currentColor;
    }

    void OnTriggerEnter(Collider other)
    {
        print("trigger");
        //Check Tag
        if(other.tag == "Player") {
            print("player!");
            if (isImgOn == false)
            {
                img.SetActive(true);
                isImgOn = true;
            }
            
            
        }
            //Switch
    }

    private void OnTriggerExit(Collider other)
    {
        if (isImgOn == true)
        {

            img.SetActive(false);
            isImgOn = false;
            
        }
    }
}
