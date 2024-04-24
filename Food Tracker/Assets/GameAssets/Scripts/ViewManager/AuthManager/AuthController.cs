using UnityEngine;
using TMPro;
using System;
using PlayFab;
using System.Collections.Generic;
using PlayFab.Internal;
using UnityEngine.UI;

public class AuthController : MonoBehaviour, PageController
{
    [Header("Managers")]
    public GoogleManager aGmailManager;
    public PlayfabManager aPlayFabManager;

    [Header("Utilities")]
    public TMP_Text aError;
    public TMP_Text aPageToggleText1;
    public TMP_Text aPageToggleText2;

    [Header("Auth Fields")]
    public TMP_InputField aUsername;
    public TMP_InputField aPassword;
    public TextMeshProUGUI aTriggerButton;

    private string mAuthType;


    public void onInit(Dictionary<string, object> pData)
    {
        this.mAuthType = (string)pData.GetValueOrDefault(AuthKey.sAuthType, AuthConstant.sAuthTypeSignup);
        if (this.mAuthType == AuthConstant.sAuthTypeLogin)
        {
            aTriggerButton.text = "Log In";
            aPageToggleText1.text = "I Don’t have an account? ";
            aPageToggleText2.text = "Signup";
        }
        else if (this.mAuthType == AuthConstant.sAuthTypeSignup)
        {
            aTriggerButton.text = "Sign Up";
            aPageToggleText1.text = "Already have an acount?";
            aPageToggleText2.text = "Log In";
        }

    }

    public void OnPrivacyPolicy()
    {
        Application.OpenURL("");
    }

    public void OnOpenWebsite()
    {
        Application.OpenURL("");
    }

    void Start()
    {
        this.aUsername.text = "msmannan00@gmail.com";
        this.aPassword.text = "killprg1";
        //OnTrigger();
    }

    public void OnTrigger()
    {
        if (this.mAuthType == AuthConstant.sAuthTypeLogin)
        {
            Action<string, string> mCallbackSuccess = (string pResult1, string pResult2) =>
            {
                GlobalAnimator.Instance.FadeOutLoader();
                userSessionManager.Instance.OnInitialize(pResult1, pResult2);
                onSignIn();
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
        else if (this.mAuthType == AuthConstant.sAuthTypeSignup)
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
    }

    public void OnForgotPassword()
    {
        Action callbackSuccess = () =>
        {
            Action callbackSuccess = () =>
            {
                Application.OpenURL("mailto:");
            };

            GlobalAnimator.Instance.FadeOutLoader();
            GameObject alertPrefab = Resources.Load<GameObject>("Prefabs/alerts/alertSuccess");
            GameObject alertsContainer = GameObject.FindGameObjectWithTag("alerts");
            GameObject instantiatedAlert = Instantiate(alertPrefab, alertsContainer.transform);
            AlertController alertController = instantiatedAlert.GetComponent<AlertController>();
            alertController.InitController("Reset password instructions have been sent to your email address", pCallbackSuccess: callbackSuccess, pTrigger : "Open Mail");
            GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
        };

        Action<PlayFabError> callbackFailure = (pError) =>
        {
            GlobalAnimator.Instance.FadeOutLoader();
            GlobalAnimator.Instance.FadeIn(aError.gameObject);
            aError.text = ErrorManager.Instance.getTranslateError(pError.Error.ToString());

            GameObject alertPrefab = Resources.Load<GameObject>("Prefabs/alerts/alertFailure");
            GameObject alertsContainer = GameObject.FindGameObjectWithTag("alerts");
            GameObject instantiatedAlert = Instantiate(alertPrefab, alertsContainer.transform);
            AlertController alertController = instantiatedAlert.GetComponent<AlertController>();
            alertController.InitController("Internal error occured, please try again later", pTrigger: "Continue", pHeader: "Server Error");
            GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
        };
        GlobalAnimator.Instance.FadeInLoader();
        aPlayFabManager.InitiatePasswordRecovery(aUsername.text, callbackSuccess, callbackFailure);
    }

    public void onSignIn()
    {
        bool mFirsTimePlanInitialized = PreferenceManager.Instance.GetBool("FirstTimePlanInitialized", false);
        if (!mFirsTimePlanInitialized)
        {
            Dictionary<string, object> mData = new Dictionary<string, object>
            {
                { AuthKey.sAuthType, AuthConstant.sAuthTypeSignup}
            };

            StateManager.Instance.OpenStaticScreen("planCreator", gameObject, "planCreatorScreen", mData);
        }
        else
        {
            Dictionary<string, object> mData = new Dictionary<string, object> { };
            StateManager.Instance.OpenStaticScreen("dashboard", gameObject, "dashboardScreen", mData);
        }

    }

    public void OnSignGmail()
    {
    }

    public void OnResetErrors()
        {
            GlobalAnimator.Instance.FadeOut(aUsername.gameObject);
            GlobalAnimator.Instance.FadeOut(aPassword.gameObject);
        }

    public void OnToogleAuth()
    {
        if(this.mAuthType == AuthConstant.sAuthTypeLogin)
        {
            Dictionary<string, object> mData = new Dictionary<string, object>
            {
                { AuthKey.sAuthType, AuthConstant.sAuthTypeSignup}
            };
            StateManager.Instance.OpenStaticScreen("auth", gameObject, "authScreen", mData);
        }
        else if(this.mAuthType == AuthConstant.sAuthTypeSignup)
        {
            OnResetErrors();
            Dictionary<string, object> mData = new Dictionary<string, object>
            {
                { AuthKey.sAuthType, AuthConstant.sAuthTypeLogin}
            };
            StateManager.Instance.OpenStaticScreen("auth", gameObject, "authScreen", mData);
        }
    }

}
