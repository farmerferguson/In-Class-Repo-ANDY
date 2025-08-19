using UnityEngine;
using MyGame.Characters;

public class Hero : MonoBehaviour
{
    public int health = 100;
    public void print health();
    {
        Debug.Log ("Player Health is" + health);
    }
    void Start
    {

    }

}
