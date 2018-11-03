using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour {

	private float timerOne = 1f;

    private float timeOne = 6.0f;

    private float timerWave = 0f;

    private float timeWave = 10.0f;

    private int countPerWave = 0;

	private GameObject monster;

    // Update is called once per frame

	void Start(){
	}

    void Update () {

        timerWave += Time.deltaTime;

        if(timerWave < timeWave) {
            timerOne += Time.deltaTime;
            if(timerOne > timeOne) {
                monster = Instantiate (Resources.Load("Prefabs/monster1"), this.gameObject.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
                timerOne -= timeOne;
            }
        }
       if (timerWave >= timeWave) {
            timerWave -= timeWave;
            countPerWave = 0;
        }

	}

	
	private void DestroySelf6Second(){
		Destroy(monster, 6);
	}
}
