using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileScript : MonoBehaviour
{
    private float moveSpeed;
    private int damage;
    private float areaSize;

    private void OnEnable()
    {
        Invoke(nameof(KillYourself), 5f);
    }

    public void SetStats(float speed, int damage) 
    {
        moveSpeed = speed;
        this.damage = damage;
    }

    private void FixedUpdate()
    {
        this.transform.position += transform.right * moveSpeed;
    }

    private void KillYourself()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
