using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioClip collectionSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HealingFruit")) 
        {
            //collectionSoundEffect.Play();
            SoundManager.instance.PlaySound(collectionSound);
            Destroy(collision.gameObject);
            transform.gameObject.GetComponent<PlayerLife>().HealBlood(1);
        }
    }
}
