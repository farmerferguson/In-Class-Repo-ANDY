using UnityEngine;

public class Testing2 : MonoBehaviour
{
    public PotionData potion;
    void Start()
    {
        Debug.Log("Potion: " + potion.potionName + " heals for " + potion.healAmount);
    }


}
