using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFell : MonoBehaviour {
    public bool isDead = false;
    public GameObject CheckPoint;
    public GameObject player;
    public GameObject camera;
    public float waitTime = 1.0f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            isDead = true;
            getStatus();

            StartCoroutine(Wait(waitTime));


            CheckPoint.GetComponent<CheckPoint>().isActive = false;
        }
    }

    private void getStatus()
    {
        print("Falling!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

        //TODO : player HP-1
        print("player:" + player.transform.position);
        print("check point:" + CheckPoint.transform.position);
        print("camera:" + camera.transform.position);
    }




    IEnumerator Wait(float time)
    {
            yield return new WaitForSeconds(time);
       
            player.transform.position = CheckPoint.GetComponent<CheckPoint>().playerPos;
            camera.transform.position = CheckPoint.GetComponent<CheckPoint>().cameraPos;
    }
}
