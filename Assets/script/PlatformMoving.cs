using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour {

    public GameObject defaultObj;
	
		
    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    [SerializeField] private bool reverseMove = false;
    [SerializeField] private Transform objectToUse;
    [SerializeField] private bool moveThisObject = false;

    private float startTime;
    private float journeyLength;
    private float distCovered;
    private float fracJourney;

    private float scaleX;
    private float scaleY;

    

    void Start()
    {
        startTime = Time.time;

        if (moveThisObject)
        {
            objectToUse = transform;
        }

        journeyLength = Vector3.Distance(pointA.transform.position, pointB.transform.position);
        scaleX = GetComponent<Transform>().localScale.x;
        scaleY = GetComponent<Transform>().localScale.y;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

    void Update()
    {
        distCovered = (Time.time - startTime) * moveSpeed;
        fracJourney = distCovered / journeyLength;

        if (reverseMove)
        {
            objectToUse.position = Vector3.Lerp(pointB.transform.position, pointA.transform.position, fracJourney);
        }
        else
        {
            objectToUse.position = Vector3.Lerp(pointA.transform.position, pointB.transform.position, fracJourney);
        }

        if ((Vector3.Distance(objectToUse.position, pointB.transform.position) == 0.0f || Vector3.Distance(objectToUse.position, pointA.transform.position) == 0.0f)) //Checks if the object has travelled to one of the points
        {
            if (reverseMove)
            {
                reverseMove = false;
            }
            else
            {
                reverseMove = true;
            }

            startTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Player");
        if(other.gameObject.tag == "Player"){
            Debug.Log("xxxxxxx");
            other.gameObject.GetComponent<Transform>().parent = defaultObj.GetComponent<Transform>();
            Debug.Log(other.gameObject.GetComponent<Transform>().localScale.x);
            other.gameObject.GetComponent<Transform>().localScale = new Vector3(other.gameObject.GetComponent<Transform>().localScale.x/scaleX,other.gameObject.GetComponent<Transform>().localScale.y/scaleY, 1.0f);
        //    other.GetComponent<Transform>().parent = this.GetComponent<Transform>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<Transform>().parent = null;
        }
    }
    
}