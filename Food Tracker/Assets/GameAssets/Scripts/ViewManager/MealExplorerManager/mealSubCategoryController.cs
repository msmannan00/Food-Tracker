using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class mealSubCategoryController : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text aName;
    public TMP_Text aDescription;
    public Image aImage;
    public SubCategory mSubCategory;

    public void initCategory(string pTitle, string description, SubCategory pSubCategory, string pImagePath)
    {
        aName.text = pTitle;
        aDescription.text = description;
        mSubCategory = pSubCategory;
        Sprite sprite = Resources.Load<Sprite>(pImagePath);
        if (sprite != null)
        {
            aImage.sprite = sprite;
        }
        else
        {
            sprite = Resources.Load<Sprite>("UIAssets/mealExplorer/Categories/default");
            aImage.sprite = sprite;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GlobalAnimator.Instance.WobbleObject(gameObject);
    }

    void Start()
    {
    }

    void Update()
    {
        
    }
}
