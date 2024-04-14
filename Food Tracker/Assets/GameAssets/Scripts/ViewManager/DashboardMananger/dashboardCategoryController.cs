using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class dashboardCategoryController : MonoBehaviour
{
    [Header("Utilities")]
    public TMP_Text aName;
    public TMP_Text aItemCount;
    public Image aImage;

    public void SetCategory(string title, List<SubCategory> subCategory, string path)
    {
        aName.text = title;
        aItemCount.text = subCategory.Count.ToString() + " Items";
        Sprite sprite = Resources.Load<Sprite>(path);
        if (sprite != null)
        {
            aImage.sprite = sprite;
        }
        else
        {
            sprite = Resources.Load<Sprite>("UIAssets/Dashboard/Categories/category_1");
            aImage.sprite = sprite;
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
