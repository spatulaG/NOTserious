using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit : MonoBehaviour {
    private Color current;
	// Use this for initialization
	void Start () {
        current = this.GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet") {
            
            if (Oppo(getColor(collision.GetComponent<SpriteRenderer>().color)) == getColor(this.GetComponent<SpriteRenderer>().color))
            {
                print("enemy dead");

                Destroy(this.gameObject);
            }
            else {

                //print("bullet color" + collision.GetComponent<SpriteRenderer>().color.r * 255);
                //print("enemy color" + this.GetComponent<SpriteRenderer>().color.r * 255);
                print("wrong color!");
            }
            Destroy(collision);
        }

    }

    public string Oppo(string color)
    {
        Color oppoColor = Color.white;
        String oppoName = "";
        switch (color) {
            case "red":
                oppoName = "green";
                oppoColor = colorSlot[6];
                break;
            case "black":
                oppoName = "white";
                oppoColor = colorSlot[0];
                break;
            case "yellow":
                oppoName = "purple";
                oppoColor = colorSlot[5];
                break;
            case "purple":
                oppoName = "yellow";
                oppoColor = colorSlot[2];
                break;
            case "white":
                oppoName = "black";
                oppoColor = colorSlot[7];
                break;
            case "green":
                oppoName = "red";
                oppoColor = colorSlot[1];
                break;
            case "blue":
                oppoName = "orange";
                oppoColor = colorSlot[3];
                break;
            case "orange":
                oppoName = "blue";
                oppoColor = colorSlot[4];
                break;
        }

        if (oppoName == "")
            print("get oppo color failed");
        print("oppo:" + oppoName);
        return oppoName;
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
    public string getColor(Color color)
    {
        //print("R" + color.r * 255);
        string currentColor = "";
        int colorID = 0;
        //print(color.r+"!!!!!!!!!!!!!!!!!!!!");
        for (int n = 0; n < 8; n++)
        {
            //print(color.r * 255 - colorSlot[n].r * 255);
            if (Math.Abs(color.r*255 - colorSlot[n].r) <= 0.5) {
                
                colorID = n;
               
            }
            
            
            
        }
        switch (colorID)
        {

            case 0:
                currentColor = "white";
                break;
            case 1:
                currentColor = "red";
                break;
            case 2:
                currentColor = "yellow";
                break;
            case 3:
                currentColor = "orange";
                break;
            case 4:
                currentColor = "blue";
                break;
            case 5:
                currentColor = "purple";
                break;
            case 6:
                currentColor = "green";
                break;
            case 7:
                currentColor = "black";
                break;
        }
        print(currentColor);
        return currentColor;
    }


    
}
