using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mealCategoryController : MonoBehaviour, IPointerClickHandler
{
    [Header("Utilities")]
    public TMP_Text aName;
    public TMP_Text aItemCount;
    public Image aImage;
    public List<SubCategory> mSubCategory;


    public void initCategory(string pTitle, List<SubCategory> pSubCategory, string pImagePath)
    {
        aName.text = pTitle;
        mSubCategory = pSubCategory;
        aItemCount.text = pSubCategory.Count.ToString() + " Items";
        Sprite sprite = Resources.Load<Sprite>(pImagePath);
        if (sprite != null)
        {
            aImage.sprite = sprite;
        }
        else
        {
            sprite = Resources.Load<Sprite>("UIAssets/Dashboard/Categories/default");
            aImage.sprite = sprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalAnimator.Instance.WobbleObject(gameObject);
        openExplorerScreen();
    }

    public void openExplorerScreen()
    {
        Dictionary<string, object> mData = new Dictionary<string, object> { };
        mData["data"] = mSubCategory;
        StateManager.Instance.OpenStaticScreen(gameObject, "mealExplorerScreen", mData, true);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
