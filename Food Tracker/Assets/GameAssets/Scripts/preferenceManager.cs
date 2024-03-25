using UnityEngine;

public class PreferenceManager
{
    private static PreferenceManager instance;

    private PreferenceManager()
    {
    }

    public static PreferenceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PreferenceManager();
            }
            return instance;
        }
    }

    public void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public bool GetBool(string key, bool defaultValue = false)
    {
        return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
    }

    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public string GetString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(key, defaultValue);
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public void Save()
    {
        PlayerPrefs.Save();
    }
}
