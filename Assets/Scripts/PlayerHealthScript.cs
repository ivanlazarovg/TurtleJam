using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int health;

    public void TakeDamage(int damage) 
    {
        health -= damage;
        Debug.Log("Player Health: " + health);
    }
}
