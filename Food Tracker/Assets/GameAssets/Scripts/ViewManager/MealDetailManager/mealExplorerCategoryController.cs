using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class mealExplorerCategoryController : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text aName;
    public TMP_Text aDescription;
    public Image aImage;
    public GameObject loader;
    public GameObject mParent;

    string mTitle;
    ServingInfo mEachServing;
    MealItem mDishes;

    public void InitCategory(string pTitle, string description, MealItem pDish, ServingInfo pServing, string pImagePath, GameObject pParent)
    {
        aName.text = pTitle;
        aDescription.text = description;
        mTitle = pTitle;
        mEachServing = pServing;
        mDishes = pDish;
        mParent = pParent;

        if (pImagePath.StartsWith("http://") || pImagePath.StartsWith("https://"))
        {
            StartCoroutine(HelperMethods.Instance.LoadImageFromURL(pImagePath, aImage, loader));
        }
        else
        {
            HelperMethods.Instance.LoadImageFromResources("UIAssets/mealExplorer/Categories/" + pImagePath, aImage);
            loader.SetActive(false);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalAnimator.Instance.WobbleObject(gameObject);
        Dictionary<string, object> mData = new Dictionary<string, object> { };
        mData["title"] = mTitle;
        mData["eachServing"] = mEachServing;
        mData["dish"] = mDishes;
        StateManager.Instance.OpenStaticScreen("mealDetailS", mParent, "mealDetailScreen", mData, true);
    }
}
