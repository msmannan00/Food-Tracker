using TMPro;
using UnityEngine;

public class DailyMealCategoryItemController : MonoBehaviour
{
    public TMP_Text mTitle;
    public TMP_Text mDescription;
    
    void Start()
    {
        
    }

    public void initCategory(string pTitle, MealDetail pDetail)
    {
        mTitle.text = pTitle;
        mDescription.text = (pDetail.Kcals * pDetail.ServingAmount).ToString() + " kcals | " + (pDetail.Carbs * pDetail.ServingAmount).ToString() + "g carbs | " + (pDetail.Proteins * pDetail.ServingAmount).ToString() + "g protiens | " + (pDetail.Fats * pDetail.ServingAmount).ToString() + "g fats";
    }

    void Update()
    {
        
    }
}
