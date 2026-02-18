using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    TMP_Dropdown menu;
    Image button;

    private void Start()
    {
        menu = GetComponent<TMP_Dropdown>();
        menu.value = LanguageToInt(Localizer.languagePrefered);
        button = GetComponent<Image>();
        if (button != null)
        {
            
        }
        else
        {
            Debug.Log("Error");
        }
    }
    public void OnValueChanged()
    {
        Localizer.NewLanguagePrefered(LanguageToString(menu.value));
        Debug.Log(menu.value);
    }
    string LanguageToString(int value)
    {
        switch (value)
        {
            case 1:
                return "Spanish";
            case 2:
                return "Catalan";
            default:
                return "English";
        }
    }
    int LanguageToInt(string value)
    {
        switch (value)
        {
            case "Spanish":
                return 1;
            case "Catalan":
                return 2;
            default:
                return 0;
        }
    }
}