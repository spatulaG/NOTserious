using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour {
    public GameObject player;
    private Rigidbody Player;
	// Use this for initialization
	void Start () {
        Player = player.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("a")) {
            //print("a");
            Player.AddForce(-Vector3.right * 5f,ForceMode.Force);
        }
        if (Input.GetKey("d"))
        {
            //print("d");
            Player.AddForce(-Vector3.left * 5f, ForceMode.Force);
        }
    }
}
