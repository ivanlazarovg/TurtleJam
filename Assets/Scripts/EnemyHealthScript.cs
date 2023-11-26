using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        maxHealth += maxHealth * (int)FindAnyObjectByType<EnemyManagerScript>().maxDifficulty / 1000;
        health = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(int damage) 
    {
        health -= damage;

        StartCoroutine(DamageResponse());

        if (health <= 0)
        {
            FindFirstObjectByType<ScoreManagerScript>().AddScore(maxHealth);
            Destroy(gameObject);
        }
    }

    IEnumerator DamageResponse() 
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.color = Color.white;
    }
}
