using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpAndLootboxManager : MonoBehaviour
{
    [SerializeField] public Button levelUpButt;
    [SerializeField] public Button lootboxButt;

    [SerializeField] private float levelUpPrice;
    [SerializeField] private float lootboxPrice;

    [SerializeField] Sprite MagicMissileSprite;
    [SerializeField] Sprite FireballSprite;
    [SerializeField] Sprite RingOfIceSprite;
    [SerializeField] Sprite SwordSprite;
    [SerializeField] Sprite ThrowingDagger;
    

    [SerializeField] GameObject lootboxLooks;
    [SerializeField] Image lootboxSprite;

    [SerializeField] AudioClip LevelUpClip;
    [SerializeField] AudioClip LootboxClip;
    [SerializeField] AudioClip WrongClip;

    AudioSource audioSource;
    PlayerController playerController;

    private void Start()
    {
        levelUpButt.onClick.AddListener(LevelUp);
        lootboxButt.onClick.AddListener(GetLootBox);
        audioSource = GetComponent<AudioSource>();
        CoinsHub.Instance.coins += 10;
        playerController = FindAnyObjectByType<PlayerController>();

        lootboxLooks.SetActive(false);
    }

    void LevelUp() 
    {
        if (CoinsHub.Instance.coins >= levelUpPrice)
        {
            CoinsHub.Instance.coins -= levelUpPrice;

            PossibleUpgrades currentUpgrade = (PossibleUpgrades)Random.Range(0, 4);
            switch (currentUpgrade) // don't remove brackets, that way we spare memory
            {
                case PossibleUpgrades.Health:
                    {
                        PlayerHealthScript playerHealth = FindAnyObjectByType<PlayerHealthScript>();
                        playerHealth.IncreaseMaxHealth(playerHealth.GetMaxHealth() / 10);
                        break;
                    }
                case PossibleUpgrades.Damage:
                    {
                        PlayerController playerController = FindAnyObjectByType<PlayerController>();
                        playerController.IncreaseDamage(15);
                        break;
                    }
                case PossibleUpgrades.AttackSpeed:
                    {
                        PlayerController playerController = FindAnyObjectByType<PlayerController>();
                        playerController.IncreaseAttackSpeed(15);
                        break;
                    }

                case PossibleUpgrades.MovementSpeed:
                    {
                        PlayerController playerController = FindAnyObjectByType<PlayerController>();
                        playerController.IncreaseMovementSpeed(15);
                        break;
                    }
                case PossibleUpgrades.Luck: // intentional game design
                    break;
            }
            audioSource.clip = LevelUpClip;
            audioSource.Play();
        }
        else 
        {
            audioSource.clip = WrongClip;
            audioSource.Play();
        }
    }
    void GetLootBox() 
    {
        if (CoinsHub.Instance.coins >= lootboxPrice)
        {
            CoinsHub.Instance.coins -= lootboxPrice;
            audioSource.clip = LootboxClip;
            audioSource.Play();

            PlayerController.Weapons newWeapon = (PlayerController.Weapons)Random.Range(0, 5);

            playerController.ImproveInventory(newWeapon);

            switch (newWeapon) 
            {
                case PlayerController.Weapons.MagicMissile:
                    lootboxSprite.sprite = MagicMissileSprite;
                    break;
                case PlayerController.Weapons.Fireball:
                    lootboxSprite.sprite = FireballSprite;
                    break;
                case PlayerController.Weapons.IceRing:
                    lootboxSprite.sprite = RingOfIceSprite;
                    break;
                case PlayerController.Weapons.Sword:
                    lootboxSprite.sprite = SwordSprite;
                    break;
                case PlayerController.Weapons.ThrowingDagger:
                    lootboxSprite.sprite = ThrowingDagger;
                    break;
            }

            StartCoroutine(ShowLootbox());
        }
        else 
        {
            audioSource.clip = WrongClip;
            audioSource.Play();
        }
    }

    private IEnumerator ShowLootbox() 
    {
        lootboxLooks.SetActive(true);
        yield return new WaitForSeconds(1f);
        lootboxLooks.SetActive(false);
    }

    enum PossibleUpgrades 
    {
        Health,
        Damage,
        AttackSpeed,
        MovementSpeed,
        Luck
    }
}
