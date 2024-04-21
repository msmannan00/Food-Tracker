using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyMealController : MonoBehaviour, PageController
{
    public TMP_Text aMonthYearDate;
    public GameObject aBackMonth;
    public GameObject aBackDay;
    public GameObject aNextMonth;
    public GameObject aNextDay;
    public GameObject[] aDateRangeList;
    public GameObject[] aDateRangeListTriggers;
    public GridLayoutGroup gridLayoutGroup;
    public TMP_Text aCurrentDate;
    public TMP_Text aCurrentDay;
    DateTime mCurrentDate;
    int mSelectedRangeIndex = 3;

    public GameObject aContent;


    public void onInit(Dictionary<string, object> data)
    {
    }


    void Start()
    {
        bool isPlanInitialized = (bool)PreferenceManager.Instance.GetBool("FirstTimePlanInitialized", false);
        mCurrentDate = DateTime.Now;
        onUpdateDates(3);
        UpdateCellSize();
        initDailyPlanSection();
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
        GlobalAnimator.Instance.WobbleObject(aDateRangeList[pIndex]);
        onUpdateDates(pIndex);
        DateTime newStartDate = mCurrentDate.AddDays(mSelectedRangeIndex - 3);
        aCurrentDay.text = newStartDate.ToString("ddd");
        aCurrentDate.text = newStartDate.ToString("MMM dd, yyyy"); ;
    }

    private void onUpdateDates(int pSelectedIndex)
    {
        mSelectedRangeIndex = pSelectedIndex;
        aMonthYearDate.text = mCurrentDate.ToString("MMMM yyyy");

        aCurrentDay.text = mCurrentDate.ToString("ddd");
        aCurrentDate.text = mCurrentDate.ToString("MMM dd, yyyy");

        DateTime startDate = userSessionManager.Instance.mUserStatsModel.sStartingDate;
        DateTime? endDate = userSessionManager.Instance.mUserStatsModel.sContinueWeeklyPlan ? (DateTime?)null : userSessionManager.Instance.mUserStatsModel.sEndingDate;

        for (int i = 0; i < aDateRangeList.Length; i++)
        {
            int offset = i - 3;
            DateTime date = mCurrentDate.AddDays(offset);

            TMP_Text dayText = aDateRangeList[i].transform.GetChild(1).GetComponent<TMP_Text>();
            TMP_Text dateText = aDateRangeList[i].transform.GetChild(0).GetComponent<TMP_Text>();
            Image background = aDateRangeList[i].GetComponent<Image>();

            dayText.text = date.ToString("ddd");
            dateText.text = date.Day.ToString();

            bool isSelectable = date >= startDate && (endDate == null || date <= endDate.Value);
            bool isTodayOrLater = date >= DateTime.Today;

            if (i == mSelectedRangeIndex && isSelectable)
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

            if (!isSelectable || !isTodayOrLater)
            {
                aDateRangeListTriggers[i].GetComponent<Image>().raycastTarget = false;
                background.color = new Color(background.color.r, background.color.g, background.color.b, 0.3f);
            }
            else
            {
                aDateRangeListTriggers[i].GetComponent<Image>().raycastTarget = true;
                background.color = new Color(background.color.r, background.color.g, background.color.b, 1f);
            }
        }

        UpdateNavigationButton(aBackMonth, mCurrentDate.AddMonths(-1), startDate, endDate, true);
        UpdateNavigationButton(aBackDay, mCurrentDate.AddDays(-1), startDate, endDate, true);
        UpdateNavigationButton(aNextMonth, mCurrentDate.AddMonths(1), startDate, endDate, false);
        UpdateNavigationButton(aNextDay, mCurrentDate.AddDays(1), startDate, endDate, false);
    }


    private void UpdateNavigationButton(GameObject button, DateTime dateToCheck, DateTime startDate, DateTime? endDate, bool isStart)
    {
        bool isInRange;
        if (isStart)
        {
            isInRange = dateToCheck >= startDate;
        }
        else
        {
            isInRange = endDate == null || dateToCheck <= endDate.Value;
        }

        button.GetComponent<Image>().raycastTarget = isInRange;
        button.GetComponent<Image>().color = isInRange ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0.3f);
    }

    void initDailyPlanSection()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/dailyMeal/dailyMealCategory");
        for (int i = 0; i < 3; i++)
        {
            GameObject instance = Instantiate(prefab, aContent.transform);
            instance.name = "dailyPlannerCategoryInstance" + (i + 1);
            DailyMealCategoryController categoryController = instance.GetComponent<DailyMealCategoryController>();
            categoryController.initCategory(i, gameObject);
        }
    }
    void UpdateCellSize()
    {
        float panelWidth = GetComponent<RectTransform>().rect.width;
        float spacing = gridLayoutGroup.spacing.x * (6 - 1) * 1.5f;
        float padding = gridLayoutGroup.padding.left + gridLayoutGroup.padding.right;
        float size = (panelWidth - spacing - padding);
        gridLayoutGroup.cellSize = new Vector2(size / 8, size / 5f);
    }

    public void onGoBack()
    {
        StateManager.Instance.HandleBackAction(gameObject);
    }

    void Update()
    {
        
    }
}
