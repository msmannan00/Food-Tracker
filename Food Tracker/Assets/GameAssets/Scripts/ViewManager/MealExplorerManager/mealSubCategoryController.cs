using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class mealSubCategoryController : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text aName;
    public TMP_Text aDescription;
    public Image aImage;
    public SubCategory mSubCategory;
    public GameObject loader;

    public void InitCategory(string pTitle, string description, SubCategory pSubCategory, string pImagePath)
    {
        aName.text = pTitle;
        aDescription.text = description;
        mSubCategory = pSubCategory;

        if (pImagePath.StartsWith("http://") || pImagePath.StartsWith("https://"))
        {
            StartCoroutine(LoadImageFromURL(pImagePath));
        }
        else
        {
            LoadImageFromResources(pImagePath);
            loader.SetActive(false);
        }
    }

    private IEnumerator LoadImageFromURL(string imageUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            aImage.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            loader.SetActive(false);
        }
        else
        {
            LoadImageFromResources("UIAssets/mealExplorer/Categories/default");
        }
    }

    private void LoadImageFromResources(string imagePath)
    {
        Sprite sprite = Resources.Load<Sprite>(imagePath);
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
}
