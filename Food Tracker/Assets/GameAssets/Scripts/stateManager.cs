using System;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : GenericSingletonClass<StateManager>
{
    public void OpenStaticScreen(GameObject currentPage, string newPage, Dictionary<string, object> data)
    {
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
                Destroy(currentPage);
            };

            GlobalAnimator.Instance.ApplyParallax(currentPage, prefab, callbackSuccess);
        }
    }
}
