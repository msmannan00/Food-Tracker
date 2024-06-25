using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    private void Start()
    {
        #if UNITY_IOS
            PlayfabManager.Instance.OnSaveuser("ios", "ios");
            userSessionManager.Instance.OnInitialize("ios", "ios");
        #endif

        #if UNITY_ANDROID && !UNITY_EDITOR
            Screen.fullScreen = false;
            AndroidUtility.ShowStatusBar(new Color32(9, 126, 57, 255));
        #endif

        Application.targetFrameRate = 60;
        if (!PreferenceManager.Instance.GetBool("WelcomeScreensShown_v3"))
        {
            StateManager.Instance.OpenStaticScreen("welcome", null, "welcomeScreen", null);
        }
        else
        {
            Dictionary<string, object> mData = new Dictionary<string, object>
            {
                { AuthKey.sAuthType, AuthConstant.sAuthTypeLogin}
            };
            StateManager.Instance.OpenStaticScreen("auth", null, "authScreen", mData);
        }
        DataManager.Instance.OnServerInitialized();

    }

}
