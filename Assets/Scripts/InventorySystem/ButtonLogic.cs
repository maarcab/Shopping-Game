
using TMPro;
using UnityEngine;
using UnityEngine.UI;
enum ButtonTypes
{
    BuySell, Use, Generate
}
public class ButtonLogic : MonoBehaviour
{
    static InventoryManager invManager;

    static ItemData item;
    static GameObject selectedItem;

    Button button;
    TextMeshProUGUI text;
    bool shopItem;
    [SerializeField] ButtonTypes bType;

    void Awake()
    {
        invManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
        item = null;
        selectedItem = null;
        button = GetComponent<Button>();
        text = transform.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        switch (bType)
        {
            case ButtonTypes.BuySell:
                if (selectedItem != null)
                {
                    button.interactable = true;
                    if (selectedItem.layer == 7)
                    {
                        shopItem = true;
                        GetComponent<LocalizeText>().SetKeyWord("Buy");
                    }
                    else
                    {
                        shopItem = false;
                        GetComponent<LocalizeText>().SetKeyWord("Sell");
                    }
                }
                else
                {
                    button.interactable = false;
                }
                break;
            case ButtonTypes.Use:
                if (selectedItem != null)
                {
                    if (selectedItem.layer == 6 && item.itemType != ItemData.ItemType.Weapon)
                    {
                        button.interactable = true;
                    }
                    else
                    {
                        button.interactable = false;
                    }
                }
                else
                {
                    button.interactable = false;
                }
                break;
            case ButtonTypes.Generate:
                if (invManager.shopInventory.shopInventory.Count >= 35)
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
                break;
        }
    }
    public static void SelectItem(GameObject newItem, ItemData data)
    {
        selectedItem = newItem;
        item = data;
        invManager.MoveSelector(selectedItem);
    }
    public void OnBuySell()
    {
        if (shopItem)
        {
            invManager.BuyItem(item);
        }
        else
        {
            invManager.SellItem(item);
        }
        invManager.MoveSelector(null);
    }
    public void OnUse()
    {
        invManager.UseItem(item);
        invManager.MoveSelector(null);
    }
}

