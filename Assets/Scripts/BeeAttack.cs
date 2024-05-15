using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed = 7f;
    [SerializeField] private LayerMask hitableGround;
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private GameObject[] bullets;
    private float cooldownTimer = Mathf.Infinity;
    //private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool canAttack;
    private bool canAttackDown;
    private bool canReturnOriginal;
    private bool hit;
    private enum MovementState { idle, attack, attackDown, hit }
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttackDown)
        {
            Vector3 newPosition = transform.position + Vector3.down * speed * Time.deltaTime;
            transform.position = newPosition;
        } else if (canReturnOriginal) {
            if(originalPosition.y >= transform.position.y)
            {
                Vector3 newPosition = transform.position + Vector3.up * speed * Time.deltaTime;

                transform.position = newPosition;
            }
            
        } else if(originalPosition.y <= transform.position.y && gameObject.CompareTag("BeeWithBullet"))
        {
            if (cooldownTimer > attackCooldown)
            {
                Attack();
            }
            cooldownTimer += Time.deltaTime;
        }
        UpdateAnimationState();
    }

    void UpdateAnimationState()
    {
        MovementState state = MovementState.idle;
        if (originalPosition.y > transform.position.y)
        {
            state = MovementState.attackDown;
        }
        else if (originalPosition.y <= transform.position.y)
        {
            state = MovementState.idle;
        }

        if (hit)
        {
            state = MovementState.hit;
        }

        if (canAttack)
        {
            state = MovementState.attack;
        }


        anim.SetInteger("state", (int)state);
    }


    private void Attack()
    {
        canAttack = true;
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = bulletPoint.position;
        bullets[FindBullet()].GetComponent<ProjectileDown>().FallingDown();
    }

    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //th1: bee va cham player
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLife>().TakeDamage(2);
            hit = true;
            canReturnOriginal = false;
            canAttackDown = false;
        }

        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Va cham vao ground");
            canReturnOriginal = true;
            canAttackDown = false;
            hit = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hit = false;
        canAttackDown = false;
        canReturnOriginal = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player vao vung kich hoat");
            canAttackDown = true;
            canReturnOriginal = false;
            canAttack = false;
            hit = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, hitableGround);
    }
}
