using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using System;
using System.Collections.Generic;
using EasyUI.Popup;

#if !UNITY_IOS
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#else
#endif

#if UNITY_IOS
using UnityEngine.iOS;
#else
#endif

public class playfabManager : GenericSingletonClass<playfabManager>
{

    public void saveuser(string username, string password)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.Save();
    }



    public void OnServerInitialized()
    {
	#if !UNITY_IOS
	        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        	.AddOauthScope("profile")
        	.RequestServerAuthCode(false)
	        .Build();

            PlayGamesPlatform.InitializeInstance(config);
        	PlayGamesPlatform.DebugLogEnabled = false;
	        PlayGamesPlatform.Activate();

#else
#endif
    }

    public void OnTryLogin(string email, string password, Action<string, string> callbackSuccess, Action<PlayFabError> callbackFailure)
    {
        LoginWithEmailAddressRequest req = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(req,
        res =>
        {
            saveuser(email, password);
            callbackSuccess(email, res.PlayFabId);
        },
        err =>
        {
            callbackFailure(err);
        });
    }

    public void OnLogout(string username, string playfabID, Action callbackSuccess, Action<PlayFabError> callbackFailure)
    {
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
    }

    public void OnLogoutForced()
    {
        PlayFabClientAPI.ForgetAllCredentials();

        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("password");
    }

#if !UNITY_IOS
    public void OnSignGmail(Action callbackSuccess, Action<PlayFabError> callbackFailure, Action<string, string> callbackSuccessPlayfab, Action<PlayFabError> callbackFailurePlayfab)
    {

        Social.localUser.Authenticate((bool success) => {
            if (success)
            {
                var serverAuthCode = PlayGamesPlatform.Instance.GetServerAuthCode();
                PlayFabClientAPI.LoginWithGoogleAccount(new LoginWithGoogleAccountRequest()
                {
                    TitleId = "BF51B",
                    ServerAuthCode = serverAuthCode,
                    CreateAccount = true
                },
                res =>
                {
                    saveuser("raza@gmail.com", "123456789");
                    callbackSuccess();
                },
                err =>
                {
                    Popup.Show("playfab login ", "LoggedIn", "Dismiss");

                   OnTryLogin(PlayerPrefs.GetString("username"),
                PlayerPrefs.GetString("password"), callbackSuccessPlayfab, callbackFailurePlayfab);
                    callbackSuccess();
                });
            }
            else
            {
                Popup.Show("Gmail not Connected", "check your email address", "Dismiss");
                callbackFailure(null);
            }
        });
    }
#else
#endif


    #if UNITY_IOS
            public void OnSignIOS(Action callbackSuccess, Action<PlayFabError> callbackFailure, Action<string, string> callbackSuccessPlayfab, Action<PlayFabError> callbackFailurePlayfab)
        {
            Device.RequestStoreReview();
            if (Device.systemVersion.StartsWith("10"))
            {
                NativeAPI.Authorize((success) =>
                {
                    if (success)
                    {
                        OnTryLogin("player@gmail.com", "killprg1", callbackSuccessPlayfab, callbackFailurePlayfab);
                        callbackSuccess();
                    }
                    else
                    {
                        callbackFailure(null);
                    }
                });
            }
            else
            {
                callbackFailure(null);
            }
        }

        public static class NativeAPI
        {
            public delegate void SignInCallback(bool success);

            public static void Authorize(SignInCallback callback)
            {
                bool success = true; // Replace with your sign-in logic
                callback?.Invoke(success);
            }
        }
    #else
    #endif

    public void OnTryRegisterNewAccount(string email, string password, Action callbackSuccess, Action<PlayFabError> callbackFailure)
    {
        RegisterPlayFabUserRequest req = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(req,
        res =>
        {
            saveuser(email, password);
            callbackSuccess();
        },
        err =>
        {
            callbackFailure(err);
        });
    }

    public void InitiatePasswordRecovery(string email, Action callbackSuccess, Action<PlayFabError> callbackFailure)
    {
        SendAccountRecoveryEmailRequest request = new SendAccountRecoveryEmailRequest
        {
            Email = email,
            TitleId = "BF51B"
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request,
        result =>
        {
            Debug.Log("Password reset email sent successfully.");
            callbackSuccess();
        },
        error =>
        {
            Debug.LogError("Password reset failed: " + error.ErrorMessage);
            callbackFailure(error);
        });
    }

}
