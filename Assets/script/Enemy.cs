using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject Blob;
    List<GameObject> bloblist = new List<GameObject>();

    Vector3 direction;
    public float MaxSpeed;

    float speed;

    public float MaxJumpWidth;
    public float JumpHeight = 4;

    bool onGround = false;
    Vector3 size;
    Vector3 offset;
    float timer;
    int randomtime;

    Rigidbody2D rb;


    RaycastHit2D right;
    RaycastHit2D left;
    RaycastHit2D jumpcollider;

    Collider2D collider;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();


       
            size = collider.bounds.size / 2;
            offset = collider.offset;
        
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
    private void OnTriggerEnter(Collider other)
    {
    
    }
    // Update is called once per frame
    void Update()
    {
        if (speed < MaxSpeed)
            speed += Time.deltaTime;


        CheckWall();

        right = Physics2D.Raycast(transform.position + new Vector3(size.x, -size.y - 0.1f, 0) + offset, -transform.up * 0.4f);
        left = Physics2D.Raycast(transform.position + new Vector3(-size.x, -size.y - 0.1f, 0) + offset, -transform.up * 0.4f);

        Vector2 jumpTo = transform.position + new Vector3((size.x + MaxJumpWidth * 0.6f) * direction.x, -size.y - 0.2f, 0);
        Vector2 jumpDir = new Vector2(direction.x, 0);
        float jumpDist = 0.1f;
        jumpcollider = Physics2D.Raycast(jumpTo, jumpDir, jumpDist);


        RaycastHit2D groundCollider = Physics2D.Raycast(transform.position, -transform.up, 0.1f);

        if (groundCollider.collider == null)
        {
            onGround = true;
        }

        if (jumpcollider.collider == null)
        {
            //Cant jump
        }

        //Draw debug Rays
        Debug.DrawRay(transform.position + new Vector3(size.x, -size.y - 0.1f, 0) + offset, Vector2.down * 0.4f, Color.green);
        Debug.DrawRay(transform.position + new Vector3(-size.x, -size.y - 0.1f, 0) + offset, Vector2.down * 0.4f, Color.green);
        Debug.DrawRay(jumpTo, jumpDir * jumpDist, Color.green);



        if (onGround)
        {
            //  ChangeDirection();
            jumpToNext();
        }




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

    private void CheckWall()
    {
        Vector2 checkwall = transform.position + new Vector3((size.x) * direction.x, -size.y/2, 0);
        Vector2 checkwallAbove = transform.position + new Vector3((size.x) * direction.x, size.y/2, 0);
        Vector2 walldir = new Vector2(direction.x, 0);
        float walldist = size.x;

        RaycastHit2D wall = Physics2D.Raycast(checkwall, walldir, walldist);
        RaycastHit2D wallAbove = Physics2D.Raycast(checkwallAbove, walldir, walldist);

        Debug.DrawRay(checkwall, walldir * walldist, Color.green);
        Debug.DrawRay(checkwallAbove, walldir * walldist, Color.green);

        if(wall.collider != null && wall.collider.tag == "ground" && wallAbove.collider == null)
        {
            jump();
        }


    }

    private void jump()
    {
        speed = MaxSpeed;
        rb.AddForce(Vector3.up * JumpHeight, ForceMode2D.Impulse);
        rb.AddForce(direction*0.6f, ForceMode2D.Impulse);
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
                onGround = false;
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
                onGround = false;
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
        Vector3 dir = direction.x == 1 ? transform.right : -transform.right;
        transform.position += dir * speed * Time.deltaTime;
    }
}
