using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class welcomescreen : MonoBehaviour
{
    public GameObject[] welcomeScreen;
   
    public void OpenNextScreen(int a)
    {
        for (int i = 0; i < welcomeScreen.Length; i++)
        {
            welcomeScreen[i].SetActive(false);
        }
        welcomeScreen[a + 1].SetActive(true);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt("welcome", 1);
    }
}
