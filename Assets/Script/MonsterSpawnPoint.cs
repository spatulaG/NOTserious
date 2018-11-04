using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour {

    public GameObject respawnMonster;
	private MonsterHit respawnMonsterHit;
    private GameObject monster;
    public static bool isRespawn = false;

    // Update is called once per frame

	void Start(){
        respawnMonsterHit = respawnMonster.GetComponent<MonsterHit>();
	}

    void Update () {
        /*
        if(respawnMonsterHit.canRespawn && !respawnMonster){
            monster = Instantiate (Resources.Load("Prefabs/monster1"), this.gameObject.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
            respawnMonster = monster;
            respawnMonsterHit = respawnMonster.GetComponent<MonsterHit>();
            monster.GetComponent<MonsterHit>().canRespawn = true;
            monster.tag = "Enemy";
            isRespawn = false;
        }
        */
        if(isRespawn){
            StartCoroutine(spawnWait(0.3f));
        }

	}

    IEnumerator spawnWait(float time)
    {
        yield return new WaitForSeconds(time);
        monster = Instantiate(Resources.Load("Prefabs/monster1"), this.gameObject.GetComponent<Transform>().position, Quaternion.identity) as GameObject;
        monster.GetComponent<MonsterHit>().canRespawn = true;
        monster.tag = "Enemy";
        isRespawn = false;
    }
}
