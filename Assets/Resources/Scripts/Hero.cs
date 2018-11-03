using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum FacingDirection { FacingLeft,FacingRight};

public class Hero : MonoBehaviour {

    public float moveSpeed = 3.0f;
    public float bulletSpeed = 10.0f;
    public GameObject hero;

    public float JumpHeight = 2;

    private GameObject bullet;
    private FacingDirection facingDirection = FacingDirection.FacingRight;

    public bool isInAir = false;
    private bool isShooting = false;

    private bool isDead = false;
    private int HP;

    float direction;
    public float TheDistanceHeroFallBackWhenBeingAttack;

    private bool isCanAttack;
    private float attackDelay;

    Collider2D thisCollider;
    Vector3 size;
    Vector3 offset;

    void Start () {
        HP = 3;
        TheDistanceHeroFallBackWhenBeingAttack = 1;

        direction = hero.transform.localScale.x;
        isCanAttack = true;
        attackDelay = 1.0f;
        thisCollider = GetComponent<Collider2D>();
        size = thisCollider.bounds.size / 2;
        offset = thisCollider.offset;
    }
    
    public int getHP()
    {
        return HP;
    }
    public void setHP(int HP)
    {
        this.HP = HP;
    }

    void shoot()
    {
        hero.GetComponent<Animator>().SetTrigger("Shoot");
        if (facingDirection == FacingDirection.FacingLeft)
        {
            bullet = Instantiate(Resources.Load("Prefabs/bullet"), hero.transform.position - new Vector3(hero.GetComponent<Collider2D>().bounds.size.x / 2, -0.55f, 0), Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left,ForceMode2D.Impulse);
            bullet.tag = "bullet";
        }
        else
        {
            bullet = Instantiate(Resources.Load("Prefabs/bullet"), hero.transform.position + new Vector3(hero.GetComponent<Collider2D>().bounds.size.x / 2, 0.55f, 0), Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.right, ForceMode2D.Impulse);
            bullet.tag = "bullet";
        }
        StartCoroutine(DestroyBullet(1.0f,bullet));
    }

    IEnumerator DestroyBullet(float waitTime, GameObject bullet)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(bullet);
    }

    IEnumerator CanAttack(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isCanAttack = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isInAir = false;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.transform.position.x > hero.transform.position.x)
            {
                Debug.Log("Being Attacked from Right");
                hero.transform.Translate(-TheDistanceHeroFallBackWhenBeingAttack, 0, 0);
            }
            else
            {
                Debug.Log("Being Attacked from Left");
            }
            HP--;
            if (HP == 0)
            {
                isDead = true;
            }
            hero.GetComponent<Animator>().SetTrigger("IsUnderAttack");
        }
    }

    void Update () {
        
        RaycastHit2D groundCollider = Physics2D.Raycast(transform.position + new Vector3(0, -size.y - 0.1f, 0) + offset, -transform.up, 0.1f, LayerMask.GetMask("Default", "ColorGround"));
        Debug.DrawRay(transform.position + new Vector3(0, -size.y - 0.1f, 0) + offset, -transform.up * 0.1f, Color.green);


        if (groundCollider.collider == null)
        {
          //  print("NOTHING");
            isInAir = true;
        }
        else
        {
           // print(groundCollider.collider.gameObject.name);
            isInAir = false;
        }


        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Test right move");
            hero.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            hero.GetComponent<Animator>().SetBool("IsMoveRight",true);
            facingDirection = FacingDirection.FacingRight;
            hero.transform.localScale = new Vector3(direction, hero.transform.localScale.y, 1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Test left move");
            hero.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            hero.GetComponent<Animator>().SetBool("IsMoveLeft", true);
            facingDirection = FacingDirection.FacingLeft;
            hero.transform.localScale = new Vector3(-direction, hero.transform.localScale.y, 1);
        }
        else
        {
            hero.GetComponent<Animator>().SetBool("IsMoveRight", false);
            hero.GetComponent<Animator>().SetBool("IsMoveLeft", false);
        }
        if (Input.GetKeyDown(KeyCode.K) && isInAir == false)// && hero.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            //Debug.Log("Test jump");
            isInAir = true;
            hero.GetComponent<Rigidbody2D>().AddForce(Vector3.up*JumpHeight,ForceMode2D.Impulse);
            hero.GetComponent<Animator>().SetTrigger("Jump");
        }

        //if(hero.GetComponent<Rigidbody2D>().velocity.y == 0)
        //{
        //    isInAir = false;
        //}
        if (hero.transform.localEulerAngles.z != 0)
        {
            hero.transform.localEulerAngles = new Vector3(hero.transform.localEulerAngles.x, hero.transform.localEulerAngles.y, 0);
        }

        if (Input.GetKeyDown(KeyCode.J) && isShooting == false && isCanAttack == true)
        {
            shoot();
            isCanAttack = false;
            StartCoroutine(CanAttack(attackDelay));
        }

        if (isDead)
        {
            hero.GetComponent<Animator>().SetBool("IsDead", true);
            StartCoroutine(DestroyHero(2.0f, hero));
        }

    }
    IEnumerator DestroyHero(float waitTime, GameObject hero)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(hero);
    }
}
