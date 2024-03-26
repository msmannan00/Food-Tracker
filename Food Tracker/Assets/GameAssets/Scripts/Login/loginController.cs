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
    public GameObject loader;
    public TMP_Text errorLabelLogin;
    public TMP_Text errorLabelSignup;

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
            GlobalAnimator.Instance.FadeOut(loader);
        };
        Action<PlayFabError> callbackFailure = (error) =>
        {
            GlobalAnimator.Instance.FadeOut(loader);
            GlobalAnimator.Instance.FadeIn(errorLabelLogin.gameObject);
            errorLabelLogin.text = ErrorManager.Instance.translateError(error.Error.ToString());
        };

        GlobalAnimator.Instance.FadeIn(loader);
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

    public void OnTryRegisterNewAccount()
    {
        Action callbackSuccess = () =>
        {
            GlobalAnimator.Instance.FadeOut(loader);
        };

        Action<PlayFabError> callbackFailure = (error) =>
        {
            GlobalAnimator.Instance.FadeOut(loader);
            GlobalAnimator.Instance.FadeIn(errorLabelSignup.gameObject);
            errorLabelSignup.text = ErrorManager.Instance.translateError(error.Error.ToString());
        };

        GlobalAnimator.Instance.FadeIn(loader);
        playFabManager.OnTryRegisterNewAccount(this.RegisterEmailField.text, this.RegisterPasswordwordField.text, callbackSuccess, callbackFailure);
    }

    public void onForgotPassword()
    {
        Action callbackSuccess = () =>
        {
            GlobalAnimator.Instance.FadeOut(loader);
        };

        Action<PlayFabError> callbackFailure = (error) =>
        {
            GlobalAnimator.Instance.FadeOut(loader);
            GlobalAnimator.Instance.FadeIn(errorLabelLogin.gameObject);
            errorLabelLogin.text = ErrorManager.Instance.translateError(error.Error.ToString());
        };
        GlobalAnimator.Instance.FadeIn(loader);
        playFabManager.InitiatePasswordRecovery(LoginEmailField.text, callbackSuccess, callbackFailure);
    }

  
    public void OnSignGmail()
    {
        //gmailManager.OnSignGmail();
    }

    public void resetErrors()
    {
        GlobalAnimator.Instance.FadeOut(errorLabelLogin.gameObject);
        GlobalAnimator.Instance.FadeOut(errorLabelSignup.gameObject);
    }

    public void onOpenSignup()
    {
        resetErrors();
        GlobalAnimator.Instance.ApplyParallax(loginInstance, signupInstance);
    }
    public void onOpenLogin()
    {
        resetErrors();
        GlobalAnimator.Instance.ApplyParallax(signupInstance, loginInstance);
    }

}
