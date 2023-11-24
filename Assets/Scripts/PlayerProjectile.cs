using UnityEngine;

public class PlayerProjectile : MonoBehaviour {
    [SerializeField]
    private float moveSpeed;

    private void OnEnable() {
        Invoke(nameof(KillYourself), 2f);
    }

    private void FixedUpdate() {
        this.transform.position += new Vector3(1, 0, 0).normalized * moveSpeed;
    }

    private void KillYourself() {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Enemy")) {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
