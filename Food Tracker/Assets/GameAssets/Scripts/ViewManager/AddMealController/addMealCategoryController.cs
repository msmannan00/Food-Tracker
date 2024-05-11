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
    Boolean isCounterBlocked = false;

    string mTitle;
    DateTime mDate;
    int mDayState;
    ServingInfo mServing;

    public void initCategory(string pTitle, MealItem pDish, ServingInfo pServing, string pImagePath, int pDayState, DateTime pDate)
    {
        mServing = pServing;
        mDayState = pDayState;
        mDate = pDate;
        mTitle = pTitle;

        aName.text = pTitle;
        aDescriptionTop.text = pServing.KiloCal + " kcals - " + pServing.Carb + "g carbs";
        aDescriptionBottom.text = pServing.Protein + "g protiens - " + pServing.Fat + "g fats";


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
        mMealDetail.ServingAmount = 0.5f;
        userSessionManager.Instance.mPlanModel.Meals[mDate][mDayState].Details[mTitle] = mMealDetail;
        userSessionManager.Instance.SavePlanModel();
    }

}
