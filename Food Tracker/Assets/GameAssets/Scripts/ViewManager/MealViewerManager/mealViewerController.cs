using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mealViewerController : MonoBehaviour, PageController
{
    string mTitle;
    ServingInfo mEachServing;
    MealItem mDish;

    public Image aHeaderImage;
    public GameObject aPieChart;
    public GameObject aImageLoader;
    public TMP_Text aTitle;
    public TMP_Text aQuantityValue;
    public TMP_Text aServingSizeValue;
    public TMP_Text aCarbValue;
    public TMP_Text aCarbPercentage;
    public TMP_Text aFatValue;
    public TMP_Text aFatPercentage;
    public TMP_Text aProteinValue;
    public TMP_Text aProteinPercentage;

    public TMP_Text aKiloCalories;
    public TMP_Text aFatPercentageFacts;
    public TMP_Text aProteinsCalories;


    public void onInit(Dictionary<string, object> data)
    {
        mTitle = (string)data["title"];
        mEachServing = (ServingInfo)data["eachServing"];
        mDish = (MealItem)data["dish"];
    }

    void Start()
    {
        string pImagePath = mDish.ItemSourceImage;
        if (pImagePath.StartsWith("http://") || pImagePath.StartsWith("https://"))
        {
            StartCoroutine(HelperMethods.Instance.LoadImageFromURL(pImagePath, aHeaderImage, aImageLoader));
        }
        else
        {
            HelperMethods.Instance.LoadImageFromResources("UIAssets/mealExplorer/Categories/" + pImagePath, aHeaderImage);
            aImageLoader.SetActive(false);
        }

        double total = mEachServing.Carb + mEachServing.Fat + mEachServing.Protein;
        double carbsPercentage = (mEachServing.Carb / total) * 100;
        double fatsPercentage = (mEachServing.Fat / total) * 100;
        double proteinsPercentage = (mEachServing.Protein / total) * 100;

        aTitle.text = mTitle;
        aQuantityValue.text = mDish.Amount.ToString() + "g";
        aServingSizeValue.text = mDish.Measure + " cup";
        aCarbValue.text = mEachServing.Carb.ToString() + "g";
        aFatValue.text = mEachServing.Fat.ToString() + "g";
        aProteinValue.text = mEachServing.Protein.ToString() + "g";
        aCarbPercentage.text = carbsPercentage.ToString() + "%";
        aFatPercentage.text = fatsPercentage.ToString() + "%";
        aProteinPercentage.text = proteinsPercentage.ToString() + "%";
        aKiloCalories.text = mEachServing.KiloCal.ToString();

        aFatPercentageFacts.text = fatsPercentage.ToString() + "%";
        aProteinsCalories.text = proteinsPercentage.ToString() + "%";


    }

    public void onGoBack()
    {
        StateManager.Instance.HandleBackAction(gameObject);
    }

    void Update()
    {
        
    }
}
