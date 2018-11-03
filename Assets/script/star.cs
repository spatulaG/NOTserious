using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class star : MonoBehaviour {
    public Color[] color;
    private int Length;
    private Color current;
    private Vector3 scale;
    public GameObject img;
    //public Button btn;
    // Use this for initialization

    void Start() {
        Length = color.Length;
        scale = this.transform.localScale;
    }
    int n = 0;
    // Update is called once per frame
    void Update() {
        print(scale);
        //print(this.transform.localScale);
        this.transform.localScale = new Vector3(scale.x + Mathf.Abs(Mathf.Sin(Time.deltaTime)), scale.y + Mathf.Abs(Mathf.Sin(Time.deltaTime)), scale.z);



        if (Mathf.Abs(this.GetComponent<SpriteRenderer>().color.r - color[n].r) > 0.05)
        {
            this.GetComponent<SpriteRenderer>().color = Color.Lerp(this.GetComponent<SpriteRenderer>().color, color[n], Time.deltaTime * 10);
        }

        else
            n++;
        if (n == Length)
            n = 0;



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("entered");
        if (collision.tag == "Player") {
            print("player entered");
            img.SetActive(true);
            Destroy(this.gameObject);
            //btn.enabled = true;
        }
    }
}
