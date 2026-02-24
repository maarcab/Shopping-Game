
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public int playerCoins = 100;

    public void HandleDrop(ItemLogic item, string buttonTag)
    {
        if (item == null) return;

        switch (buttonTag)
        {
            case "BuyButton":
                if (inventory.shopInventory.Contains(item) && playerCoins >= item.itemData.cost)
                {
                    playerCoins -= item.itemData.cost;
                    inventory.playerInventory.Add(item);
                    inventory.shopInventory.Remove(item);
                    Debug.Log($"Comprat: {item.itemData.itemName}");
                }
                break;

            case "SellButton":
                if (inventory.playerInventory.Contains(item))
                {
                    playerCoins += item.itemData.cost;
                    inventory.playerInventory.Remove(item);
                    inventory.shopInventory.Add(item);
                    Debug.Log($"Vengut: {item.itemData.itemName}");
                }
                break;

            case "UseButton":
                if (inventory.playerInventory.Contains(item) && (item.itemData.itemType == ItemData.ItemType.Food || item.itemData.itemType == ItemData.ItemType.Potion))
                {
                    Debug.Log($"Usat: {item.itemData.itemName} - Restaura {item.itemData.lifeRestore} de vida");
                    inventory.playerInventory.Remove(item);
                }
                else
                {
                    Debug.Log("Nom?s es poden usar ?tems comestibles o pocions.");
                }
                break;

            default:
                Debug.Log("Acci? desconeguda.");
                break;
        }
    }
}
