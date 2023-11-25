using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerProjectile : MonoBehaviour {
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyMask;

    private int damage;
    private float areaSize;
    private ShotType shotType;
    private GameObject targetReference;

    private void OnEnable() {
        Invoke(nameof(KillYourself), 2f);
    }

    public void SetStats(float speed, int damage, float areaSize, ShotType shotType) // if areaSize is set to 0, damages a single enemy!!!
    {
        moveSpeed = speed;
        this.damage = damage;
        this.areaSize = areaSize;
        this.shotType = shotType;
        if (this.shotType == ShotType.ClosestEnemy)
        {
            targetReference = Physics2D.OverlapCircle(this.transform.position, 20, enemyMask).gameObject;

        }
    }

    private void FixedUpdate() {
        switch (shotType) 
        {
            case ShotType.ClosestEnemy:
                if (targetReference == null)
                {
                    transform.position += transform.right * moveSpeed;
                    return;
                }
                transform.right = targetReference.transform.position - transform.position;
                transform.position += transform.right * moveSpeed;
                break;
            case ShotType.Rotation:
                transform.position += transform.right * moveSpeed;
                break;
        }
    }

    private void KillYourself() {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Enemy")) {
            if (areaSize == 0)
            {
                collision.gameObject.GetComponent<EnemyHealthScript>().TakeDamage(damage);
            }
            else 
            {
                var hits = Physics2D.OverlapCircleAll(transform.position, areaSize);
                
                foreach(var hit in hits) 
                {
                    Debug.Log(hit.name);
                    if (hit.CompareTag("Enemy")) 
                    {
                        hit.gameObject.GetComponent<EnemyHealthScript>().TakeDamage(damage);
                    }
                }
            } 
            
            Destroy(this.gameObject);
        }
    }
    public enum ShotType
    {
        Rotation,
        ClosestEnemy
    }
}
