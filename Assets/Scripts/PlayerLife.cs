using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    public int maxHealth = 10;
    public int currentHealth;
    public HealthBar healthBar;
    private UIManager uiManager;
    [SerializeField] private AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        uiManager = FindObjectOfType<UIManager>();
        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth > 0)
        {
            anim.SetTrigger("hit");
        } else
        {
            Die();
        }
    }

    public void HealBlood(int blood)
    {
        currentHealth += blood;
        healthBar.SetHealth(currentHealth);
    }


    private void Die()
    {
        currentHealth = maxHealth;
        SoundManager.instance.PlaySound(deathSound);
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        uiManager.GameOver();
    }
}
