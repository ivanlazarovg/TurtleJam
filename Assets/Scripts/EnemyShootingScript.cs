using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingScript : MonoBehaviour
{
    private GameObject playerReference;
    [SerializeField] float distanceToFire;
    [SerializeField] int damage;
    [SerializeField] float projectileSpeed;
    [SerializeField] float timeBetweenAttacks;
    float attackTimer;

    [SerializeField] GameObject projectile;

    void Start()
    {
        playerReference = FindAnyObjectByType<PlayerController>().gameObject;
    }

    void FixedUpdate()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0 && Vector3.Distance(this.transform.position, playerReference.transform.position) <= distanceToFire) 
        {
            attackTimer = timeBetweenAttacks;
            GameObject p = Instantiate(projectile, this.transform.position, this.transform.rotation);
            p.transform.right = playerReference.transform.position - transform.position;
            p.GetComponent<EnemyProjectileScript>().SetStats(projectileSpeed, damage);
        }
    }
}
