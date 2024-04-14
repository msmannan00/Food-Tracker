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
    public GameObject aBackMonth;
    public GameObject aBackDay;
    public GameObject[] aDateRangeList;
    public GameObject[] aDateRangeListTriggers;
    public GameObject aBackNavigation;

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

            if (date < DateTime.Today)
            {
                aDateRangeListTriggers[i].SetActive(false);
                background.color = new Color(background.color.r, background.color.g, background.color.b, 0.3f);
            }
            else
            {
                aDateRangeListTriggers[i].SetActive(true);
                background.color = new Color(background.color.r, background.color.g, background.color.b, 1f);
            }
        }

        if (mCurrentDate.AddMonths(-1) < DateTime.Today)
        {
            aBackMonth.GetComponent<Image>().raycastTarget = false;
            aBackMonth.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            aBackMonth.GetComponent<Image>().raycastTarget = true;
            aBackMonth.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }

        if (mCurrentDate.AddDays(-1) < DateTime.Today)
        {
            aBackDay.GetComponent<Image>().raycastTarget = false;
            aBackDay.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.3f);
        }
        else
        {
            aBackDay.GetComponent<Image>().raycastTarget = true;
            aBackDay.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
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
        bool isPlanInitialized = (bool)PreferenceManager.Instance.GetBool("FirstTimePlanInitialized", false);
        if (!isPlanInitialized)
        {
            aBackNavigation.SetActive(false);
        }
        mCurrentDate = DateTime.Now;
        onUpdateDates(3);
    }

    public void onStartPlan()
    {
        userSessionManager.Instance.createPlan(mContinuePlan, aDateRangeStart.text, aDateRangeEnd.text);

        Dictionary<string, object> mData = new Dictionary<string, object> { };
        StateManager.Instance.OpenStaticScreen(gameObject, "dashboardScreen", mData);
    }

    void Update()
    {
        
    }

    public void onGoBack()
    {
        StateManager.Instance.HandleBackAction(gameObject);
    }
}
