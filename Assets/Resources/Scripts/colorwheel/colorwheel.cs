using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorwheel : MonoBehaviour {

    public float speed;
    public Text Head;
    public Text Body;
    public Canvas canvas;
    float timer;

    public GameObject wheel;
    public GameObject[] colors = new GameObject[6];

    int current = 0;
    int oldCurrent = 0;
    float moveTimer;

    // Use this for initialization
    void Start () {
        timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float rotation = Input.GetAxis("Vertical") * speed;
        
        rotation *= Time.deltaTime;
        
        // Rotate around our y-axis
        wheel.transform.Rotate(0, 0, rotation);

        if (rotation == 0)
        {
            wheel.transform.localEulerAngles = RotateAxisToNearestSide(wheel.transform.localEulerAngles);

            oldCurrent = current;
        }
        else
            timer = Time.time;


        string color = "";

        

        int currentColorRotation = (int)(Mathf.Round(wheel.transform.localEulerAngles.z / 60f) * 60f);


        switch (currentColorRotation)
        {
            case 0:
                color = "Blue";
                Head.text = color;
                Body.text = "The color " + color + " is one of the primary colors";
                current = 0;
                break;
            case 360:
                color = "Blue";
                Head.text = color;
                Body.text = "The color " + color + " is one of the primary colors";
                current = 0;
                break;
            case 60:
                color = "Green";
                Head.text = color;
                Body.text = "The color " + color + " is the complementary color of the base color red.\n\nCombined by the colors blue and yellow";
                current = 1;
                break;
            case 120:
                color = "Yellow";
                Head.text = color;
                Body.text = "The color " + color + " is one of the primery colors";
                current = 2;
                break;
            case 180:
                color = "Orange";
                Head.text = color;
                Body.text = "The color " + color + " is the complementary color of the base color blue.\n\nCombined by the colors yellow and red";
                current = 3;
                break;
            case 240:
                color = "Red";
                Head.text = color;
                Body.text = "The color " + color + " is one of the primery colors";
                current = 4;
                break;
            case 300:
                color = "Purple";
                Head.text = color;
                Body.text = "The color " + color + " is the complementary color of the base color yellow.\n\nCombined by the colors red and blue";
                current = 5;
                break;

        }

   

     

        Vector3 start = transform.position;
        Vector3 end = new Vector3(transform.position.x -1, transform.position.y, -0.9f);
        //start = transform.TransformVector(colors[current].transform.position);
        //end = transform.TransformVector(colors[current].transform.position + new Vector3(3, 0, 0));

        if(moveTimer >= 1)
            canvas.GetComponent<Canvas>().enabled = true;
        else

            canvas.GetComponent<Canvas>().enabled = false;

        for (int i = 0; i < colors.Length; i++)
        {

            if (i == current)
            {
                moveTimer = (Time.time - timer)*2 / 1;
                colors[current].transform.position = transform.TransformVector(Vector3.Slerp(start, end, moveTimer));
                colors[current].transform.localScale = transform.TransformVector(Vector3.Slerp(Vector3.one, new Vector3(2,2,colors[current].transform.localScale.z), moveTimer));
            }
            else
            {
                colors[i].transform.position = start;
                colors[i].transform.localScale = Vector3.one;
            }
        }

  
    }

    Vector3 RotateAxisToNearestSide(Vector3 eulerAngles)
    {
        Vector3 roundedEulerAngles = RoundToNearest60Degree(eulerAngles);
        return Vector3.Slerp(eulerAngles, roundedEulerAngles, Time.deltaTime * speed/40);
    }

    Vector3 RoundToNearest60Degree(Vector3 eulerAngles)
    {
        for (int i = 0; i < 3; i++)
        {
            eulerAngles[i] = Mathf.Round(eulerAngles[i] / 60f) * 60f;
        }
        return eulerAngles;
    }
}
