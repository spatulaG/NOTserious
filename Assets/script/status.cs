using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class status : MonoBehaviour {
    public int HP = 3;
    public int[] slot = {0,0,0};
    public GameObject bullet;
    private void Update()
    {
        GameObject[] bullet = GameObject.FindGameObjectsWithTag("bullet");
        for(int n=0;n<bullet.Length;n++)
            bullet[n].GetComponent<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color;
    }

}
