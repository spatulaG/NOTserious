using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon : MonoBehaviour {
    public GameObject bulletPrefab;
    GameObject bullet;
    public float time;
    private float currentTime;
    public Vector2 angle;
    public Transform bulletPositon;
    private Color currentColor;


    static public Color[] colorSlot = {
        new Color(233, 233, 233),
        new Color(232, 55, 146),
        new Color(255, 225, 41),
        new Color(252, 107, 24),
        new Color(38, 197, 253),
        new Color(117, 110, 248),
        new Color(176, 238, 49),
        new Color(17, 28, 56),
    };


    /*
    Color white = new Color(233, 233, 233);//white
    Color red = new Color(232, 55, 146);//red
    Color yellow = new Color(255, 225, 41);//yellow
    Color orange = new Color(252, 107, 24);//orange
    Color blue = new Color(38, 197, 253);//blue
    Color purple = new Color(117, 110, 248);//purple
    Color green = new Color(176, 238, 49);//green
    Color black = new Color(17, 28, 56);//black
           */

    public enum color { white , red, yellow, orange, blue, purple, green, black };
    public color bulletColor;
    // Use this for initialization
    void Start () {
        currentTime = time;
    }
	
	// Update is called once per frame
	void Update () {
        print(colorSlot[(int)bulletColor]);
        currentTime -= Time.deltaTime;
        //print("current time : " + currentTime);
        //print("time : " + time);
        if (currentTime <= 0f)
        {
            
            bullet = Instantiate(bulletPrefab, bulletPositon.position, Quaternion.identity) as GameObject;//this.GetComponent<Collider2D>().bounds.size.x / 2
            bullet.GetComponent<Rigidbody2D>().AddForce(angle * 10f, ForceMode2D.Impulse);
            bullet.GetComponent<SpriteRenderer>().color = new Color(colorSlot[(int)bulletColor].r/255, colorSlot[(int)bulletColor].g / 255, colorSlot[(int)bulletColor].b / 255);
            
            bullet.transform.position = new Vector3(bullet.transform.position.x, bullet.transform.position.y, -4);
            //print("size" + this.GetComponent<Collider2D>().bounds.size.x / 2);
            //print("angle :" + this.transform.localRotation.eulerAngles);
            bullet.tag = "bullet4cannon";
            currentTime = time;
        }

        StartCoroutine(DestroyBullet(1.0f, bullet));
    }

    IEnumerator DestroyBullet(float waitTime, GameObject bullet)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(bullet);
    }
}
