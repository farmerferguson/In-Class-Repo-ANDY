using UnityEngine;

namespace MyGame.Characters;

public class Enemy : MonoBehaviour
{
    public int damage = 25;
    
    public void PrintDamage()
    {
        Debug.Log("Enemy Damage is: " + damage);
    }
    


}
