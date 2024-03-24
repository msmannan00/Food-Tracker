using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Menu[] menus;
    public void Awake()
    {
        Instance = this;
        #region welcomeScreenChecking
        if(PlayerPrefs.GetInt("welcome")== 0)
        {
            OpenMenu("welcomescreen");
        }else if(PlayerPrefs.GetInt("welcome") == 1) {

            OpenMenu("auth");
        }

        #endregion
    }
    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                OpenMenu(menus[i]);
            }else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    } 
    
    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    } public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

   
}
