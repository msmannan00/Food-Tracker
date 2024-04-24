using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class userSessionManager : GenericSingletonClass<userSessionManager>
{
    public string mProfileUsername;
    public string mProfileID;
    public userStatsModel mUserStatsModel;
    public PlanModel mPlanModel = new PlanModel();

    public void OnInitialize(string pProfileUsername, string pProfileID)
    {
        LoadPlanModel();
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

        string mStartingDate = PreferenceManager.Instance.GetString("DateRangeStart", DateTime.Now.ToString());
        string mEndingDate = PreferenceManager.Instance.GetString("DateRangeEnd", DateTime.Now.ToString());

        userSessionManager.Instance.mUserStatsModel.OnInitialize(pContinueWeeklyPlan: pContinuePlan, pStartingDate: HelperMethods.Instance.ParseDateString(mStartingDate), pEndingDate: HelperMethods.Instance.ParseDateString(mEndingDate));
    }

    public void SavePlanModel()
    {
        string json = JsonConvert.SerializeObject(mPlanModel, Formatting.Indented);
        PlayerPrefs.SetString("PlanModel", json);
        PlayerPrefs.Save();
    }

    void LoadPlanModel()
    {
        string json = PlayerPrefs.GetString("PlanModel");
        if (!string.IsNullOrEmpty(json))
        {
            mPlanModel = JsonConvert.DeserializeObject<PlanModel>(json);
        }
    }

    public void RemovePlanModel()
    {
        if (PlayerPrefs.HasKey("PlanModel"))
        {
            PlayerPrefs.DeleteKey("PlanModel");
            PlayerPrefs.Save();
            mPlanModel = new PlanModel();
        }
    }
}

