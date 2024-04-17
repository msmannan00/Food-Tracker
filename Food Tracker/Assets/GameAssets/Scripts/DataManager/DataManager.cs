using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager : GenericSingletonClass<DataManager>
{
    private Dictionary<string, MealCategory> mealData;
    private const string MealDataPrefKey = "MealData";
    private const string MealDataUrl = "https://drive.google.com/uc?export=download&id=1A_AhmdhAXAxRbguW1encplQ6RNJ9GTlS";

    public void OnServerInitialized()
    {
        StartCoroutine(InitializeMealData());
    }

    private IEnumerator InitializeMealData()
    {
        if (PreferenceManager.Instance.GetBool(MealDataPrefKey))
        {
            string jsonText = PreferenceManager.Instance.GetString(MealDataPrefKey);
            if (string.IsNullOrEmpty(jsonText))
            {
                yield return TryLoadMealDataFromWebOrLocal();
            }
            else
            {
                mealData = JsonConvert.DeserializeObject<Dictionary<string, MealCategory>>(jsonText);
            }
        }
        else
        {
            yield return TryLoadMealDataFromWebOrLocal();
        }
    }

    private IEnumerator TryLoadMealDataFromWebOrLocal()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(MealDataUrl))
        {
            yield return webRequest.SendWebRequest();
            if (!webRequest.isNetworkError && !webRequest.isHttpError)
            {
                string jsonText = webRequest.downloadHandler.text;
                SaveMealData(jsonText);
                mealData = JsonConvert.DeserializeObject<Dictionary<string, MealCategory>>(jsonText);
            }
            else
            {
                Debug.Log("Failed to download meal data, loading locally.");
                LoadLocalMealData();
            }
        }
    }

    private void LoadLocalMealData()
    {
        TextAsset jsonData = Resources.Load<TextAsset>("DataManager/mealdata");
        if (jsonData != null)
        {
            string jsonText = jsonData.text;
            SaveMealData(jsonText);
            mealData = JsonConvert.DeserializeObject<Dictionary<string, MealCategory>>(jsonText);
        }
        else
        {
            Debug.LogError("Local meal data file not found.");
        }
    }

    private void SaveMealData(string jsonText)
    {
        PreferenceManager.Instance.SetString(MealDataPrefKey, jsonText);
        PreferenceManager.Instance.SetBool(MealDataPrefKey, true);
        PreferenceManager.Instance.Save();
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
