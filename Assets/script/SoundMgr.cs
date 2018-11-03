using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour {
    private GameObject[] enemys;
    private int length = 0;
    public AudioClip[] SFXs;
    private AudioSource sfx;
    // Update is called once per frame
    //MusicManager musicManager;
    private void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        length = enemys.Length;
    }
    void Update () {
        
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemys.Length < length)
        {
            //playsound
            print("play sfx");
            int playNum;
            playNum = Random.Range(0, SFXs.Length-1);
            print(playNum);

            switch (playNum) {
                case 0:
                    MusicManager.instance.PlaySE("MonsterSE1");
                    break;
                case 1:
                    MusicManager.instance.PlaySE("MonsterSE2");
                    break;
                case 2:
                    MusicManager.instance.PlaySE("MonsterSE3");
                    break;
                case 3:
                    MusicManager.instance.PlaySE("MonsterSE4");
                    break;


            }
            length = enemys.Length;
        }
        //else
        //    length = enemys.Length;
	}
}
