using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum Type
    {
        Jumping,
        NoneJumping,
        Rush
    }

    public int attackStage;
    Vector3 startposition;

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
        startposition = transform.position;

        rb = GetComponent<Rigidbody2D>();
        thisCollider = GetComponent<Collider2D>();
        layermask = LayerMask.GetMask("Default", "ColorGround");


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
            if (LayerMask.LayerToName(other.gameObject.layer) == "ColorGround")
                GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<SpriteRenderer>().color;
        }
    }

    bool paused = false;
    void OnPauseGame()
    {
        paused = true;

    }
    void OnResumeGame()
    {
        paused = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (speed < MaxSpeed)
                speed += Time.deltaTime;


            CheckWall();

            //Check if at end of platform
            right = Physics2D.Raycast(transform.position + new Vector3(size.x, -size.y - 0.1f, 0) + offset, -transform.up, 0.4f, layermask);
            left = Physics2D.Raycast(transform.position + new Vector3(-size.x, -size.y - 0.1f, 0) + offset, -transform.up, 0.4f, layermask);
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
                //  jumpToNext();
            }


            if (enemyType == Type.Rush)
            {

                if (timer > randomtime)
                {

                    if (attackStage == 0)
                    {
                        if (jumpcollider.collider != null && onGround)
                        {
                            rb.AddForce(Vector2.left * MaxJumpWidth, ForceMode2D.Impulse);

                            attackStage++;
                        }
                        else
                            timer = 0;
                    }



                }
                else
                    timer += Time.deltaTime;


                if (attackStage == 1 && Mathf.Abs(rb.velocity.x) < 0.2f)
                {
                    direction.x = 1;
                    attackStage++;
                }
                print(Vector3.Distance(transform.position, startposition));
                if (attackStage == 2 && Vector3.Distance(transform.position, startposition) < 0.5f)
                {
                    direction.x = 0;
                    enemyType = Type.Jumping;

                    timer = 0;
                    randomtime = Random.Range(3, 5);
                }
            }


            if (Random.Range(1, 50) == 2)
            {
                GameObject bullet = Instantiate(Resources.Load("Prefabs/bullet"), transform.position - new Vector3(GetComponent<Collider2D>().bounds.size.x / 2, 0, 0), Quaternion.identity) as GameObject;
                bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.left * (Random.Range(1,10)*0.1f), ForceMode2D.Impulse);
                Destroy(bullet.gameObject, Random.Range(50, 100));
                bullet.tag = "bullet";
            }

            if (enemyType == Type.Jumping)
            {
                if (timer > randomtime)
                {
                 
                    if (Random.Range(1, 5) == 2)
                    {
                        enemyType = Type.Rush;
                        attackStage = 0;
                    }
                    else
                    {
                        if (jumpcollider.collider != null && onGround)
                            jump();
                        timer = 0;
                        randomtime = Random.Range(3, 5);
                    }
                }
                else
                    timer += Time.deltaTime;
            }

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
        else if(wall.collider != null && wall.collider.tag == "ground" && wallAbove.collider != null && wallAbove.collider.tag == "ground" && onGround)
        {
            ChangeDirection();
        }


    }

    private void jump()
    {

        if (rb.velocity.y <= 0)
        {
            speed = MaxSpeed;
            rb.AddForce(Vector3.up * JumpHeight, ForceMode2D.Impulse);
          //  rb.AddForce(direction * 0.6f, ForceMode2D.Impulse);
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
        if (paused)
            return;


        Vector3 dir = Vector3.zero;

        if (direction.x != 0)
            dir = direction.x == 1 ? transform.right : -transform.right;
        transform.position += dir * speed * Time.deltaTime;
    }
}
