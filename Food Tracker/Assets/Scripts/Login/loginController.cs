using UnityEngine;
using TMPro;
using PlayFab;
using EasyUI.Popup;

using System.Collections;

public class loginController : MonoBehaviour
{
    public static loginController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [Header("Managers")]
    public GmailManager gmailManager;
    public PlayFabManager playFabManager;
    [Header("Utilities")]
    public GameObject UIBlocker;
    public GameObject RegisterUI;
    public GameObject LoginUI;
    public GameObject ForgotUI;
    public GameObject OnSignupGmail;
  


    [Header("Login Screen")]
    public TMP_InputField LoginEmailField;
    public TMP_InputField LoginPasswordField;

    [Header("Register Screen")]
   
    public TMP_InputField RegisterEmailField;
    public TMP_InputField RegisterPasswordwordField;
    public TMP_InputField ForgotEmailField;

    public void OnTryLogin()
    {
        playFabManager.OnTryLogin();
    }

    public void OnPrivacyPolicy()
    {
        Application.OpenURL("");
    }

    public void OnOpenWebsite()
    {
        Application.OpenURL("");
    }

    public void OnDummyLogin()
    {
   
        Debug.LogError("Login succefull");
}

public void OnTryRegisterNewAccount()
    {
        playFabManager.OnTryRegisterNewAccount();
    }

    public void showForgotUI()
    {
        ForgotUI.SetActive(true);
    }

    public void closeRegister()
    {
        LoginUI.SetActive(true);
        RegisterUI.SetActive(false);
    }

    public void onForgotPassword()
    {
        playFabManager.onForgotPassword();
    }

  
    public void closeResetPassword()
    {
        ForgotUI.SetActive(false);
        LoginUI.SetActive(true);
    }

    public void OnSignGmail()
    {
        gmailManager.OnSignGmail();
    }

   


    public void Loginpanel()
    {
        RegisterUI.SetActive(false);
        LoginUI.SetActive(true);
    } 
    public void Signpanel()
    {
        LoginUI.SetActive(false);
        RegisterUI.SetActive(true);
    }
 

    public void onRegisterUI()
    {
        RegisterUI.SetActive(true);
        LoginUI.SetActive(false);
    }

 

}
