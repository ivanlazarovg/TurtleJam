using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer spriteRenderer2;

    [SerializeField] AudioClip damageClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer.transform.localScale = new Vector3(health * 10, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.y);
        spriteRenderer2.transform.localScale = new Vector3(maxHealth * 10, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.y);
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
        spriteRenderer.transform.localScale = new Vector3(health * 10, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.y);
        spriteRenderer2.transform.localScale = new Vector3(maxHealth * 10, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.y);

        if (health <= 0) 
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    public void IncreaseMaxHealth(int amount) 
    {
        maxHealth += amount;
        health += amount;
        spriteRenderer.transform.localScale = new Vector3(health * 10, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.y);
        spriteRenderer2.transform.localScale = new Vector3(maxHealth * 10, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.y);
    }

    public void Heal(int amount) 
    {
        health += amount;
        if (health > maxHealth) 
        {
            health = maxHealth;
        }
        spriteRenderer.transform.localScale = new Vector3(health * 10, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.y);
        spriteRenderer2.transform.localScale = new Vector3(maxHealth * 10, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.y);
    }
}
