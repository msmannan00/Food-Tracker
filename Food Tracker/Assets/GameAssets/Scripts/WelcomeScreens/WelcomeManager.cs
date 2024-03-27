using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeManager : MonoBehaviour
{
    // Array to hold references to all welcome screens
    public GameObject[] welcomeScreens;

    // Index of the currently active welcome screen
    private int currentScreenIndex = 0;

    private void Start()
    {
        // Check if the welcome screens have been shown before
        if (!PlayerPrefs.HasKey("WelcomeScreensShown"))
        {
            // Welcome screens have not been shown before, so show the first one
            ShowWelcomeScreen(currentScreenIndex);

            // Set the flag to indicate that the welcome screens have been shown
            PlayerPrefs.SetInt("WelcomeScreensShown", 1);
        }
        else
        {
            // Welcome screens have been shown before, so disable all of them
            HideAllWelcomeScreens();
        }
    }

    // Method to show a specific welcome screen by index
    private void ShowWelcomeScreen(int index)
    {
        if (index >= 0 && index < welcomeScreens.Length)
        {
           
            welcomeScreens[index].SetActive(true);
        }
    }

    private void HideAllWelcomeScreens()
    {
        foreach (GameObject welcomeScreen in welcomeScreens)
        {
            welcomeScreen.SetActive(false);
        }
        MenuManager.Instance.OpenMenu("auth");
    }

    public void OnContinueButtonClick()
    {
        welcomeScreens[currentScreenIndex].SetActive(false);
        currentScreenIndex++;
        if (currentScreenIndex < welcomeScreens.Length)
        {
            ShowWelcomeScreen(currentScreenIndex);
        }
        else
        {
            MenuManager.Instance.OpenMenu("auth");
        }
    }
    public void onOpenLogin()
    {
        GlobalAnimator.Instance.ApplyParallax(MenuManager.Instance.menus[0].gameObject, MenuManager.Instance.menus[1].gameObject);
    }
}