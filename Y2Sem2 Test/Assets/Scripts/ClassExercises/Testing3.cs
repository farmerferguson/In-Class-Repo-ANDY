using UnityEngine;

public class Testing3 : MonoBehaviour
{
    public InventoryItem item;

    void Start()
    {
        Debug.Log("Item: " + item.itemName);
        Debug.Log("Description: " + item.itemDescription);
        Debug.Log("Value: " + item.itemValue);
    }
    
}
