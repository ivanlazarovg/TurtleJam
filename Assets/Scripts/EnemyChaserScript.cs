using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaserScript : MonoBehaviour
{
    
    private GameObject playerReference;
    private float hitTimer; // determines time between damaging player
    

    [SerializeField] private float movementSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float distanceToPlayer; // determines if it should try getting closer to player

    [SerializeField] bool rotateEnemyTowardsPlayer;
    SpriteRenderer spriteRenderer;

    
    void Start()
    {
        playerReference = FindAnyObjectByType<PlayerController>().gameObject;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(this.transform.position, playerReference.transform.position) > distanceToPlayer)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, playerReference.transform.position, movementSpeed);
            if (rotateEnemyTowardsPlayer) 
            {
                if (transform.position.x > playerReference.transform.position.x) 
                {
                    
                    spriteRenderer.flipX = false;
                }
                else 
                {
                    Debug.Log("God Help");
                    spriteRenderer.flipX = true;
                }
            }
        }

        hitTimer -= Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && hitTimer <= 0)
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(damage);
            hitTimer = timeBetweenAttacks;
        }
    }
}
