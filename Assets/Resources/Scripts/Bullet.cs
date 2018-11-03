using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private bool isInAir;

	void Start () {
        isInAir = true;
	}
	
	void Update () {
        GetComponent<Animator>().SetTrigger("IsShootBullet");
    }
}
