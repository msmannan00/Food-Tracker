using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class addMealCategoryController : MonoBehaviour
{
    public TMP_Text aName;
    public TMP_Text aDescriptionTop;
    public TMP_Text aDescriptionBottom;
    public Image aImage;

    public GameObject aAddMealButton;
    public GameObject aRemoveMealButton;

    public GameObject loader;
    public TMP_Text aCounter;
    public Image aCountPlus;
    public Image aCountMinus;

    string mTitle;
    DateTime mDate;
    int mDayState;
    ServingInfo mServing;
    double aMealServingCount = 0.5f;

    public void initCategory(string pTitle, MealItem pDish, ServingInfo pServing, string pImagePath, int pDayState, DateTime pDate)
    {
        mServing = pServing;
        mDayState = pDayState;
        mDate = pDate;
        mTitle = pTitle;

        aName.text = pTitle;
        aDescriptionTop.text = pDish.Measure + " Cups";

        try
        {
            if (userSessionManager.Instance.mPlanModel.Meals[mDate]!=null && userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState] != null && userSessionManager.Instance.mPlanModel.Meals[mDate] != null && userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[pTitle] != null)
            {
                aCounter.text = userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[pTitle].ServingAmount.ToString();
                aMealServingCount = userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[pTitle].ServingAmount;
            }
            else
            {
                aCounter.text = "0.5";
                aMealServingCount = 0.5f;
            }
        }
        catch (Exception xe) {
            int e = 0;
            e++;
        }

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


    public void onRemoveMeal()
    {
        aRemoveMealButton.SetActive(false);
        aAddMealButton.SetActive(true);
        userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[mTitle] = null;
        userSessionManager.Instance.SavePlanModel();

        aMealServingCount = 0.5;
        aCounter.text = "0.5";

    }

    public void onAddMeal()
    {
        aRemoveMealButton.SetActive(true);
        aAddMealButton.SetActive(false);
        initMeal();
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
    public void countIncrement()
    {
        if (aMealServingCount < 99.5)
        {
            aMealServingCount = aMealServingCount + 0.5;
            aCounter.text = aMealServingCount.ToString();
        }
        if (aRemoveMealButton.active)
        {
            initMeal();
        }
    }
    public void countDecrement()
    {
        if (aMealServingCount > 0.5)
        {
            aMealServingCount = aMealServingCount - 0.5;
            aCounter.text = aMealServingCount.ToString();
        }
        if (aRemoveMealButton.active)
        {
            initMeal();
        }
    }

}