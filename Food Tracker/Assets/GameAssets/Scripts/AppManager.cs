using UnityEngine;

public class AppManager : MonoBehaviour
{
    private void Start()
    {
        /*Initiating app pages*/
        Application.targetFrameRate = 60;
        if (!PlayerPrefs.HasKey("WelcomeScreensShown_v3"))
        {
            StateManager.Instance.OpenStaticScreen(null, "welcomeScreen", null);
        }

        /*Initiating loading of meal data*/
        MealManager.Instance.OnServerInitialized();
    }
}
