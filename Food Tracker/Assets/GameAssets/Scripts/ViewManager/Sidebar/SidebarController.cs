using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SidebarController : MonoBehaviour, PageController
{
    public TMP_Text aUsername;

    public void onInit(Dictionary<string, object> data)
    {
        aUsername.SetText(userSessionManager.Instance.name);
    }

    public void onGoBack()
    {
        StateManager.Instance.HandleSidebarBackAction(gameObject);
    }
    public void onLogout()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void onRemoveAccount()
    {
        Action callbackSuccess = () =>
        {
            string recipient = "delete.account@genesistechnologies.org";
            string subject = "Request to Delete My Account";
            string body = "Hello,\n\nI would like to request the deletion of my account with the ID: "+ userSessionManager.Instance.mProfileID +". Please process this request and confirm.\n\nThank you.";
            HelperMethods.Instance.CreateAndOpenEmail(recipient, subject, body);
        };

        GlobalAnimator.Instance.FadeOutLoader();
        GameObject alertPrefab = Resources.Load<GameObject>("Prefabs/alerts/alertFailure");
        GameObject alertsContainer = GameObject.FindGameObjectWithTag("alerts");
        GameObject instantiatedAlert = Instantiate(alertPrefab, alertsContainer.transform);
        AlertController alertController = instantiatedAlert.GetComponent<AlertController>();
        alertController.InitController("Are you sure you want to delete this account", pCallbackSuccess: callbackSuccess, pTrigger: "Delete Account", pHeader:"Delete Account");
        GlobalAnimator.Instance.AnimateAlpha(instantiatedAlert, true);
    }
    public void onPrivacyPolicy()
    {
        Application.OpenURL("https://www.termsfeed.com/live/fd617121-60c1-4047-9f9c-63268dd05bc2");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StateManager.Instance.HandleBackAction(gameObject);
        }
    }

}
