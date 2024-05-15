using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDown : MonoBehaviour
{
    [SerializeField] private float speed;
    private BoxCollider2D boxCollider;
    private bool hit;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        
        //float movementSpeed = -speed * Time.deltaTime;
        //transform.Translate(0, movementSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerLife>().TakeDamage(1);
        }
        hit = true;
        boxCollider.enabled = false;
        Deactive();
    }

    public void FallingDown()
    {
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}
