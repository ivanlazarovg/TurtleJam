using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProjectile : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyMask;

    private int damage;
    private float areaSize;
    private ShotType shotType;
    private GameObject targetReference;
    private bool piercing;
    private float lifetime;

    private List<Collider2D> currentEnemiesInRange;
    [SerializeField] int currentIndex;
    [SerializeField] int maxIndex;

    public void SetStats(float speed, int damage, float areaSize, ShotType shotType, bool piercing, float lifetime) // if areaSize is set to 0, damages a single enemy!!!
    {
        moveSpeed = speed;
        this.damage = damage;
        this.areaSize = areaSize;
        this.shotType = shotType;
        if (this.shotType == ShotType.ClosestEnemy)
        {
            var temp = Physics2D.OverlapCircleAll(this.transform.position, 20, enemyMask);
            if (temp.Length > 0)
            {
                targetReference = temp.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).First().gameObject;
                transform.right = targetReference.transform.position - transform.position;
            }
        }
        this.piercing = piercing;
        this.lifetime = lifetime;
        currentEnemiesInRange = new List<Collider2D>();

        Invoke(nameof(KillYourself), lifetime);
        currentIndex = maxIndex - 1;
    }

    private void FixedUpdate() 
    {
        currentIndex++;
        if (currentIndex >= maxIndex) 
        {
            currentIndex = 0;
            if (currentEnemiesInRange.Count > 0)
            {
                
                foreach (var item in currentEnemiesInRange)
                {
                    item.gameObject.GetComponent<EnemyHealthScript>().TakeDamage(damage);
                }
            }
            currentEnemiesInRange.Clear();

        }
        


        if (shotType == ShotType.AreaAroundPlayer) return;
        transform.position += transform.right * moveSpeed;
    }

    private void KillYourself() 
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shotType == ShotType.AreaAroundPlayer) return;
        if(collision.CompareTag("Enemy")) {
            if (areaSize == 0)
            {
                collision.gameObject.GetComponent<EnemyHealthScript>().TakeDamage(damage);
            }
            else 
            {
                var hits = Physics2D.OverlapCircleAll(transform.position, areaSize);
                
                foreach(var hit in hits) 
                {
                    if (hit.CompareTag("Enemy")) 
                    {
                        hit.gameObject.GetComponent<EnemyHealthScript>().TakeDamage(damage);
                    }
                }
            } 
            
            if(!piercing)Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !currentEnemiesInRange.Contains(collision) && shotType == ShotType.AreaAroundPlayer) currentEnemiesInRange.Add(collision);
    }

    public enum ShotType
    {
        Rotation,
        ClosestEnemy,
        AreaAroundPlayer
    }
}
