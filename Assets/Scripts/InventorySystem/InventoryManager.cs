using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    //UI
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI shopCoinText;
    public TextMeshProUGUI healthText;
    public Slider healthBar;  //barra de vida

    [SerializeField] private int playerCoins = 100;
    [SerializeField] private int shopCoins = 10000;
    [SerializeField] private int health = 50;

    [SerializeField] ItemData[] items;
    [SerializeField] GameObject ItemSelector;
    [SerializeField] GameObject itemPrefab;

    public ItemLogic SelectedItem;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        UpdateUI();
    }
    private void OnEnable()
    {
        Localizer.OnLanguageChange += UpdateUI;
    }
    private void OnDisable()
    {
        Localizer.OnLanguageChange -= UpdateUI;
    }
    void UpdateUI()
    {
        coinText.text = $"{Localizer.GetText("Coins")}: {playerCoins}";
        shopCoinText.text = $"{Localizer.GetText("Coins")}: {shopCoins}";
        healthText.text = $"{Localizer.GetText("Health")}: {health}";

        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }
    int GetCoinValueFromText(string text)
    {
        string[] parts = text.Split(':');
        if (parts.Length > 1)
        {
            string coinValue = parts[1].Trim();
            int value;
            if (int.TryParse(coinValue, out value))
            {
                return value;
            }
        }
        return 0;
    }
    public bool BuyItem()
    {
        if (Inventory.instance.playerInventory.Count < 35)
        {
            if (SelectedItem != null && Inventory.instance.shopInventory.Contains(SelectedItem) && playerCoins >= SelectedItem.itemData.cost)
            {
                playerCoins -= SelectedItem.itemData.cost;
                shopCoins += SelectedItem.itemData.cost;

                SelectedItem.gameObject.layer = 6;
                SelectedItem.transform.SetParent(null);

                
                Inventory.instance.playerInventory.Add(SelectedItem);
                Inventory.instance.shopInventory.Remove(SelectedItem);

                // Asignar nuevo slot en el inventario del jugador
             
                InventoryGridShow.instance.AddItemToSlot(SelectedItem.gameObject, true);
                InventoryGridShow.instance.ReorganizeSlots(false);

                UpdateUI();
                MoveSelector();
                return true;
            }
            else
            {
                Debug.Log("No tienes suficiente dinero o el objeto no está disponible.");
                return false;
            }
        }
        else
        {
            Debug.Log("InventarioLleno");
            return false;
        }
    }
    public bool SellItem()
    {
        if (Inventory.instance.shopInventory.Count < 35)
        {
            if (SelectedItem != null && Inventory.instance.playerInventory.Contains(SelectedItem))
            {
                playerCoins += SelectedItem.itemData.cost;
                shopCoins -= SelectedItem.itemData.cost;

                SelectedItem.gameObject.layer = 7;
                SelectedItem.transform.SetParent(null);

                Inventory.instance.playerInventory.Remove(SelectedItem);
                Inventory.instance.shopInventory.Add(SelectedItem);

                InventoryGridShow.instance.AddItemToSlot(SelectedItem.gameObject, false);
                InventoryGridShow.instance.ReorganizeSlots(true);

                UpdateUI();
                MoveSelector();
                return true;
            }
            else
            {
                Debug.Log("No puedes vender este ítem.");
                return false;
            }
        }
        else
        {
            Debug.Log("TiendaLlena");
            return false;
        }
    }
    public void UseItem()
    {
        if (SelectedItem != null && Inventory.instance.playerInventory.Contains(SelectedItem))
        {
            if (SelectedItem.itemData.itemType == ItemData.ItemType.Food || SelectedItem.itemData.itemType == ItemData.ItemType.Potion)
            {
                Debug.Log($"Usado: {SelectedItem.itemData.itemName} - Restaura vida");
                health += SelectedItem.itemData.lifeRestore;
                health = Mathf.Clamp(health, 0, 100);

                Inventory.instance.playerInventory.Remove(SelectedItem);

                Destroy(SelectedItem.gameObject);
                SelectedItem = null;

                InventoryGridShow.instance.ReorganizeSlots(true);

                UpdateUI();
                MoveSelector();
            }
            else
            {
                Debug.Log("Solo se pueden usar ítems comestibles o pociones.");
            }
        }
        else
        {
            Debug.Log("Selecciona un ítem válido.");
        }
    }
    public void GenerateItem()
    {
        if (Inventory.instance.shopInventory.Count < 35)
        {
            GameObject k = Instantiate(itemPrefab);
            int rnd = Random.Range(0, items.Length);
            k.GetComponent<ItemLogic>().OnData(items[rnd]);

            k.layer = 7;

            Inventory.instance.shopInventory.Add(k.GetComponent<ItemLogic>());

            InventoryGridShow.instance.AddItemToSlot(k, false);
        }
        else
        {
            Debug.Log("TiendaLlena");
        }
    }
    public void MoveSelector()
    {
        if (SelectedItem != null)
        {
            ItemSelector.GetComponent<Image>().enabled = true;
            ItemSelector.transform.position = SelectedItem.transform.position;
        }
        else
        {
            ItemSelector.GetComponent<Image>().enabled = false;
        }
    }
}