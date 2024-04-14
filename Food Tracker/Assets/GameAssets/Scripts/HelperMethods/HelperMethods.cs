using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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