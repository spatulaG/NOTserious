using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBroke : MonoBehaviour
{
    public GameObject[] Rocks;
    // Use this for initialization
    void Start()
    {
        for (int n = 0; n < Rocks.Length; n++)
        {

            Rocks[n].GetComponent<SpriteRenderer>().color = this.gameObject.GetComponent<SpriteRenderer>().color;
            Rocks[n].GetComponent<SpriteRenderer>().color = new Color(Rocks[n].GetComponent<SpriteRenderer>().color.r, Rocks[n].GetComponent<SpriteRenderer>().color.g, Rocks[n].GetComponent<SpriteRenderer>().color.b, 0);


        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<Block_Smash>().isDestroyed == true)
        {
            for (int n = 0; n < Rocks.Length; n++)
            {
                Broke(Rocks[n]);

            }
        }
    }

    private void Broke(GameObject Rock)
    {
        Rock.GetComponent<SpriteRenderer>().color = new Color(Rock.GetComponent<SpriteRenderer>().color.r, Rock.GetComponent<SpriteRenderer>().color.g, Rock.GetComponent<SpriteRenderer>().color.b, 1);

        Rock.gameObject.AddComponent<Rigidbody2D>();
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(this.gameObject.GetComponent<SpriteRenderer>().color.r, this.gameObject.GetComponent<SpriteRenderer>().color.g, this.gameObject.GetComponent<SpriteRenderer>().color.b, 0);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(wait(0.8f));
    }


    IEnumerator wait(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);

        for (int n = 0; n < Rocks.Length; n++)
        {
            Destroy(Rocks[n].gameObject);
        }
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
