using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    [SerializeField] AudioClip damageClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public int GetMaxHealth() 
    {
        return maxHealth;
    }
    public void TakeDamage(int damage) 
    {
        health -= damage;
        audioSource.clip = damageClip;
        audioSource.Play();
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
