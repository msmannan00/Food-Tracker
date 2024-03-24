using UnityEngine;

public class GlobalAnimator
{
    private static GlobalAnimator instance;
    public float fadeDuration = 0.2f;

    private GlobalAnimator() { }

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
        LeanTween.alphaCanvas(canvasGroup, 1, fadeDuration);
    }

    public void FadeOut(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        LeanTween.alphaCanvas(canvasGroup, 0, fadeDuration).setOnComplete(() =>
        {
            obj.SetActive(false);
        });
    }
    public void FadeInTranslate(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0;
        obj.SetActive(true);

        float startX = obj.transform.localPosition.x - 150; // Start from left side
        obj.transform.localPosition = new Vector3(startX, obj.transform.localPosition.y, obj.transform.localPosition.z);

        LeanTween.alphaCanvas(canvasGroup, 1, fadeDuration)
            .setDelay(0.14f)
            .setEase(LeanTweenType.linear)
            .setOnComplete(() =>
            {
                canvasGroup.alpha = 1;
                obj.transform.localPosition = new Vector3(startX + 150, obj.transform.localPosition.y, obj.transform.localPosition.z);
            });

        LeanTween.moveLocalX(obj, startX + 150, fadeDuration)
            .setEase(LeanTweenType.easeOutQuad); // Use easeOutQuad for smoother acceleration and deceleration
    }


    public void FadeOutTranslate(GameObject obj)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        LeanTween.alphaCanvas(canvasGroup, 0, fadeDuration).setDelay(0.14f).setOnComplete(() =>
        {
            obj.SetActive(false);
        });

        LeanTween.moveLocalX(obj, obj.transform.localPosition.x - 150, fadeDuration); // Move towards left
    }


}
