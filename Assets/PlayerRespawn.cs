using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {

	private Hero _hero;
    public GameObject camera;
	public GameObject checkPoint;
	private float waitTime = 1.0f;

	// Use this for initialization
	void Start () {
		_hero = GetComponent<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
		OnCheckHP();		
	}

	void OnCheckHP(){
		if (_hero.getHP() == 0)
        {
            StartCoroutine(Wait(waitTime));
            checkPoint.GetComponent<CheckPoint>().isActive = false;
        }
	}

	IEnumerator Wait(float time)
    {
            yield return new WaitForSeconds(time);
       
            gameObject.transform.position = checkPoint.GetComponent<CheckPoint>().playerPos;
            camera.transform.position = checkPoint.GetComponent<CheckPoint>().cameraPos;
    }
}
