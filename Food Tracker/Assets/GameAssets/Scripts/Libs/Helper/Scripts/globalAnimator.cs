using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAnimator : GenericSingletonClass<GlobalAnimator>
{
    public float mFadeDuration = 0.3f;

    public void FadeIn(GameObject mAppObject)
    {
        CanvasGroup mCanvasGroup = mAppObject.GetComponent<CanvasGroup>();
        if (mCanvasGroup == null)
        {
            mCanvasGroup = mAppObject.AddComponent<CanvasGroup>();
        }

        mAppObject.SetActive(true);
        mCanvasGroup.alpha = 0;
        mCanvasGroup.DOFade(1, mFadeDuration);
    }

    public void FadeOut(GameObject mAppObject)
    {
        CanvasGroup mCanvasGroup = mAppObject.GetComponent<CanvasGroup>();
        if (mCanvasGroup == null)
        {
            mCanvasGroup = mAppObject.AddComponent<CanvasGroup>();
        }

        mCanvasGroup.DOFade(0, mFadeDuration).OnComplete(() =>
        {
            mAppObject.SetActive(false);
        });
    }

    public void FadeInLoader()
    {
        GameObject overlayBlockerInstance = Resources.Load<GameObject>("overlayBlocker");
        if (overlayBlockerInstance != null)
        {
            GameObject instance = UnityEngine.Object.Instantiate(overlayBlockerInstance);
            CanvasGroup canvasGroup = instance.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, 0.5f);
        }
    }

    public void FadeOutLoader()
    {
        GameObject overlayBlockerInstance = Resources.Load<GameObject>("overlayBlocker");
        if (overlayBlockerInstance != null)
        {
            CanvasGroup canvasGroup = overlayBlockerInstance.GetComponent<CanvasGroup>();
            canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
            {
                UnityEngine.Object.Destroy(overlayBlockerInstance);
            });
        }
    }
    public void ApplyParallax(GameObject currentPage, GameObject targetPage, Action callbackSuccess)
    {
        var currentCanvas = currentPage.GetComponent<CanvasGroup>();
        var targetCanvas = targetPage.GetComponent<CanvasGroup>();

        var overlayBlocker = Instantiate(Resources.Load<GameObject>("Prefabs/overlayBlocker"));
        overlayBlocker.transform.SetParent(currentPage.transform, false);
        overlayBlocker.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        overlayBlocker.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        overlayBlocker.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        overlayBlocker.transform.SetAsLastSibling();

        float distanceFactor = 1.5f;
        targetPage.transform.position = new Vector3(Screen.width * distanceFactor, targetPage.transform.position.y, targetPage.transform.position.z);
        targetPage.SetActive(true);
        targetCanvas.alpha = 0.3f;
        targetPage.transform.SetAsLastSibling();

        DOTween.Sequence()
            .OnStart(() =>
            {
                currentCanvas.interactable = false;
                targetCanvas.interactable = false;
            })
            .Append(overlayBlocker.GetComponent<Image>().DOFade(0.7f, 0.4f).SetEase(Ease.Linear))
            .Join(targetPage.transform.DOMoveX(Screen.width / 2f, 0.4f).SetEase(Ease.OutQuad))
            .Join(targetCanvas.DOFade(1f, 0.2f).SetEase(Ease.Linear))
            .OnComplete(() =>
            {
                callbackSuccess?.Invoke();
                currentPage.SetActive(false);
                Destroy(overlayBlocker);
                targetCanvas.interactable = true;
            });
    }
}

