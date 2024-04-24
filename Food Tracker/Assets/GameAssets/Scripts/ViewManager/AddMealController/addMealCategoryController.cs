using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Playables;
using DG.Tweening;

public class addMealCategoryController : MonoBehaviour
{
    public TMP_Text aName;
    public TMP_Text aDescriptionTop;
    public TMP_Text aDescriptionBottom;
    public Image aImage;
    public TMP_Text aCounter;
    public GameObject aAddMealButton;
    public GameObject aRemoveMealButton;

    double aMealServingCount = 0.5f;
    public GameObject loader;
    Boolean isCounterBlocked = false;

    public Image aCountPlus;
    public Image aCountMinus;
    int mDayState;
    string mTitle;
    DateTime mDate;
    ServingInfo mServing;

    public void initCategory(string pTitle, MealItem pDish, ServingInfo pServing, string pImagePath, int pDayState, DateTime pDate)
    {
        mServing = pServing;
        mDayState = pDayState;
        mDate = pDate;
        mTitle = pTitle;

        aName.text = pTitle;
        aDescriptionTop.text = pServing.KiloCal + " kcals - " + pServing.Carb + "g carbs";
        aDescriptionBottom.text = pServing.KiloCal + "g protiens - " + pServing.Carb + "g fats";


        if (pImagePath.StartsWith("http://") || pImagePath.StartsWith("https://"))
        {
            StartCoroutine(HelperMethods.Instance.LoadImageFromURL(pImagePath, aImage, loader));
        }
        else
        {
            HelperMethods.Instance.LoadImageFromResources("UIAssets/mealExplorer/Categories/" + pImagePath, aImage);
            loader.SetActive(false);
        }
        initMealDetail();
        userSessionManager.Instance.mPlanModel.initKey(mDate, mDayState, mTitle);
    }

    void initMealDetail()
    {
        try
        {
            blockCounter();
            var meals = userSessionManager.Instance.mPlanModel.Meals;
            if (meals != null && meals.ContainsKey(mDate))
            {
                var dayMeals = meals[mDate];
                if (dayMeals != null && dayMeals.ContainsKey(mDayState))
                {
                    var mealDetails = dayMeals[mDayState].Details;
                    if (mealDetails != null && mealDetails.ContainsKey(mTitle))
                    {
                        MealDetail mMealDetail = mealDetails[mTitle];
                        if (mMealDetail != null)
                        {
                            aAddMealButton.SetActive(false);
                            aRemoveMealButton.SetActive(true);
                            aMealServingCount = mMealDetail.ServingAmount;
                            aCounter.text = aMealServingCount.ToString();
                            counterButtonStatus();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("An error occurred: " + ex.Message);
        }
    }

    public void countIncrement()
    {
        if (!isCounterBlocked)
        {
            if (aMealServingCount < 99)
            {
                aMealServingCount = aMealServingCount + 0.5;
                aCounter.text = aMealServingCount.ToString();
            }
            initMeal();
            counterButtonStatus();
        }
    }
    public void countDecrement()
    {
        if (!isCounterBlocked)
        {
            if (aMealServingCount > 1)
            {
                aMealServingCount = aMealServingCount - 0.5;
                aCounter.text = aMealServingCount.ToString();
            }
            initMeal();
            counterButtonStatus();
        }
    }

    public void onRemoveMeal()
    {
        aRemoveMealButton.SetActive(false);
        aAddMealButton.SetActive(true);
        userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[mTitle] = null;
        userSessionManager.Instance.SavePlanModel();
        blockCounter();
    }

    public void onAddMeal()
    {
        aRemoveMealButton.SetActive(true);
        aAddMealButton.SetActive(false);
        initMeal();
        counterButtonStatus();
    }

    void initMeal()
    {
        if (userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[mTitle] == null)
        {
            userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[mTitle] = new MealDetail();
        }
        MealDetail mMealDetail = userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[mTitle];
        mMealDetail.Fats = mServing.Fat;
        mMealDetail.Kcals = mServing.KiloCal;
        mMealDetail.Carbs = mServing.Carb;
        mMealDetail.Proteins = mServing.Protein;
        mMealDetail.ServingAmount = aMealServingCount;
        userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[mTitle] = mMealDetail;
        userSessionManager.Instance.SavePlanModel();
    }

    void blockCounter()
    {
        Color currentOutlineColor;
        currentOutlineColor = aCountPlus.GetComponent<Outline>().effectColor;
        currentOutlineColor.a = 0.2f;
        aCountPlus.GetComponent<Outline>().DOColor(currentOutlineColor, 0.2f);
        aCountPlus.GetComponent<Image>().raycastTarget = false;

        currentOutlineColor = aCountMinus.GetComponent<Outline>().effectColor;
        currentOutlineColor.a = 0.2f;
        aCountMinus.GetComponent<Outline>().DOColor(currentOutlineColor, 0.2f);
        aCountMinus.GetComponent<Image>().raycastTarget = false;
        isCounterBlocked = true;
        aCounter.alpha = 0.5f;
    }

    void counterButtonStatus()
    {
        isCounterBlocked = false;
        Color currentOutlineColor;
        if (aMealServingCount >= 99)
        {
            aCountPlus.GetComponent<Image>().raycastTarget = false;
            currentOutlineColor = aCountPlus.GetComponent<Outline>().effectColor;
            currentOutlineColor.a = 0.2f;
            aCountPlus.GetComponent<Outline>().DOColor(currentOutlineColor, 0.1f);
        }
        else
        {
            aCountPlus.GetComponent<Image>().raycastTarget = true;
            currentOutlineColor = aCountPlus.GetComponent<Outline>().effectColor;
            currentOutlineColor.a = 1f;
            aCountPlus.GetComponent<Outline>().DOColor(currentOutlineColor, 0.1f);
        }

        if (aMealServingCount <= 1)
        {
            aCountMinus.GetComponent<Image>().raycastTarget = false;
            currentOutlineColor = aCountMinus.GetComponent<Outline>().effectColor;
            currentOutlineColor.a = 0.2f;
            aCountMinus.GetComponent<Outline>().DOColor(currentOutlineColor, 0.1f);
        }
        else
        {
            aCountMinus.GetComponent<Image>().raycastTarget = true;
            currentOutlineColor = aCountMinus.GetComponent<Outline>().effectColor;
            currentOutlineColor.a = 1f;
            aCountMinus.GetComponent<Outline>().DOColor(currentOutlineColor, 0.1f);
        }
        aCounter.alpha = 1f;

    }

}
