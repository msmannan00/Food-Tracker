using UnityEngine;
using TMPro;
using System;
using PlayFab;

public class AuthController : MonoBehaviour
{
    [Header("Managers")]
    public GoogleManager aGmailManager;
    public PlayfabManager aPlayFabManager;

    [Header("Utilities")]
    public TMP_Text aError;

    [Header("Auth Fields")]
    public TMP_InputField aUsername;
    public TMP_InputField aPassword;

    public void OnTryLogin()
    {
        Action<string, string> mCallbackSuccess = (string pResult1, string pResult2) =>
        {
            GlobalAnimator.Instance.FadeOutLoader();
        };
        Action<PlayFabError> callbackFailure = (pError) =>
        {
            GlobalAnimator.Instance.FadeOutLoader();
            GlobalAnimator.Instance.FadeIn(aError.gameObject);
            aError.text = ErrorManager.Instance.getTranslateError(pError.Error.ToString());
        };

        GlobalAnimator.Instance.FadeInLoader();
        aPlayFabManager.OnTryLogin(this.aUsername.text, this.aPassword.text, mCallbackSuccess, callbackFailure);
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
            GlobalAnimator.Instance.FadeOutLoader();
        };

        Action<PlayFabError> callbackFailure = (pError) =>
        {
            GlobalAnimator.Instance.FadeOutLoader();
            GlobalAnimator.Instance.FadeIn(aError.gameObject);
            aError.text = ErrorManager.Instance.getTranslateError(pError.Error.ToString());
        };

        GlobalAnimator.Instance.FadeInLoader();
        aPlayFabManager.OnTryRegisterNewAccount(this.aUsername.text, this.aPassword.text, callbackSuccess, callbackFailure);
    }

    public void OnForgotPassword()
    {
        Action callbackSuccess = () =>
        {
            GlobalAnimator.Instance.FadeOutLoader();
        };

        Action<PlayFabError> callbackFailure = (pError) =>
        {
            GlobalAnimator.Instance.FadeOutLoader();
            GlobalAnimator.Instance.FadeIn(aError.gameObject);
            aError.text = ErrorManager.Instance.getTranslateError(pError.Error.ToString());
        };
        GlobalAnimator.Instance.FadeInLoader();
        aPlayFabManager.InitiatePasswordRecovery(aUsername.text, callbackSuccess, callbackFailure);
    }

  
    public void OnSignGmail()
    {
    }

    public void OnResetErrors()
        {
            GlobalAnimator.Instance.FadeOut(aUsername.gameObject);
            GlobalAnimator.Instance.FadeOut(aPassword.gameObject);
        }

        public void OnOpenSignup()
        {
            OnResetErrors();
            // GlobalAnimator.Instance.ApplyParallax(loginInstance, signupInstance);
        }
        public void OnOpenLogin()
        {
            OnResetErrors();
            // GlobalAnimator.Instance.ApplyParallax(signupInstance, loginInstance);
        }

}
