using System;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : GenericSingletonClass<StateManager>
{
    public void OpenStaticScreen(GameObject currentPage, string newPage, string contentTag, Dictionary<string, object> data)
    {
        var prefabPath = "Prefabs/" + newPage;
        var prefabResource = Resources.Load<GameObject>(prefabPath);
        if (prefabResource == null)
        {
            Debug.LogError($"Prefab not found at path: {prefabPath}");
            return;
        }

        var prefab = Instantiate(prefabResource);
        var container = GameObject.FindGameObjectWithTag(contentTag);
        if (container == null)
        {
            Debug.LogError($"Container with tag '{contentTag}' not found.");
            return;
        }

        prefab.transform.SetParent(container.transform, false);
        var welcomeController = prefab.GetComponent<WelcomeController>();
        if (welcomeController == null)
        {
            Debug.LogError("WelcomeController component not found on the prefab.");
            return;
        }

        welcomeController.onInit(data);
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
