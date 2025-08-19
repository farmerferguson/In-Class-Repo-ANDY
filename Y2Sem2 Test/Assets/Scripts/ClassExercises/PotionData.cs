using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "Scriptable Objects/Potion")]

public class PotionData : ScriptableObject
{
    public string potionName;
    public int healAmount;
}
