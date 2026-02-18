using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class InventoryManager : MonoBehaviour
{
    public Inventory playerInventory; // Referencia al inventario del jugador
    public Inventory shopInventory;   // Referencia al inventario de la tienda

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI shopCoinText;
    public TextMeshProUGUI healthText;
    public Slider healthBar;  //barra de vida

    [SerializeField] private int playerCoins = 100;
    [SerializeField] private int shopCoins = 10000;
    [SerializeField] private int health = 50;
    [SerializeField] ItemData[] items;
    [SerializeField] GameObject ItemSelector;

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
    public void BuyItem(ItemData selectedItem)
    {
        if (playerInventory.playerInventory.Count < 35)
        {
            if (selectedItem != null && shopInventory.shopInventory.Contains(selectedItem) && playerCoins >= selectedItem.cost)
            {
                playerCoins -= selectedItem.cost;
                shopCoins += selectedItem.cost;

                // Añadir el item al inventario del jugador y quitarlo de la tienda
                playerInventory.playerInventory.Add(selectedItem);
                shopInventory.shopInventory.Remove(selectedItem);

                UpdateUI();
            }
            else
            {
                Debug.Log("No tienes suficiente dinero o el objeto no está disponible.");
            }
        }
        else
        {
            Debug.Log("InventarioLleno");
        }
    }
    public void SellItem(ItemData selectedItem)
    {
        if (shopInventory.shopInventory.Count < 35)
        {
            if (selectedItem != null && playerInventory.playerInventory.Contains(selectedItem))
            {
                playerCoins += selectedItem.cost;
                shopCoins -= selectedItem.cost;

                // Añadir el item al inventario de la tienda y quitarlo del inventario del jugador
                playerInventory.playerInventory.Remove(selectedItem);
                shopInventory.shopInventory.Add(selectedItem);

                UpdateUI();
            }
            else
            {
                Debug.Log("No puedes vender este ítem.");
            }
        }
        else
        {
            Debug.Log("TiendaLlena");
        }
    }
    public void UseItem(ItemData selectedItem)
    {
        if (selectedItem != null && playerInventory.playerInventory.Contains(selectedItem))
        {
            if (selectedItem.itemType == ItemData.ItemType.Food || selectedItem.itemType == ItemData.ItemType.Potion)
            {
                Debug.Log($"Usado: {selectedItem.itemName} - Restaura vida");
                health += selectedItem.lifeRestore;
                health = Mathf.Clamp(health, 0, 100); // no superar max de salud
                playerInventory.playerInventory.Remove(selectedItem);
                UpdateUI();
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
        if (shopInventory.shopInventory.Count < 35)
        {
            int rnd = Random.Range(0, items.Length);
            Debug.Log(rnd);
            shopInventory.shopInventory.Add(items[rnd]);
        }
        else
        {
            Debug.Log("TiendaLlena");
        }
    }
    public void MoveSelector(GameObject itemSelected)
    {
        if (itemSelected != null)
        {
            ItemSelector.GetComponent<Image>().enabled = true;
            ItemSelector.transform.position = itemSelected.transform.position;
        }
        else
        {
            ItemSelector.GetComponent<Image>().enabled = false;
        }
    }
}