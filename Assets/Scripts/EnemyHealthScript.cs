using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;

        if (health <= 0)
        {
            FindFirstObjectByType<ScoreManagerScript>().AddScore(maxHealth);
            Destroy(gameObject);
        }
    }
}
