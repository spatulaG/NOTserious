using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject hero;
    public Camera mainCamera;
    private Vector3 offset;
	void Start () {
        offset = mainCamera.transform.position - hero.transform.position;
        //offset /= 3;//fr
	}
	
	// Update is called once per frame
	void Update () {
        mainCamera.transform.position = offset + hero.transform.position;
	}
}
