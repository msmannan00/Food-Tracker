using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : GenericSingletonClass<StateManager>
{
    private List<GameObject> inactivePages = new List<GameObject>();

    public void OpenStaticScreen(GameObject currentPage, string newPage, Dictionary<string, object> data, bool keepState = false)
    {
        if (!keepState)
        {
            onRemoveBackHistory();
        }
        var prefabPath = "Prefabs/" + newPage;
        var prefabResource = Resources.Load<GameObject>(prefabPath);
        var prefab = Instantiate(prefabResource);
        var container = GameObject.FindGameObjectWithTag(newPage);

        prefab.transform.SetParent(container.transform, false);
        var mController = prefab.GetComponent<PageController>();
        mController.onInit(data);

        if (currentPage != null)
        {
            Action callbackSuccess = () =>
            {
                if (keepState)
                {
                    currentPage.SetActive(false);
                    inactivePages.Add(currentPage);
                }
                else
                {
                    Destroy(currentPage);
                }
            };

            GlobalAnimator.Instance.ApplyParallax(currentPage, prefab, callbackSuccess, keepState);
        }
    }

    public void HandleBackAction(GameObject currentActivePage)
    {
        Destroy(currentActivePage);

        if (inactivePages.Count > 0)
        {
            GameObject lastPage = inactivePages[inactivePages.Count - 1];
            CanvasGroup canvasGroup = lastPage.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = lastPage.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0;
            lastPage.SetActive(true);
            canvasGroup.DOFade(1, 0.3f).SetEase(Ease.InOutQuad);
            inactivePages.RemoveAt(inactivePages.Count - 1);
        }
    }

    public void onRemoveBackHistory()
    {
        foreach (GameObject page in inactivePages)
        {
            Destroy(page);
        }
        inactivePages.Clear();
    }

    public int getInactivePagesCount()
    {
        return inactivePages.Count;
    }
}
