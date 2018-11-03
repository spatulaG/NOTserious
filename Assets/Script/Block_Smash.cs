using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Smash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet") {
            Debug.Log("1111");
            if (Mathf.Abs(this.GetComponent<SpriteRenderer>().color.r*255 - collision.gameObject.GetComponent<SpriteRenderer>().color.r*255) < 0.5f)
            {
			//	Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
            
        }

    }
}
