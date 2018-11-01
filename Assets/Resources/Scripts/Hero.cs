using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FacingDirection { FacingLeft,FacingRight};

public class Hero : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float bulletSpeed = 10.0f;
    public Transform hero;
    private bool isInAir = false;
    private bool isShooting = false;
    public GameObject bullet;
    private FacingDirection facingDirection = FacingDirection.FacingRight;
    void Start () {
		
	}
    
    void shoot()
    {
        if(facingDirection == FacingDirection.FacingLeft)
        {
            bullet = Instantiate(Resources.Load("Prefabs/bullet"), hero.transform.position - new Vector3(hero.GetComponent<Collider2D>().bounds.size.x / 2, 0, 0), Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left,ForceMode2D.Impulse);
        }
        else
        {
            bullet = Instantiate(Resources.Load("Prefabs/bullet"), hero.transform.position + new Vector3(hero.GetComponent<Collider2D>().bounds.size.x / 2, 0, 0), Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.right, ForceMode2D.Impulse);
        }
        StartCoroutine(DestroyBullet(1.0f,bullet));
    }

    IEnumerator DestroyBullet(float waitTime, GameObject bullet)
    {
        yield return new WaitForSeconds(waitTime);
        //等待之后执行的动作  
        Destroy(bullet);
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Test right move");
            hero.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            hero.GetComponent<Animator>().SetBool("IsMoveRight",true);
            facingDirection = FacingDirection.FacingRight;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Test left move");
            hero.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            hero.GetComponent<Animator>().SetBool("IsMoveLeft", true);
            facingDirection = FacingDirection.FacingLeft;
        }
        else
        {
            hero.GetComponent<Animator>().SetBool("IsMoveRight", false);
            hero.GetComponent<Animator>().SetBool("IsMoveLeft", false);
        }
        if (Input.GetKeyDown(KeyCode.K) && isInAir == false)
        {
            Debug.Log("Test jump");
            isInAir = true;
            hero.GetComponent<Rigidbody2D>().AddForce(Vector3.up,ForceMode2D.Impulse);
            hero.GetComponent<Animator>().SetTrigger("Jump");
        }

        if(hero.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            isInAir = false;
        }
        if (hero.transform.localEulerAngles.z != 0)
        {
            hero.transform.localEulerAngles = new Vector3(hero.transform.localEulerAngles.x, hero.transform.localEulerAngles.y, 0);
        }

        if(Input.GetKeyDown(KeyCode.J) && isShooting == false)
        {
            shoot();
        }
    }
}
