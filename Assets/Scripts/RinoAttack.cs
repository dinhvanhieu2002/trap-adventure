using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoAttack : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 7f;
    [SerializeField] private LayerMask hitableGround;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool canAttack;
    private bool hit;
    private bool isLeft;

    private float dirX = 0f;

    private enum MovementState { idle, running, hit }

    private MovementState state;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack)
        {
            if(isLeft)
            {
                dirX = Mathf.Sign(-transform.localScale.x) * speed;
            } else
            {
                dirX = Mathf.Sign(transform.localScale.x) * speed;
            }
            rb.velocity = new Vector2(dirX, rb.velocity.y);
        }
        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else
        {
            state = MovementState.idle;
        }

        if (hit)
        {
            state = MovementState.hit;
        }

      
        anim.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //th1: rino va cham player
        if (collision.gameObject.CompareTag("Player"))
        {
            hit = true;
        }
       
        dirX = 0f;
        canAttack = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hit = false;
        dirX = 0f;
        canAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //neu player vao vung kich hoat, rino translate x infinity theo huong cua nguoi choi
            canAttack = true;
            hit = false;
            
            if(player.transform.position.x < transform.position.x)
            {
                isLeft = true;
            } else
            {
                isLeft = false;
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, hitableGround);
    }
}
