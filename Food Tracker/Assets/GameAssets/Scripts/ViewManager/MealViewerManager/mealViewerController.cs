using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mealViewerController : MonoBehaviour, PageController
{
    public SubCategory mSubCategory;

    public void onInit(Dictionary<string, object> data)
    {
        mSubCategory = (SubCategory)data["data"];
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
