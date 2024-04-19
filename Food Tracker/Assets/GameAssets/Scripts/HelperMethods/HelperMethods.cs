using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelperMethods : GenericSingletonClass<HelperMethods>
{
    public void ShuffleList<T>(List<T> pList)
    {
        System.Random mRandom = new System.Random();

        for (int i = 0; i < pList.Count; i++)
        {
            int mRandomIndex = mRandom.Next(i, pList.Count);
            T temp = pList[i];
            pList[i] = pList[mRandomIndex];
            pList[mRandomIndex] = temp;
        }
    }

    public Color GetColorFromString(string pColorName)
    {
        switch (pColorName)
        {
            case "red":
                return Color.red;
            case "green":
                return Color.green;
            case "blue":
                return Color.blue;
            case "white":
                return Color.white;
            default:
                return Color.magenta;
        }
    }

    public void RestartScene(string pScene)
    {
        SceneManager.LoadScene(pScene);
    }

    public List<List<string>> GeneratePermutations(List<string> pColors, int pLength, int pCount)
    {
        List<List<string>> mPermutations = new List<List<string>>();

        void GeneratePermutationsRecursive(List<string> mCurrentPermutation)
        {
            if (mCurrentPermutation.Count == pLength)
            {
                if (!AreAllColorsSame(mCurrentPermutation))
                {
                    mPermutations.Add(new List<string>(mCurrentPermutation));
                }
                return;
            }

            foreach (string mColor in pColors)
            {
                if (mCurrentPermutation.Count > 0 && mCurrentPermutation[mCurrentPermutation.Count - 1] == mColor)
                {
                    continue;
                }

                mCurrentPermutation.Add(mColor);
                GeneratePermutationsRecursive(mCurrentPermutation);
                mCurrentPermutation.RemoveAt(mCurrentPermutation.Count - 1);
            }
        }

        GeneratePermutationsRecursive(new List<string>());
        ShuffleList(mPermutations);

        if (mPermutations.Count > pCount)
        {
            return mPermutations.GetRange(0, pCount);
        }
        else
        {
            return mPermutations;
        }
    }

    private bool AreAllColorsSame(List<string> pColors)
    {
        string mFirstColor = pColors[0];
        for (int mCounter = 1; mCounter < pColors.Count; mCounter++)
        {
            if (pColors[mCounter] != mFirstColor)
            {
                return false;
            }
        }
        return true;
    }

    public DateTime ParseDateString(string dateString)
    {
        if (DateTime.TryParse(dateString, out DateTime date))
        {
            return date;
        }
        return DateTime.Now;
    }

    public IEnumerator LoadImageFromURL(string imageUrl, Image aImage, GameObject loader)
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
            this.LoadImageFromResources("UIAssets/mealExplorer/Categories/default", aImage);
        }
    }

    public void LoadImageFromResources(string imagePath, Image aImage)
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

    public string ExtractUsernameFromEmail(string email)
    {
        int atIndex = email.IndexOf('@');
        if (atIndex >= 0)
        {
            return email.Substring(0, atIndex);
        }
        return null;
    }
}