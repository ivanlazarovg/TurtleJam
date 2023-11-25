using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float horizontalAxis;
    private float verticalAxis;

    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject playerProjectile;
    [SerializeField] private float timeUntilAttack;

    public float baseAttackSpeed;
    public int baseDamage; 

    private void Update() {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        transform.position += new Vector3(horizontalAxis, verticalAxis, 0).normalized * moveSpeed;

        if (timeUntilAttack <= 0)
        {
            timeUntilAttack = baseAttackSpeed;
            FireProjectile();
        }

        timeUntilAttack -= Time.deltaTime;
    }

    private void FireProjectile() {
        GameObject projectile = Instantiate(playerProjectile, transform.position, transform.rotation);
        projectile.GetComponent<PlayerProjectile>().SetStats(0.1f, baseDamage, 0, PlayerProjectile.ShotType.ClosestEnemy);
    }

    public void IncreaseMovementSpeed(int percent) 
    {
        moveSpeed += moveSpeed * percent / 100;
    }

    public void IncreaseDamage(int percent)
    {
        baseDamage += baseDamage * percent / 100;
    }

    public void IncreaseAttackSpeed(int percent)
    {
        baseAttackSpeed -= baseAttackSpeed * (float)percent / 100;
    }
}
