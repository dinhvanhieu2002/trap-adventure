using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CollectPoint : MonoBehaviour
{
    [SerializeField] private float pointAward;
    [SerializeField] private AudioClip collectionPointSound;
    [SerializeField] private Text pointText;

    private float currentPoint;
    private void Awake()
    {
        currentPoint = PlayerPrefs.GetFloat("point", 0);
        pointText.text = " x " + currentPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            currentPoint += pointAward;
            PlayerPrefs.SetFloat("point", currentPoint);
            SoundManager.instance.PlaySound(collectionPointSound);
            Destroy(collision.gameObject);
            pointText.text = " x " + currentPoint;
        }
    }
}
