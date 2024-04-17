using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class DataManager : GenericSingletonClass<DataManager>
{
    private Dictionary<string, MealCategory> mealData;
    private const string MealDataPrefKey = "MealData";

    public void OnServerInitialized()
    {
        Debug.Log(":::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
        if (PreferenceManager.Instance.GetBool(MealDataPrefKey))
        {
            string jsonText = PreferenceManager.Instance.GetString(MealDataPrefKey);
            if (jsonText.Equals(""))
            {
                TextAsset jsonData = Resources.Load<TextAsset>("DataManager/mealdata");
                jsonText = jsonData.text;
            }
            mealData = JsonConvert.DeserializeObject<Dictionary<string, MealCategory>>(jsonText);
        }
        else
        {
            TextAsset jsonData = Resources.Load<TextAsset>("DataManager/mealdata");
            string jsonText = jsonData.text;
            mealData = JsonConvert.DeserializeObject<Dictionary<string, MealCategory>>(jsonText);

            PreferenceManager.Instance.SetString(MealDataPrefKey, jsonText);
            PreferenceManager.Instance.SetBool(MealDataPrefKey, true);
            PreferenceManager.Instance.Save();
        }
        Debug.Log(":::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::");
    }

    public Dictionary<string, MealCategory> GetCategories()
    {
        return mealData;
    }

    public List<string> GetSubCategories(string category)
    {
        List<string> subCategories = new List<string>();
        if (mealData.ContainsKey(category))
        {
            var categoryData = mealData[category].SubCategories;
            foreach (var subCategory in categoryData)
            {
                subCategories.Add(subCategory.Title);
            }
        }
        return subCategories;
    }

    public List<MealItem> GetItems(string category, string subCategoryTitle)
    {
        List<MealItem> items = new List<MealItem>();
        if (mealData.ContainsKey(category))
        {
            var categoryData = mealData[category].SubCategories;
            foreach (var subCategory in categoryData)
            {
                if (subCategory.Title == subCategoryTitle)
                {
                    foreach (var item in subCategory.Dishes.Values)
                    {
                        items.Add(item);
                    }
                    break;
                }
            }
        }
        return items;
    }

    public ServingInfo GetEachServing(string category, string subCategoryTitle)
    {
        if (mealData.ContainsKey(category))
        {
            var categoryData = mealData[category].SubCategories;
            foreach (var subCategory in categoryData)
            {
                if (subCategory.Title == subCategoryTitle)
                {
                    return subCategory.EachServing;
                }
            }
        }
        return null;
    }
}
