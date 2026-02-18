using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int cost;
    public ItemType itemType;
    public int lifeRestore;  // Només per als consumibles.

    public enum ItemType { Food, Potion, Weapon }
}
