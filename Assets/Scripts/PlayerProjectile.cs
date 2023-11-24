using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    [SerializeField]
    private float moveSpeed;
    private int damage;
    private float areaSize;

    private void OnEnable() {
        Invoke(nameof(KillYourself), 2f);
    }

    public void SetStats(float speed, int damage, float areaSize) // if areaSize is set to 0, damages a single enemy!!!
    {
        moveSpeed = speed;
        this.damage = damage;
        this.areaSize = areaSize;
    }

    private void FixedUpdate() {
        this.transform.position += transform.right * moveSpeed;
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
}
