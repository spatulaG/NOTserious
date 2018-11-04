using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour {

	
	private GameObject monster;
    public static bool isRespawn = false;

    // Update is called once per frame

	void Start(){
	}

    void Update () {
        if(isRespawn){
            monster = Instantiate (Resources.Load("Prefabs/monster1"), this.gameObject.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
            monster.GetComponent<MonsterHit>().canRespawn = true;
            monster.tag = "Enemy";
            isRespawn = false;
        }
                

	}
}
