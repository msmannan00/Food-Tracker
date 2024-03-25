using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GlobalAnimator
{
    private static GlobalAnimator instance;
    public float fadeDuration = 0.3f;

    private GlobalAnimator() {
    }

    public static GlobalAnimator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GlobalAnimator();
            }
            return instance;
        }
    }


    public void FadeIn(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        obj.SetActive(true);
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, fadeDuration);
    }

    public void FadeOut(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        canvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
        {
            obj.SetActive(false);
        });
    }

    public void ApplyParallax(GameObject obj1, GameObject obj2)
    {
        CanvasGroup canvasGroup1 = obj1.GetComponent<CanvasGroup>();
        CanvasGroup canvasGroup2 = obj2.GetComponent<CanvasGroup>();

        if (canvasGroup1 == null || canvasGroup2 == null)
        {
            Debug.LogError("Both objects must have CanvasGroup components.");
            return;
        }

        GameObject overlayBlocker = GameObject.Instantiate(Resources.Load<GameObject>("overlayBlocker"));
        overlayBlocker.transform.SetParent(obj1.transform, false);
        overlayBlocker.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        overlayBlocker.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        overlayBlocker.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        overlayBlocker.transform.SetAsLastSibling();

        float distanceFactor = 1.5f;
        obj2.transform.position = new Vector3(Screen.width * distanceFactor, obj2.transform.position.y, obj2.transform.position.z);
        obj2.SetActive(true);
        canvasGroup2.alpha = 0.3f;
        obj2.transform.SetAsLastSibling();

        DOTween.Sequence()
            .OnStart(() =>
            {
                canvasGroup1.interactable = false;
                canvasGroup2.interactable = false;
            })
            .Append(overlayBlocker.GetComponent<Image>().DOFade(0.7f, 0.4f).SetEase(Ease.Linear))
            .Join(obj2.transform.DOMoveX(Screen.width / 2f, 0.4f).SetEase(Ease.OutQuad))
            .Join(canvasGroup2.DOFade(1f, 0.2f).SetEase(Ease.Linear))
            .OnComplete(() =>
            {
                obj1.SetActive(false);
                GameObject.Destroy(overlayBlocker);
                canvasGroup2.interactable = true;
            });
    }
}
