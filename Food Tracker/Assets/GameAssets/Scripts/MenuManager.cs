using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
  public static MenuManager Instance;
    [SerializeField]
    public menu[] menus;

    public void Awake()
    {

        Instance = this;
    }
  
    public void OpenMenu(string menuName)
    {
        Time.timeScale = 1f;
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                OpenMenu(menus[i]);
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }

    public void OpenMenu(menu menu)
    {
       
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }
    public void CloseMenu(menu menu)
    {
        menu.Close();
    }

    public void Exit_app()
    {
        Application.Quit();
    }
 
    //public void onOpenLogin()
    //{
    //    MenuManager.Instance.OpenMenu("auth");
    //    loginController.instance.onOpenLogin();
    //}
}