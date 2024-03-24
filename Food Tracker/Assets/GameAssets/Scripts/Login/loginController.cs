using UnityEngine;
using TMPro;
using System;
using PlayFab;

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
    [Header("Controller")]
    public GameObject signupInstance;
    public GameObject loginInstance;

    [Header("Managers")]
    public googleManager gmailManager;

    public playfabManager playFabManager;
    [Header("Utilities")]
    public GameObject UIBlocker;
    public GameObject RegisterUI;
    public GameObject loader;
    public GameObject LoginUI;
    public GameObject ForgotUI;
    public GameObject OnSignupGmail;
  


    [Header("Login Screen")]
    public TMP_InputField LoginEmailField;
    public TMP_InputField LoginPasswordField;

    [Header("Register Screen")]
   
    public TMP_InputField RegisterEmailField;
    public TMP_InputField RegisterPasswordwordField;


    public void OnTryLogin()
    {
        Action<string, string> callbackSuccess = (string result1, string result2) =>
        {
            Debug.Log("yes");
            loader.SetActive(false);
        };
        Action<PlayFabError> callbackFailure = (error) =>
        {
            Debug.Log(error);
            loader.SetActive(false);
        };

        loader.SetActive(true);
        playFabManager.OnTryLogin(this.LoginEmailField.text, this.LoginPasswordField.text, callbackSuccess, callbackFailure);
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
        Action callbackSuccess = () =>
        {
            loader.SetActive(false);
        };

        Action<PlayFabError> callbackFailure = (error) =>
        {
            loader.SetActive(false);
        };

        loader.SetActive(true);
        playFabManager.OnTryRegisterNewAccount(this.RegisterEmailField.text, this.RegisterPasswordwordField.text, callbackSuccess, callbackFailure);
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
        Action callbackSuccess = () =>
        {
            loader.SetActive(false);
        };

        Action<PlayFabError> callbackFailure = (error) =>
        {
            loader.SetActive(false);
        };
        loader.SetActive(true);
        playFabManager.InitiatePasswordRecovery(LoginEmailField.text, callbackSuccess, callbackFailure);
    }

  
    public void closeResetPassword()
    {
        ForgotUI.SetActive(false);
        LoginUI.SetActive(true);
    }

    public void OnSignGmail()
    {
        //gmailManager.OnSignGmail();
    }

   
    public void onRegisterUI()
    {
        RegisterUI.SetActive(true);
        LoginUI.SetActive(false);
    }

    public void onOpenSignup()
    {
        signupInstance.SetActive(true);
        loginInstance.SetActive(false);
    }
    public void onOpenLogin()
    {
        loginInstance.SetActive(true);
        signupInstance.SetActive(false);
    }

}
