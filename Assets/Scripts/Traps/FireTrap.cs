using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private int damage;

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; // when the trap gets triggered
    private bool active;    // when the trap is active and can hurt the player

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                //trigger the firetrap
                StartCoroutine(ActivateFireTrap());
            }
            if (active)
            {
                Debug.Log("tru mau");
                collision.GetComponent<PlayerLife>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        // turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red; 

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white; //turn the sprite black to its initial color
        active = true;
        anim.SetBool("Activated", true);

        //Wait until X seconds, deactivate trap and reset all variables and animatior
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("Activated", false);
    }

}
