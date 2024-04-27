using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class StartMenu : MonoBehaviour
{
    [SerializeField] private Sprite[] images; // Array of images to cycle through
    [SerializeField] private Image imageDisplay;
    private int currentIndex = 0;
    private string inputName = "";

    public void StartGame()
    {
        PlayerPrefs.SetInt("PlayerCharacter", currentIndex);
        PlayerPrefs.SetString("PlayerName", inputName);
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

    public void OnInputTextChanged(string text)
    {
        inputName = text;
    }
}
