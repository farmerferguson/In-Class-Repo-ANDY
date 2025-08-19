using UnityEngine;

[CreateAssetMenu(fileName = "Potion", menuName = "Scriptable Objects/Potion")]

public class PotionData : ScriptableObject
{
    public string potionName;
    public int healAmount;
}
