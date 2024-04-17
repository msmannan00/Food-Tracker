using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MealExplorerController : MonoBehaviour, PageController
{
    public List<SubCategory> mSubCategory;
    public GameObject aScrollViewContent;
    public TMP_InputField aSearchBar;

    string mSearchText = "";

    public void onInit(Dictionary<string, object> data)
    {
        mSubCategory = (List<SubCategory>)data["data"];
        aSearchBar.onValueChanged.AddListener(HandleInputChanged);
        initFoodCategories();
    }
    private void HandleInputChanged(string text)
    {
        mSearchText = text;
        initFoodCategories();
    }

    public void initFoodCategories()
    {
        foreach (Transform child in aScrollViewContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        int index = 0;
        foreach (var categoryItem in mSubCategory)
        {
            foreach (var mDishItem in categoryItem.Dishes)
            {
                string imagePath = "UIAssets/mealExplorer/Categories/" + mDishItem.Value.ItemSourceImage;
                string description = categoryItem.EachServing.KiloCal + " / " + mDishItem.Value.Amount;
                if (mDishItem.Key.ToLower().Contains(mSearchText.ToLower()) || description.ToLower().Contains(mSearchText.ToLower()))
                {
                    GameObject dish = Instantiate(Resources.Load<GameObject>("Prefabs/mealSubCategory"));
                    dish.name = "Category_" + index++;
                    dish.transform.SetParent(aScrollViewContent.transform, false);
                    mealSubCategoryController categoryController = dish.GetComponent<mealSubCategoryController>();
                    categoryController.initCategory(mDishItem.Key, description, categoryItem, imagePath);
                }
            }
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject bottomSpace = new GameObject("BottomSpace_" + i);
            LayoutElement layoutElement = bottomSpace.AddComponent<LayoutElement>();
            layoutElement.minHeight = 50f;
            bottomSpace.transform.SetParent(aScrollViewContent.transform, false);
        }
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void onGoBack()
    {
        StateManager.Instance.HandleBackAction(gameObject);
    }

}
