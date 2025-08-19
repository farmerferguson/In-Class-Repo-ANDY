using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory Item")]

public class InventoryItem : ScriptableObject
{

    public string itemName;
    public int itemValue;
    [TextArea] public string itemDescription;
}
