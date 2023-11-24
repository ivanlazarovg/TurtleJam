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
    
    void Start()
    {
        playerReference = FindAnyObjectByType<PlayerController>().gameObject;
    }

    void FixedUpdate()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, playerReference.transform.position, movementSpeed);
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
