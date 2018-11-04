using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private bool isInAir;

	void Start () {
        isInAir = true;
	}
	
	void Update () {
        //GetComponent<Animator>().SetTrigger("IsShootBullet");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "ground")
        {
            GetComponent<Animator>().SetTrigger("IsShootBullet");
        }
        if(collision.gameObject.tag == "rope")
        {
            Destroy(collision.gameObject);
        }
    }
}
