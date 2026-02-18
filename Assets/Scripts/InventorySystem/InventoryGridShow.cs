using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridShow : MonoBehaviour
{
    Inventory inv;
    [SerializeField] bool isPlayerInv = false;
    [SerializeField] List<ItemData> inventoryToShow = new List<ItemData>();
    [SerializeField] List<Transform> objectsDisplayed = new List<Transform>();
    [SerializeField] List<GameObject> slots = new List<GameObject>();
    GridLayoutGroup grid;
    
    [SerializeField] GameObject[] objectPrefabs;//1-> Usable 2->Sellable 3->Buyable

    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("GameController").GetComponent<Inventory>();
       
        grid = gameObject.GetComponent<GridLayoutGroup>();
        if (isPlayerInv)
        {
            inventoryToShow = inv?.playerInventory;
        }
        else
        {
            inventoryToShow = inv?.shopInventory;
        }
    }

    void Update()
    {
        if (isPlayerInv)
        {
            if (inventoryToShow.Count != objectsDisplayed.Count)
            {
                inventoryToShow = inv?.playerInventory;
                CreateItems();
            }
        }
        else
        {
            if (inventoryToShow.Count != objectsDisplayed.Count)
            {
                inventoryToShow = inv?.shopInventory;
                CreateItems();
            }
        }
    }
    void CreateItems()
    {
        ClearLateObjects();
        objectsDisplayed.Clear();
        for (int i = 0; i < inventoryToShow.Count; i++)
        {
            GameObject k;
            if (isPlayerInv)
            {
                if (inventoryToShow[i].itemType != ItemData.ItemType.Weapon)
                {
                    k = Instantiate(objectPrefabs[0]);
                    Debug.Log("UsableItemCreated");
                }
                else
                {
                    k = Instantiate(objectPrefabs[1]);
                    Debug.Log("NonUsableItemCreated");
                }
            }
            else
            {
                k = Instantiate(objectPrefabs[2]);
                Debug.Log("ShopItemCreated");
            }

            k.SendMessage("OnData", inventoryToShow[i]);
            k.transform.SetParent(slots[i].transform, false);
            if (isPlayerInv)
            {
                k.layer = 6;
            }
            else
            {
                k.layer = 7;
            }
            objectsDisplayed.Add(k.transform);
        }
    }
    void ClearLateObjects()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].GetComponent<SlotLogic>().isEmpty && slots[i].transform.childCount > 0)
            {
                Destroy(slots[i].transform.GetChild(0).gameObject);
            }
        }
    }
}
