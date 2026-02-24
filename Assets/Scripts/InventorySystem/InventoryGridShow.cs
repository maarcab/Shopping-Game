using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridShow : MonoBehaviour
{
    public static InventoryGridShow instance;
    [SerializeField] List<SlotLogic> shopSlot = new List<SlotLogic>();
    [SerializeField] List<SlotLogic> inventorySlots = new List<SlotLogic>();

    void Awake()
    {
        instance = this;
    }

    public bool AddItemToSlot(GameObject item, bool toInventory)
    {
        foreach (SlotLogic s in toInventory ? inventorySlots : shopSlot)
        {
            if (s.isEmpty)
            {
                s.isEmpty = false;
                item.transform.parent = s.transform;
                item.transform.localPosition = Vector3.zero;
                return true;
            }
        }
        return false;
    }

    // Nuevo método para reorganizar los slots
    public void ReorganizeSlots(bool isPlayerInventory)
    {
        List<SlotLogic> slots = isPlayerInventory ? inventorySlots : shopSlot;
        List<ItemLogic> items = new List<ItemLogic>();

        foreach (SlotLogic slot in slots)
        {
            if (slot.transform.childCount > 0)
            {
                ItemLogic item = slot.transform.GetChild(0).GetComponent<ItemLogic>();
                if (item != null)
                {
                    items.Add(item);
                }
            }
        }
        foreach (SlotLogic slot in slots)
        {
            slot.isEmpty = true;
        }
        for (int i = 0; i < items.Count && i < slots.Count; i++)
        {
            slots[i].isEmpty = false;
            items[i].transform.SetParent(slots[i].transform);
            items[i].transform.localPosition = Vector3.zero;
        }
    }
}
