using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenColor : MonoBehaviour {

    private GameObject GreenTrigger;
    private GameObject newObject;
    private GameObject Hair;
	// Use this for initialization
	void Start () {
        
    }
    void GreenColorAppear()
    {
        GreenTrigger = GameObject.FindGameObjectWithTag("GreenTrigger");
        Hair = GameObject.FindGameObjectWithTag("HAIR");
        Vector3 pos = GreenTrigger.transform.position;
        Destroy(GreenTrigger);
        newObject = Instantiate(Resources.Load("Prefabs/Green 1"), pos, Quaternion.identity) as GameObject;
        newObject.GetComponent<absorb>().player = Hair;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            GreenColorAppear();
        }
    }
    // Update is called once per frame
    void Update () {

	}
}
