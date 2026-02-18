using TMPro;
using UnityEngine;

public class LocalizeText : MonoBehaviour
{
    [SerializeField] string textKey; //KeyWord a traducir
    private TMP_Text textComponent;

    void Start()
    {
        textComponent = GetTextComponent();
        UpdateText();
        Localizer.OnLanguageChange += UpdateText;
    }

    private void OnDestroy()
    {
        Localizer.OnLanguageChange -= UpdateText;
    }

    void UpdateText()
    {
        if (textComponent != null && !string.IsNullOrEmpty(textKey))
        {
            textComponent.text = Localizer.GetText(textKey);
        }
    }
    public void SetKeyWord(string key)
    {
        textKey = key;
        UpdateText();
    }

    TMP_Text GetTextComponent()
    {
        if (GetComponent<TMP_Text>() != null)
        {
            return GetComponent<TMP_Text>();
        }
        else
        {
            if (transform.GetChild(0) != null)
            {
                return transform.GetChild(0).GetComponent<TMP_Text>();
            }
            else
            {
                Debug.LogWarning("[No text component found]");
                return null;
            }
        }
    }
}