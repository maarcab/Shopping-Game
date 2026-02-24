using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<ItemLogic> playerInventory = new List<ItemLogic>();
    public List<ItemLogic> shopInventory = new List<ItemLogic>();

    private void Awake()
    {
        instance = this;
    }
}
