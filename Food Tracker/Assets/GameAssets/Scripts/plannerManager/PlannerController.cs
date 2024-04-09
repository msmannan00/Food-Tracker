using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

public class PlannerController : MonoBehaviour, PageController
{
    DateTime mCurrentDate;
    int mSelectedRangeIndex = 3;
    bool mContinuePlan = false;

    public TMP_Text aMonthYearDate;
    public TMP_Text aDateRangeStart;
    public TMP_Text aDateRangeEnd;
    public Image aContinuePlan;
    public GameObject[] aDateRangeList;

    public void onInit(Dictionary<string, object> data)
    {
    }

    public void onNextDate()
    {
        mCurrentDate = mCurrentDate.AddDays(1);
        onUpdateDates(3);
    }

    public void onPreviousDate()
    {
        mCurrentDate = mCurrentDate.AddDays(-1);
        onUpdateDates(3);
    }

    public void onNextMonth()
    {
        mCurrentDate = mCurrentDate.AddMonths(1);
        onUpdateDates(3);
    }

    public void onPreviousMonth()
    {
        mCurrentDate = mCurrentDate.AddMonths(-1);
        onUpdateDates(3);
    }

    public void onUpdateSelectedIndex(int pIndex)
    {
        onUpdateDates(pIndex);
        DateTime newStartDate = mCurrentDate.AddDays(mSelectedRangeIndex - 3);
        aDateRangeStart.text = newStartDate.ToString("MMM dd, yyyy");
        aDateRangeEnd.text = newStartDate.AddDays(7).ToString("MMM dd, yyyy");
    }

    private void onUpdateDates(int pSelectedIndex)
    {
        mSelectedRangeIndex = pSelectedIndex;
        aMonthYearDate.text = mCurrentDate.ToString("MMMM yyyy");
        aDateRangeStart.text = mCurrentDate.ToString("MMM dd, yyyy");
        aDateRangeEnd.text = mCurrentDate.AddDays(7).ToString("MMM dd, yyyy");

        for (int i = 0; i < aDateRangeList.Length; i++)
        {
            int offset = i - 3;
            DateTime date = mCurrentDate.AddDays(offset);

            TMP_Text dayText = aDateRangeList[i].transform.GetChild(1).GetComponent<TMP_Text>();
            TMP_Text dateText = aDateRangeList[i].transform.GetChild(0).GetComponent<TMP_Text>();
            Image background = aDateRangeList[i].GetComponent<Image>();

            dayText.text = date.ToString("ddd");
            dateText.text = date.Day.ToString();

            if (i == mSelectedRangeIndex)
            {
                background.color = new Color32(0x09, 0x7E, 0x39, 0xFF);
                dayText.color = Color.white;
                dateText.color = Color.white;
            }
            else
            {
                background.color = Color.white;
                dayText.color = new Color32(0x8C, 0x8C, 0x8C, 0xFF);
                dateText.color = new Color32(0x32, 0x31, 0x36, 0xFF);
            }
        }
    }

    public void onToggleContinuePlan()
    {
        if (mContinuePlan)
        {
            aContinuePlan.color = Color.white;
        }
        else
        {
            aContinuePlan.color = new Color32(0x09, 0x7E, 0x39, 0xFF);
        }
        mContinuePlan = !mContinuePlan;
    }

    void Start()
    {
        bool mFirsTimePlanInitialized = PreferenceManager.Instance.GetBool("FirstTimePlanInitialized", false);
        if (!mFirsTimePlanInitialized)
        {
            mCurrentDate = DateTime.Now;
            onUpdateDates(3);
        }
        else
        {
            Dictionary<string, object> mData = new Dictionary<string, object> { };
            StateManager.Instance.OpenStaticScreen(gameObject, "dashboardScreen", mData);
        }

    }

    public void onStartPlan()
    {
        PreferenceManager.Instance.SetBool("FirstTimePlanInitialized", true);
        PreferenceManager.Instance.SetBool("ContinuePlan", mContinuePlan);
        PreferenceManager.Instance.SetString("DateRangeStart", aDateRangeStart.text);
        PreferenceManager.Instance.SetString("DateRangeEnd", aDateRangeEnd.text);


        Dictionary<string, object> mData = new Dictionary<string, object>{};
        StateManager.Instance.OpenStaticScreen(gameObject, "dashboardScreen", mData);
    }

    void Update()
    {
        
    }
}
