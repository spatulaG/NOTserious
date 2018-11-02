using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Vector3 direction;
    public float MaxSpeed;
    float speed;

    public float jump;

    bool onGround = false;
    Vector3 size;
    float timer;
    int randomtime;

    Rigidbody2D rb;


    RaycastHit2D right;
    RaycastHit2D left;
    RaycastHit2D jumpcollider;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        size = GetComponent<Collider2D>().bounds.size / 2;
        direction.x = -1;
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = true;
            speed *= 0.5f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       

        if (other.tag == "Powerup")
        {
            print("Spelaren krockade med " + other.name);
            Destroy(other.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (speed < MaxSpeed)
            speed += Time.deltaTime;

        right = Physics2D.Raycast(transform.position + new Vector3(size.x, -size.y, 0), -transform.up);
        left = Physics2D.Raycast(transform.position + new Vector3(-size.x, -size.y, 0), -transform.up);

        Vector2 jumpTo = transform.position + new Vector3((size.x + jump*0.6f) * direction.x, -size.y - 0.2f, 0);
        Vector2 jumpDir = new Vector2(direction.x, 0);
        float jumpDist = 0.1f;
        jumpcollider = Physics2D.Raycast(jumpTo,jumpDir, jumpDist);


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
        Debug.DrawRay(transform.position + new Vector3(size.x, -size.y, 0), Vector2.down, Color.green);
        Debug.DrawRay(transform.position + new Vector3(-size.x, -size.y, 0), Vector2.down, Color.green);
        Debug.DrawRay(jumpTo, jumpDir*jumpDist, Color.green);

        

        if (onGround)
        {
            //  ChangeDirection();
            jumpToNext();
        }




        if (timer > randomtime)
        {
            if(jumpcollider.collider != null && onGround)
                rb.AddForce(Vector2.up * jump);
            timer = 0;
            randomtime = Random.Range(3, 5);
        }
        else
            timer += Time.deltaTime;

        
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
                rb.AddForce(Vector2.up * jump);
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
                rb.AddForce(Vector2.up * jump);
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
