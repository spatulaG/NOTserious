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
    public GameObject[] compColors = new GameObject[6];

    public float zValue = 0.5f;
    public float zValue2 = 8;
    public float Z;

    int current = 0;
    int oldCurrent = 0;
    float moveTimer;

    Vector3 gravity;

    Vector3 startScale;
    Vector3 endScale;
    Vector3 startPos;
    Vector3 endPos;
    float moveTimer2;
    float timer2;

    bool firstTime = true;


    // Use this for initialization
    void Start () {
        timer = Time.time;
     //   timer2 = Time.time;
        gravity = Physics.gravity;

        startScale = new Vector3(0.3f, 0.3f, 1);
        endScale = new Vector3(1, 1, 1);
        transform.localScale = startScale;

        startPos = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.8f, 0));
        startPos.z = zValue;
        transform.position = startPos;


        endPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, zValue);

        for (int i = 0; i < compColors.Length; i++)
        {
            Color tempColor = compColors[i].GetComponent<Renderer>().material.color;
            tempColor.a = 0;
            compColors[i].GetComponent<Renderer>().material.color = tempColor;
        }

    }

    bool paused = false;
 

    void OnPauseGame()
    {
        Physics2D.gravity = new Vector3(0, 0, 0);
       // paused = true;

    }
    void OnResumeGame()
    {
        Physics2D.gravity = gravity;
     //  paused = false;
    }
    

    // Update is called once per frame
    void Update ()
    {
        // transform.position = new Vector3(transform.position.x, transform.position.y, zValue);
        


        startPos = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.8f, 0));
        startPos.z = transform.position.z;
        //endPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, startPos.z);
        endPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Z);

        //Put on middle of screen

        PasueGame();


        moveTimer2 = (Time.time - timer2) * 2 / 1;


        if (!paused)
        {
            timer = Time.time;
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].transform.position = new Vector3(transform.position.x,transform.position.y, -zValue2);
                colors[i].transform.localScale = Vector3.one;


                Color tempColor = compColors[i].GetComponent<Renderer>().material.color;
                tempColor.a = 0;
                compColors[i].GetComponent<Renderer>().material.color = tempColor;
            }
            
            if(firstTime)
            {
                moveTimer2 = 1;
            }

            canvas.GetComponent<Canvas>().enabled = false;
            wheel.transform.localEulerAngles = RotateAxisToNearestSide(wheel.transform.localEulerAngles);

            transform.position = Vector3.Slerp(endPos, startPos, moveTimer2);
            transform.localScale = Vector3.Slerp(endScale, startScale, moveTimer2);
            transform.position = new Vector3(transform.position.x, transform.position.y, zValue);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);



            return;
        }
        transform.position = Vector3.Slerp(startPos, endPos, moveTimer2);
        transform.localScale = Vector3.Slerp(startScale,endScale, moveTimer2);
        transform.position = new Vector3(transform.position.x, transform.position.y, zValue);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);


        if (moveTimer2 < 1)
        {
            timer = Time.time;
        }
        else if (moveTimer2 >= 1)
        {
            firstTime = false;
            
            float rotation = Input.GetAxis("Vertical") * speed;

            rotation *= Time.unscaledDeltaTime;

            // Rotate around our z-axis
            wheel.transform.Rotate(0, 0, rotation);


            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                wheel.transform.localEulerAngles = new Vector3(0, 0, 120);
            }


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
                    Body.text = "The color " + color + " is the complementary color of the primery color blue.\n\nCombined by the colors yellow and red";
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
                    Body.text = "The color " + color + " is the complementary color of the primery color yellow.\n\nCombined by the colors red and blue";
                    current = 5;
                    break;

            }





            Vector3 start = new Vector3(transform.position.x, transform.position.y, -zValue2);
            Vector3 end = new Vector3(transform.position.x - 1, transform.position.y, -zValue2-0.1f);
            //start = transform.TransformVector(colors[current].transform.position);
            //end = transform.TransformVector(colors[current].transform.position + new Vector3(3, 0, 0));

            if (moveTimer >= 1)
                canvas.GetComponent<Canvas>().enabled = true;
            else
                canvas.GetComponent<Canvas>().enabled = false;

            for (int i = 0; i < colors.Length; i++)
            {

                if (i == current)
                {
                    moveTimer = (Time.time - timer) * 2 / 1;
                    colors[current].transform.position = transform.TransformVector(Vector3.Slerp(start, end, moveTimer));
                    colors[current].transform.localScale = transform.TransformVector(Vector3.Slerp(Vector3.one, new Vector3(2, 2, colors[current].transform.localScale.z), moveTimer));

                    compColors[current].transform.position = transform.TransformVector(Vector3.Slerp(start, end, moveTimer));
                    compColors[current].transform.localScale = transform.TransformVector(Vector3.Slerp(Vector3.one, new Vector3(2, 2, compColors[current].transform.localScale.z+0.01f), moveTimer));

                    Color tempColor = compColors[i].GetComponent<Renderer>().material.color;
                    tempColor.a = moveTimer > 1 ? 1 : moveTimer;
                    compColors[i].GetComponent<Renderer>().material.color = tempColor;

                }
                else
                {
                    colors[i].transform.position = start;
                    colors[i].transform.localScale = Vector3.one;


                    compColors[i].transform.position = start;
                    compColors[i].transform.localScale = Vector3.one;

                    Color tempColor = compColors[i].GetComponent<Renderer>().material.color;
                    tempColor.a = 0;
                    compColors[i].GetComponent<Renderer>().material.color = tempColor;
                }
            }
        }

        
    }

    private void PasueGame()
    {
      
            if (Input.GetKeyDown(KeyCode.P)||Input.GetKeyDown(KeyCode.JoystickButton6)|| Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            timer2 = Time.time;
            timer = Time.time;
            moveTimer = 0;
            moveTimer2 = 0;

            Object[] objects = FindObjectsOfType(typeof(GameObject));
            foreach (GameObject go in objects)
            {
                Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
                Animation ani = go.GetComponent<Animation>();

                if (paused)
                {
                    go.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
                    if (rb != null) rb.WakeUp();
                    if (ani != null) ani.enabled = false;

                }
                else
                {
                    go.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
                    if (rb != null) rb.Sleep();
                    if (ani != null)
                        ani.enabled = true;

                }
            }

            paused = !paused;
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
