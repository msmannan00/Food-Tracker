using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddMealController : MonoBehaviour, PageController
{
    public GameObject aScrollViewContent;
    public List<SubCategory> mSubCategory = new List<SubCategory>();
    public TMP_Dropdown sDropdown;
    public TMP_Text aServingText;
    public GridLayoutGroup gridLayoutGroup;

    int mDayState;
    DateTime mDate;


    public void onInit(Dictionary<string, object> data)
    {
        mDayState = (int)data["state"];
        mDate = (DateTime)data["date"];

        sDropdown.onValueChanged.AddListener(delegate { initFoodCategories(); });
        PopulateDropdown();
        initFoodCategories();
        UpdateCellSize();
    }

    public void initFoodCategories()
    {
        foreach (Transform child in aScrollViewContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        int index = 0; 
        Dictionary<string, MealCategory> mCategories = DataManager.Instance.GetCategories();
        foreach (var category in mCategories)
        {
            foreach (var subCategory in category.Value.SubCategories)
            {
                if (subCategory.Title.Equals(sDropdown.options[sDropdown.value].text))
                {
                    aServingText.SetText(subCategory.EachServing.Carb + " carbs, " + subCategory.EachServing.Protein + " proteins, " + subCategory.EachServing.Fat + " fats, " + subCategory.EachServing.KiloCal);
                    foreach (var dish in subCategory.Dishes)
                    {
                        GameObject categoryItem = Instantiate(Resources.Load<GameObject>("Prefabs/addMeal/addMealCategory"));
                        categoryItem.name = "Category_" + index++;
                        categoryItem.transform.SetParent(aScrollViewContent.transform, false);
                        addMealCategoryController categoryController = categoryItem.GetComponent<addMealCategoryController>();
                        string imagePath = dish.Value.ItemSourceImage;
                        categoryController.initCategory(dish.Key, dish.Value, subCategory.EachServing, imagePath, mDayState, mDate);
                    }
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

    void PopulateDropdown()
    {
        sDropdown.options.Clear();
        Dictionary<string, MealCategory> mCategories = DataManager.Instance.GetCategories();
        foreach (var category in mCategories)
        {
            foreach (var subCategory in category.Value.SubCategories)
            {
                if (subCategory.Title.Equals("root"))
                {
                    subCategory.Title = category.Key.ToString();
                }
                mSubCategory.Add(subCategory);
                sDropdown.options.Add(new TMP_Dropdown.OptionData(subCategory.Title));
            }

        }
        sDropdown.value = 0;
        sDropdown.RefreshShownValue();
    }

    public void onGoBack()
    {
        StateManager.Instance.HandleBackAction(gameObject);
    }

    void UpdateCellSize()
    {
        gridLayoutGroup.cellSize = new Vector2(gridLayoutGroup.GetComponent<RectTransform>().rect.width / 2.1f, gridLayoutGroup.cellSize.y);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
