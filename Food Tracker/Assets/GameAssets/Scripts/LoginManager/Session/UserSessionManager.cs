using System.Collections.Generic;

public class userSessionManager : GenericSingletonClass<userSessionManager>
{
    public string mProfileUsername;
    public string mProfileID;

    public void OnInitialize(string pProfileUsername, string pProfileID)
    {
        this.mProfileUsername = pProfileUsername;
        this.mProfileID = pProfileID;
    }

    public void OnResetSession()
    {
        this.mProfileUsername = null;
        this.mProfileID = null;
    }

    public void createPlan(bool pContinuePlan, string pDateRangeStartText, string pDateRangeEndText)
    {
        PreferenceManager.Instance.SetBool("FirstTimePlanInitialized", true);
        PreferenceManager.Instance.SetBool("ContinuePlan", pContinuePlan);
        PreferenceManager.Instance.SetString("DateRangeStart", pDateRangeStartText);
        PreferenceManager.Instance.SetString("DateRangeEnd", pDateRangeEndText);
    }

}

