using UnityEngine;

public class DropZone : MonoBehaviour
{
    public enum DropAction { Use, Sell, Buy }
    public DropAction action;

    private InventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ItemLogic dragItem = other.GetComponent<ItemLogic>();
        dragItem.CheckZoneCollision(this);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        ItemLogic dragItem = other.GetComponent<ItemLogic>();
        dragItem.CheckZoneCollision(null);
    }
    public bool ActivateAction(ItemData item)
    {
        switch (action)
        {
            case DropAction.Sell:
                return inventoryManager.SellItem(item);

            case DropAction.Buy:
                return inventoryManager.BuyItem(item);
        }
        return false;
        // Destruir el objeto despu?s de la acci?n
        //Destroy(item);
    }
}
