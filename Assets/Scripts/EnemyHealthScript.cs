using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] private int health;

    public void TakeDamage(int damage) 
    {
        health -= damage;
        Debug.Log(health);

        if (health <= 0) 
        {
            Destroy(gameObject);
        }
    }
}
