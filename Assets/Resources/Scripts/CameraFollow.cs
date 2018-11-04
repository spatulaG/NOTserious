using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float speed;
    public GameObject hero;
    public Camera mainCamera;
    private Vector3 offset;
    private GameObject LeftDown;
    private GameObject RightUp;
    private GameObject BackGround;
    private float LeftAndRightAdjustNumber;
    private float UpAndDownAdjustNumber;
	void Start () {
        LeftAndRightAdjustNumber = 0.0f;
        UpAndDownAdjustNumber = 0.0f;
        LeftDown = new GameObject("LeftDown");
        RightUp = new GameObject("RightUp");
        offset = mainCamera.transform.position - hero.transform.position;
        BackGround = GameObject.FindGameObjectWithTag("background");
        hero = GameObject.FindGameObjectWithTag("Player");
        LeftDown.transform.position = new Vector3(LeftAndRightAdjustNumber - BackGround.GetComponent<SpriteRenderer>().sprite.bounds.size.x  + BackGround.transform.position.x, -UpAndDownAdjustNumber - BackGround.GetComponent<SpriteRenderer>().sprite.bounds.size.y  + BackGround.transform.position.y, LeftDown.transform.position.z);
        RightUp.transform.position = new Vector3(-LeftAndRightAdjustNumber + BackGround.GetComponent<SpriteRenderer>().sprite.bounds.size.x  + BackGround.transform.position.x, UpAndDownAdjustNumber + BackGround.GetComponent<SpriteRenderer>().sprite.bounds.size.y  + BackGround.transform.position.y, RightUp.transform.position.z);
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



        Vector3 cameraMove = transform.position + new Vector3(Input.GetAxis("Horizontal2") * Time.deltaTime * speed, Input.GetAxis("Vertical2") * Time.deltaTime * speed, 0);
        transform.position = Vector3.Slerp(transform.position, cameraMove,1);



    }
}
