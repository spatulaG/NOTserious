using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum Type
    {
        Jumping,
        NoneJumping
    }

    public Type enemyType = Type.NoneJumping;

    public bool MoveOnStart = false;

    public GameObject Blob;
    List<GameObject> bloblist = new List<GameObject>();

    Vector3 direction;
    public float MaxSpeed;

    float speed;

    public float MaxJumpWidth;
    public float JumpHeight = 3;

    public bool onGround = false;
    Vector3 size;
    Vector3 offset;
    float timer;
    int randomtime;

    Rigidbody2D rb;


    RaycastHit2D right;
    RaycastHit2D left;
    RaycastHit2D jumpcollider;

    Collider2D thisCollider;
    int layermask;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<Collider2D>();
        layermask = LayerMask.GetMask("Default");


        size = thisCollider.bounds.size / 2;
            offset = thisCollider.offset;
        
        if(MoveOnStart)
            direction.x = -1;
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "ground")
        {
            onGround = true;
            speed *= 0.5f;
        }
        if (collision.gameObject.tag == "bullet")
        {
           // GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<SpriteRenderer>().color;
            bloblist.Add(Instantiate(Blob, collision.gameObject.transform.position, Quaternion.identity, transform));
            bloblist[bloblist.Count-1].GetComponent<SpriteRenderer>().color = collision.gameObject.GetComponent<SpriteRenderer>().color;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!MoveOnStart && direction.x == 0 && other.tag == "MainCamera")
        {
            direction.x = -1;
        }
        if (other.tag == "ground")
        {
            GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<SpriteRenderer>().color;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (speed < MaxSpeed)
            speed += Time.deltaTime;


        CheckWall();

        //Check if at end of platform
        right = Physics2D.Raycast(transform.position + new Vector3(size.x, -size.y - 0.1f, 0) + offset, -transform.up, 0.4f,layermask);
        left = Physics2D.Raycast(transform.position + new Vector3(-size.x, -size.y - 0.1f, 0) + offset, -transform.up, 0.4f,layermask);
        //Draw debug Rays
        Debug.DrawRay(transform.position + new Vector3(size.x, -size.y - 0.1f, 0) + offset, -transform.up * 0.4f, Color.green);
        Debug.DrawRay(transform.position + new Vector3(-size.x, -size.y - 0.1f, 0) + offset, -transform.up * 0.4f, Color.green);


        //Check platform infront
        Vector2 jumpTo = transform.position + new Vector3((size.x + MaxJumpWidth * 0.6f) * direction.x, -size.y - 0.2f, 0);
        Vector2 jumpDir = new Vector2(direction.x, 0);
        float jumpDist = 0.1f;
        jumpcollider = Physics2D.Raycast(jumpTo, jumpDir, jumpDist, layermask);
        Debug.DrawRay(jumpTo, jumpDir * jumpDist, Color.green);


        //Check ground
        RaycastHit2D groundCollider = Physics2D.Raycast(transform.position + new Vector3(0, -size.y - 0.1f, 0) + offset, -transform.up, 0.1f, layermask);
        Debug.DrawRay(transform.position + new Vector3(0, -size.y - 0.1f, 0) + offset, -transform.up * 0.1f, Color.green);
        
        if (groundCollider.collider == null)
        {
            onGround = false;
        }
        else
        {
            onGround = true;
        }

        if (jumpcollider.collider == null)
        {
            //Cant jump
        }

       
       


        if (onGround)
        {
            //  ChangeDirection();
            jumpToNext();
        }



        if (enemyType == Type.Jumping)
        {
            if (timer > randomtime)
            {
                if (jumpcollider.collider != null && onGround)
                    jump();
                timer = 0;
                randomtime = Random.Range(3, 5);
            }
            else
                timer += Time.deltaTime;
        }


    }

    private void CheckWall()
    {
        Vector2 checkwall = transform.position + new Vector3((size.x) * direction.x, -size.y/2, 0);
        Vector2 checkwallAbove = transform.position + new Vector3((size.x) * direction.x, size.y/2, 0);
        Vector2 walldir = new Vector2(direction.x, 0);
        float walldist = size.x;

        RaycastHit2D wall = Physics2D.Raycast(checkwall, walldir, walldist, layermask);
        RaycastHit2D wallAbove = Physics2D.Raycast(checkwallAbove, walldir, walldist, layermask);

        Debug.DrawRay(checkwall, walldir * walldist, Color.green);
        Debug.DrawRay(checkwallAbove, walldir * walldist, Color.green);

        if(wall.collider != null && wall.collider.tag == "ground" && wallAbove.collider == null && onGround)
        {
            jump();
        }


    }

    private void jump()
    {

        if (rb.velocity.y <= 0)
        {
            speed = MaxSpeed;
            rb.AddForce(Vector3.up * JumpHeight, ForceMode2D.Impulse);
            rb.AddForce(direction * 0.6f, ForceMode2D.Impulse);
            onGround = false;
        }
    }

    private void ChangeDirection()
    {
        if (left.collider == null)
        {
            direction.x = 1;
            if (speed >= MaxSpeed)
                speed *= 0.5f;
        }
        else if (right.collider == null)
        {
            direction.x = -1;
            if (speed >= MaxSpeed)
                speed *= 0.5f;
        }
    }

    private void jumpToNext()
    {

        if (left.collider == null && direction.x == -1)
        {
            if (jumpcollider.collider != null)
            {
             //   onGround = false;
                jump();
            }
            else
            {
                ChangeDirection();
            }
        }

        if (right.collider == null && direction.x == 1)
        {
            if (jumpcollider.collider != null)
            {
             //   onGround = false;
                jump();
            }
            else
            {
                ChangeDirection();
            }
        }

        
    }

    private void FixedUpdate()
    {
        Vector3 dir = Vector3.zero;

        if (direction.x != 0)
            dir = direction.x == 1 ? transform.right : -transform.right;
        transform.position += dir * speed * Time.deltaTime;
    }
}
