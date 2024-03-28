using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Import for the Image component

public class WelcomeController : MonoBehaviour
{
    private int mPageNumber = 0;
    public GameObject mPage;
    public GameObject mFood;
    public TMP_Text mTitle;
    public TMP_Text mDescription;

    private string[] mPointerImage = { "pagePointer1", "pagePointer2", "pagePointer3" };
    private string[] mFoodImage = { "welcome1", "welcome2", "welcome3" };
    private string[] mTitleList = { "Mindful Eating", "Keep a food diary", "Traditional Pakistani Recipes" };
    private string[] mDescriptionList = {
        "Practice mindful awareness while eating food",
        "Track your daily meals and snacks using this app",
        "Explore authentic and flavorful meals and dishes"
    };

    public void onInit(Dictionary<string, object> pData)
    {
        if (pData != null)
        {
            mPageNumber = (int)pData[WelcomeKeys.sPageNumber];
            mTitle.text = mTitleList[mPageNumber];
            mDescription.text = mDescriptionList[mPageNumber];

            Sprite pagePointer = Resources.Load<Sprite>("UIAssets/Welcome/Images/" + mPointerImage[mPageNumber]);
            mPage.GetComponent<Image>().sprite = pagePointer;

            Sprite foodImage = Resources.Load<Sprite>("UIAssets/Welcome/Images/" + mPointerImage[mPageNumber]);
            mFood.GetComponent<Image>().sprite = foodImage;
        }
    }

    public void getStarted()
    {
        Dictionary<string, object> mData = new Dictionary<string, object>
        {
            { WelcomeKeys.sPageNumber, mPageNumber + 1 }
        };
        StateManager.Instance.OpenStaticScreen(gameObject, "welcomeScreen", "welcomeScreen", mData);
    }
}
