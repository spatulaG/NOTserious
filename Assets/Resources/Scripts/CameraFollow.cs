using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float speed;
    public float moveDistance;

    float startTime;
    float distance;

    public GameObject hero;
    public Camera mainCamera;
    private Vector3 offset;
    private GameObject LeftDown;
    private GameObject RightUp;
    private GameObject BackGround;
    private float LeftAndRightAdjustNumber;
    private float UpAndDownAdjustNumber;
	void Start () {
        LeftAndRightAdjustNumber = -20.0f;
        UpAndDownAdjustNumber =10.0f;
        LeftDown = new GameObject("LeftDown");
        RightUp = new GameObject("RightUp");
        offset = mainCamera.transform.position - hero.transform.position;
        BackGround = GameObject.FindGameObjectWithTag("background");
        hero = GameObject.FindGameObjectWithTag("Player");
        LeftDown.transform.position = new Vector3(LeftAndRightAdjustNumber - BackGround.GetComponent<SpriteRenderer>().sprite.bounds.size.x  + BackGround.transform.position.x, -UpAndDownAdjustNumber - BackGround.GetComponent<SpriteRenderer>().sprite.bounds.size.y  + BackGround.transform.position.y, LeftDown.transform.position.z);
        RightUp.transform.position = new Vector3(-LeftAndRightAdjustNumber + BackGround.GetComponent<SpriteRenderer>().sprite.bounds.size.x  + BackGround.transform.position.x, UpAndDownAdjustNumber + BackGround.GetComponent<SpriteRenderer>().sprite.bounds.size.y  + BackGround.transform.position.y, RightUp.transform.position.z);

        startTime = Time.time;

        // Calculate the journey length.
        
    }

    // Update is called once per frame

    private void FixedUpdate()
    { 
        Vector3 newPosition = offset + hero.transform.position;
        if (newPosition.x < LeftDown.transform.position.x) 
        {
            newPosition.x = LeftDown.transform.position.x;
        }
        else if(newPosition.x > RightUp.transform.position.x)
        {
            newPosition.x = RightUp.transform.position.x;
        }
        else if(newPosition.y > RightUp.transform.position.y)
        {
            newPosition.y = RightUp.transform.position.y;
        }
        else if(newPosition.y < LeftDown.transform.position.y)
        {
            newPosition.y = LeftDown.transform.position.y;
        }



        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newPosition, 1.0f);




        //Vector3 cameraMove =  new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * moveDistance, Input.GetAxis("Vertical") * Time.deltaTime * moveDistance, 0);
        Vector3 cameraMove = new Vector3(Input.GetAxis("Axis 4") * Time.deltaTime * moveDistance, -Input.GetAxis("Axis 5") * Time.deltaTime * moveDistance, 0);
        //if (cameraMove == Vector3.zero)
        //    startTime = Time.time;

        distance = Vector3.Distance(transform.position, transform.position + cameraMove);
        float distCovered = (Time.time - startTime) * speed;
        
        float fracJourney = distCovered / distance;

        
        transform.position = Vector3.Slerp(transform.position, transform.position + cameraMove,fracJourney);



    }
}
