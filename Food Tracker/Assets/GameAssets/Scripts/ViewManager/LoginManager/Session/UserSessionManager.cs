using System;
using System.Collections.Generic;

public class userSessionManager : GenericSingletonClass<userSessionManager>
{
    public string mProfileUsername;
    public string mProfileID;
    public userStatsModel mUserStatsModel;

    public void OnInitialize(string pProfileUsername, string pProfileID)
    {
        this.mProfileUsername = pProfileUsername;
        this.mProfileID = pProfileID;
        this.mUserStatsModel = new userStatsModel();

        bool mContinueWeeklyPlan = PreferenceManager.Instance.GetBool("ContinuePlan", false);
        string mStartingDate = PreferenceManager.Instance.GetString("DateRangeStart", DateTime.Now.ToString());
        string mEndingDate = PreferenceManager.Instance.GetString("DateRangeEnd", DateTime.Now.ToString());
        this.mUserStatsModel.OnInitialize(pContinueWeeklyPlan: mContinueWeeklyPlan, pStartingDate: HelperMethods.Instance.ParseDateString(mStartingDate), pEndingDate: HelperMethods.Instance.ParseDateString(mEndingDate));

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
