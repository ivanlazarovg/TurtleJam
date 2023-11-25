using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private float horizontalAxis;
    private float verticalAxis;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeUntilAttack;

    public float baseAttackSpeed;
    public int baseDamage;
    [SerializeField] private AudioClip walkingClip;

    private AudioSource audioSource;

    private Dictionary<Weapons, Vector2> weapons = new Dictionary<Weapons, Vector2>(); // Vector2. x = weapon level, Vector2.y = timeUntil next shot

    [SerializeField] private GameObject playerProjectile;
    [SerializeField] private GameObject magicMissile;
    [SerializeField] private GameObject fireball;
    [SerializeField] private GameObject iceRing;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject throwingDagger;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weapons.Add(Weapons.MagicMissile, new Vector2(0, 0));
        weapons.Add(Weapons.Fireball, new Vector2(0, 0));
        weapons.Add(Weapons.IceRing, new Vector2(0, 0));
        weapons.Add(Weapons.Sword, new Vector2(1, 0));
        weapons.Add(Weapons.ThrowingDagger, new Vector2(0, 0));
    }

    private void Update() {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        Dictionary<Weapons, Vector2> tempWeapons = new Dictionary<Weapons, Vector2>(weapons);
        foreach (var weapon in tempWeapons.Where(x => x.Value.x > 0))
        {
            switch (weapon.Key)
            {
                case Weapons.MagicMissile:
                    if (weapon.Value.y <= 0)
                    {
                        FireMagicMissile();
                        weapons[Weapons.MagicMissile] = new Vector2(weapon.Value.x, baseAttackSpeed);
                    }
                    else
                    {
                        weapons[Weapons.MagicMissile] = new Vector2(weapon.Value.x, weapon.Value.y - Time.deltaTime);
                    }
                    break;
                case Weapons.Fireball:
                    if (weapon.Value.y <= 0)
                    {
                        FireFireball();
                        weapons[Weapons.Fireball] = new Vector2(weapon.Value.x, baseAttackSpeed * 2.5f);
                    }
                    else
                    {
                        weapons[Weapons.Fireball] = new Vector2(weapon.Value.x, weapon.Value.y - Time.deltaTime);
                    }
                    break;
                case Weapons.IceRing:
                    if (weapon.Value.y <= 0)
                    {
                        FireIceRing();
                        weapons[Weapons.IceRing] = new Vector2(weapon.Value.x, baseAttackSpeed * 3f);
                    }
                    else
                    {
                        weapons[Weapons.IceRing] = new Vector2(weapon.Value.x, weapon.Value.y - Time.deltaTime);
                    }
                    break;
                case Weapons.Sword:
                    if (weapon.Value.y <= 0)
                    {
                        FireSword();
                        weapons[Weapons.Sword] = new Vector2(weapon.Value.x, baseAttackSpeed * 3f);
                    }
                    else
                    {
                        weapons[Weapons.Sword] = new Vector2(weapon.Value.x, weapon.Value.y - Time.deltaTime);
                    }
                    break;
                case Weapons.ThrowingDagger:
                    if (weapon.Value.y <= 0)
                    {
                        FireThrowingKnife();
                        weapons[Weapons.ThrowingDagger] = new Vector2(weapon.Value.x, baseAttackSpeed * 1.5f);
                    }
                    else
                    {
                        weapons[Weapons.ThrowingDagger] = new Vector2(weapon.Value.x, weapon.Value.y - Time.deltaTime);
                    }
                    break;
            }

        }

        transform.position += new Vector3(horizontalAxis, verticalAxis, 0).normalized * moveSpeed;
        if (!audioSource.isPlaying && (horizontalAxis != 0 || verticalAxis != 0))
        {
            audioSource.clip = walkingClip;
            audioSource.Play();
        }
    }

    private void FireMagicMissile()
    {
        GameObject projectile = Instantiate(magicMissile, transform.position, transform.rotation);
        projectile.GetComponent<PlayerProjectile>().SetStats(0.2f, baseDamage * (int)weapons[Weapons.MagicMissile].x, 0, PlayerProjectile.ShotType.ClosestEnemy, false, 3f);
    }

    private void FireFireball()
    {
        GameObject projectile = Instantiate(fireball, transform.position, transform.rotation);
        projectile.GetComponent<PlayerProjectile>().SetStats(0.1f, baseDamage * (int)weapons[Weapons.Fireball].x * 2, 1.5f * weapons[Weapons.Fireball].x, PlayerProjectile.ShotType.ClosestEnemy, false,3f);
    }

    private void FireIceRing() 
    {
        GameObject projectile = Instantiate(iceRing, transform.position, transform.rotation);
        projectile.GetComponent<PlayerProjectile>().SetStats(0.1f, baseDamage * (int)weapons[Weapons.IceRing].x / 2, 1.5f * weapons[Weapons.IceRing].x, PlayerProjectile.ShotType.AreaAroundPlayer, false,2f);
    }

    private void FireSword()
    {
        GameObject projectile = Instantiate(sword, transform.position, transform.rotation);
        projectile.GetComponent<PlayerProjectile>().SetStats(0.2f, baseDamage * (int)weapons[Weapons.Sword].x * 2, 0, PlayerProjectile.ShotType.ClosestEnemy, true, 0.2f);
    }

    private void FireThrowingKnife() 
    {
        GameObject projectile = Instantiate(throwingDagger, transform.position, transform.rotation);
        projectile.GetComponent<PlayerProjectile>().SetStats(0.15f, baseDamage * (int)weapons[Weapons.ThrowingDagger].x, 0, PlayerProjectile.ShotType.ClosestEnemy, true, 1f);
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

    public void ImproveInventory(Weapons weapon) 
    {
        weapons[weapon] = new Vector2(weapons[weapon].x + 1, weapons[weapon].y);
        Debug.Log(weapon + "has gotten to level " + weapons[weapon].x);
    }

    public enum Weapons 
    {
        MagicMissile,
        Fireball,
        IceRing,
        Sword,
        ThrowingDagger
    }
}
