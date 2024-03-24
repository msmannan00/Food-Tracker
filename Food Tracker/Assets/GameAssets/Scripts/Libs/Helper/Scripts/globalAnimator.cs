using UnityEngine;

public class GlobalAnimator
{
    private static GlobalAnimator instance;
    public float fadeDuration = 0.3f;

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

        LeanTween.alphaCanvas(canvasGroup, 1, fadeDuration).setDelay(0.14f);

        float startY = obj.transform.localPosition.y - 150;
        obj.transform.localPosition = new Vector3(obj.transform.localPosition.x, startY, obj.transform.localPosition.z);
        LeanTween.moveLocalY(obj, obj.transform.localPosition.y + 150, fadeDuration);
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

        LeanTween.moveLocalY(obj, obj.transform.localPosition.y - 150, fadeDuration);
    }


}
