using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoungMgr : MonoBehaviour {
    private GameObject[] enemys;
    private int length = 0;
    public AudioSource[] SFX;
    // Update is called once per frame
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
            int playNum;
            playNum = Random.Range(0, SFX.Length);
            

        }
        else
            length = enemys.Length;
	}
}
