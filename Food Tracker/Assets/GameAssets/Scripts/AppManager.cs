using UnityEngine;

public class AppManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        if (!PlayerPrefs.HasKey("WelcomeScreensShown_v3"))
        {
            StateManager.Instance.OpenStaticScreen(null, "welcomeScreen", "welcomeScreen", null);
        }
        else
        {
        }

    }
}
