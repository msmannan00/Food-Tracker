using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dailyMealCategoryController : MonoBehaviour
{
    public Image aCategoryIcon;
    public TMP_Text aHeader;
    GameObject mParent;
    int mDayState = 0;

    public void initCategory(int state, GameObject pParent)
    {
        string imageName = "";
        mDayState = state;
        mParent = pParent;

        if (state == 0)
        {
            aHeader.text = "Breakfast";
            imageName = "breakfastIcon";
        }
        else if (state == 1)
        {
            aHeader.text = "Lunch";
            imageName = "lunchIcon";
        }
        else if (state == 2)
        {
            aHeader.text = "Dinner";
            imageName = "dinnerIcon";
        }

        LoadImage(imageName);
    }

    public void openMealExplorer()
    {
        Dictionary<string, object> mData = new Dictionary<string, object> { };
        mData["state"] = mDayState;
        StateManager.Instance.OpenStaticScreen("addMeal", mParent, "addMealScreen", mData, true);
    }

    private void LoadImage(string imageName)
    {
        string imagePath = $"UIAssets/PlanDashboard/Images/{imageName}";
        Sprite sprite = Resources.Load<Sprite>(imagePath);
        aCategoryIcon.sprite = sprite;
    }
}
