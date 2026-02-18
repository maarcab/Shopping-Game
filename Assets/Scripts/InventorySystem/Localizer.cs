using System;
using System.Collections.Generic;
using UnityEngine;

public class Localizer : MonoBehaviour
{
    public TextAsset dataSheet;

    static Dictionary<string, Dictionary<string, string>> localizationData;
    public static string languagePrefered;

    public static event Action OnLanguageChange;
    private void Awake()
    {
        languagePrefered = PlayerPrefs.GetString("language", "English");
        LoadLocalizationData();
    }
    void LoadLocalizationData()
    {
        localizationData = new Dictionary<string, Dictionary<string, string>>();
        string[] lines = dataSheet.text.Split('\n');
        string[] languages = lines[0].Split(';');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] entries = lines[i].Split(';');
            string key = entries[0];

            localizationData[key] = new Dictionary<string, string>();

            for (int j = 1; j < entries.Length; j++)
            {
                localizationData[key][languages[j].Trim()] = entries[j].Trim();
            }
        }
    }
    public static string GetText(string keyWord)
    {
        if (localizationData.TryGetValue(keyWord, out var translations))
        {
            if (translations.TryGetValue(languagePrefered, out string value))
                return value;
        }
        return $"[No Translation for {keyWord}]";
    }
    public static void NewLanguagePrefered(string newLanguage)
    {
        PlayerPrefs.SetString("language", newLanguage);
        languagePrefered = newLanguage;
        if (OnLanguageChange != null)
        {
            OnLanguageChange();
        }
    }
}
