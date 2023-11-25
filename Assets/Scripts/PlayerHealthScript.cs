using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    public int GetMaxHealth() 
    {
        return maxHealth;
    }
    public void TakeDamage(int damage) 
    {
        health -= damage;
    }

    public void IncreaseMaxHealth(int amount) 
    {
        maxHealth += amount;
        health += amount;
    }

    public void Heal(int amount) 
    {
        health += amount;
        if (health > maxHealth) 
        {
            health = maxHealth;
        }
    }
}
