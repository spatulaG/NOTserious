using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    public GameObject player;
    public GameObject camera;
    public bool isActive = false;
    public Vector3 playerPos, cameraPos;
    //public GameObject wheel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            isActive = true;
            print("Check point activated");
            playerPos = new Vector3(player.transform.position.x, player.transform.position.y + 1.0f, player.transform.position.z);
            //wheel.gameObject.GetComponent<Transform>().position = new Vector3(wheel.gameObject.GetComponent<Transform>().position.x, wheel.gameObject.GetComponent<Transform>().position.y, wheel.gameObject.GetComponent<Transform>().position.z + 5.0f);
            cameraPos = camera.transform.position;

            print("playerPos:"+playerPos);
            print("cameraPos:"+cameraPos);

        }

    }
        
}
