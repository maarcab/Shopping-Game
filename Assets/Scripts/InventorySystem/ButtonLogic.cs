
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

    Button button;
    TextMeshProUGUI text;
    bool shopItem;
    [SerializeField] ButtonTypes bType;

    void Awake()
    {
        invManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
        button = GetComponent<Button>();
        text = transform.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Update()
    {
        ItemLogic item = InventoryManager.instance.SelectedItem;
        switch (bType)
        {
            case ButtonTypes.BuySell:
                if (item != null)
                {
                    button.interactable = true;
                    if (item.gameObject.layer == 7)
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
                if (item != null)
                {
                    if (item.gameObject.layer == 6 && item.itemData.itemType != ItemData.ItemType.Weapon)
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
                if (Inventory.instance.shopInventory.Count >= 35)
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
    public void OnBuySell()
    {
        if (InventoryManager.instance.SelectedItem != null)
        {
            if (shopItem)
            {
                invManager.BuyItem();
            }
            else
                invManager.SellItem();
        }
        invManager.MoveSelector();
    }

    public void OnUse()
    {
        if (InventoryManager.instance.SelectedItem != null)
        {
            invManager.UseItem();
            invManager.MoveSelector();
        }
    }
}

