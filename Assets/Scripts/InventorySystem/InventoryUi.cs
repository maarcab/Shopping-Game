
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public int playerCoins = 100;

    public void HandleDrop(ItemData item, string buttonTag)
    {
        if (item == null) return;

        switch (buttonTag)
        {
            case "BuyButton":
                if (inventory.shopInventory.Contains(item) && playerCoins >= item.cost)
                {
                    playerCoins -= item.cost;
                    inventory.playerInventory.Add(item);
                    inventory.shopInventory.Remove(item);
                    Debug.Log($"Comprat: {item.itemName}");
                }
                break;

            case "SellButton":
                if (inventory.playerInventory.Contains(item))
                {
                    playerCoins += item.cost;
                    inventory.playerInventory.Remove(item);
                    inventory.shopInventory.Add(item);
                    Debug.Log($"Vengut: {item.itemName}");
                }
                break;

            case "UseButton":
                if (inventory.playerInventory.Contains(item) && (item.itemType == ItemData.ItemType.Food || item.itemType == ItemData.ItemType.Potion))
                {
                    Debug.Log($"Usat: {item.itemName} - Restaura {item.lifeRestore} de vida");
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

