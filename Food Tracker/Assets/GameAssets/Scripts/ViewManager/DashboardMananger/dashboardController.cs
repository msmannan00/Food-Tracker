using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dashboardController : MonoBehaviour, PageController 
{
    public TMP_Text aUserWelcome;
    public GameObject aScrollViewContent;
    public GridLayoutGroup gridLayoutGroup;

    public void onInit(Dictionary<string, object> data)
    {
        aUserWelcome.text = "Welcome! " + userSessionManager.Instance.mProfileUsername;
        initFoodCategories();
        UpdateCellSize();
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
        Dictionary<string, MealCategory> mCategories = DataManager.Instance.GetCategories();
        int index = 0;
        foreach (var category in mCategories)
        {
            GameObject categoryItem = Instantiate(Resources.Load<GameObject>("Prefabs/mealCategory"));
            categoryItem.name = "Category_" + index++;
            categoryItem.transform.SetParent(aScrollViewContent.transform, false);
            mealCategoryController categoryController = categoryItem.GetComponent<mealCategoryController>();
            string imagePath = "UIAssets/Dashboard/Categories/" + category.Value.ItemSourceImage;
            categoryController.initCategory(category.Value.Title, category.Value.SubCategories, imagePath, gameObject);
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject bottomSpace = new GameObject("BottomSpace_" + i);
            LayoutElement layoutElement = bottomSpace.AddComponent<LayoutElement>();
            layoutElement.minHeight = 50f;
            bottomSpace.transform.SetParent(aScrollViewContent.transform, false);
        }
    }

    void UpdateCellSize()
    {
        gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.GetComponent<RectTransform>().rect.width/2.1f, gridLayoutGroup.cellSize.y);
    }

    void Update()
    {
        
    }
}
