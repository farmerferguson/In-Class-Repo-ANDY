using UnityEngine;

namespace MyGame.Characters
{
    public class Hero : MonoBehaviour
    {
        public int health = 100;
        public void PrintHealth()
        {
        Debug.Log("Hero Health is: " + health);
        }

 
    }

}
