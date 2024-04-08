using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Sprite[] images; // Array of images to cycle through
    [SerializeField] private Image imageDisplay;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button playButton;
    private int currentIndex = 0;

    private void Awake()
    {
        nextButton.onClick.AddListener(NextImage);
        previousButton.onClick.AddListener(PreviousImage);
    }
    private void Start()
    {
        DisplayImage();

    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void DisplayImage()
    {
        imageDisplay.sprite = images[currentIndex];
    }

    public void NextImage()
    {
        if(currentIndex < images.Length - 1)
        {
            currentIndex++;
        }
        DisplayImage();
    }

    public void PreviousImage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        DisplayImage();
    }
}
