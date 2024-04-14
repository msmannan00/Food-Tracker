using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dashboardController : MonoBehaviour, PageController 
{
    public TMP_Text aUserWelcome;
    public GameObject aScrollViewContent;

    public void onInit(Dictionary<string, object> data)
    {
        aUserWelcome.text = "Welcome! " + userSessionManager.Instance.mProfileUsername;
        initFoodCategories();
    }

    public void updatePlanDetail()
    {
        Dictionary<string, object> mData = new Dictionary<string, object> { };
        StateManager.Instance.OpenStaticScreen(gameObject, "plannerScreen", mData, true);
    }

    public void viewPlanDetail()
    {
        Dictionary<string, object> mData = new Dictionary<string, object> { };
        StateManager.Instance.OpenStaticScreen(gameObject, "planDashboardScreen", mData, true);
    }

    public void initFoodCategories()
    {
        Dictionary<string, List<SubCategory>> mCategories = DataManager.Instance.GetCategories();
        int index = 0;
        foreach (var category in mCategories)
        {
            GameObject categoryItem = Instantiate(Resources.Load<GameObject>("Prefabs/foodCategory"));
            categoryItem.name = "Category_" + index++;
            categoryItem.transform.SetParent(aScrollViewContent.transform, false);
            dashboardCategoryController categoryController = categoryItem.GetComponent<dashboardCategoryController>();
            string imagePath = "UIAssets/Dashboard/Categories/category_" + index;
            categoryController.SetCategory(category.Key, category.Value, imagePath);
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject bottomSpace = new GameObject("BottomSpace_" + i);
            LayoutElement layoutElement = bottomSpace.AddComponent<LayoutElement>();
            layoutElement.minHeight = 50f;
            bottomSpace.transform.SetParent(aScrollViewContent.transform, false);
        }
    }

    void Update()
    {
        
    }
}
