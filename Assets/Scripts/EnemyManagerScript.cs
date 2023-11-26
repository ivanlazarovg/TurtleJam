using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManagerScript : MonoBehaviour
{
    [SerializeField] GameObject LightMeleeEnemy; 
    [SerializeField] GameObject LightRangedEnemy;
    [SerializeField] GameObject MediumMeleeEnemy;
    [SerializeField] GameObject MediumRangedEnemy;
    [SerializeField] float distanceToSpawn;

    public float difficulty;
    public float maxDifficulty;
    public float timeTillNextEnemy;
    float currentTime;

    GameObject playerReference;
    AudioSource audioSource;

    private void Start()
    {
        playerReference = FindAnyObjectByType<PlayerHealthScript>().gameObject;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        difficulty += Time.deltaTime;
        currentTime -= Time.deltaTime;

        if (currentTime<= 0) 
        {
            SpawnEnemy();
            currentTime = timeTillNextEnemy - difficulty / maxDifficulty * 1.2f;
        }

        if (difficulty >= maxDifficulty) 
        {
            difficulty = maxDifficulty / 3;
            maxDifficulty += maxDifficulty / 2;
        }

    }

    void SpawnEnemy() 
    {
        int enemyToSpawn = Random.Range(0, 100) + (int)difficulty;
        if (enemyToSpawn <= 100)
        {
            Instantiate(LightMeleeEnemy, RandomCircle(playerReference.transform.position, distanceToSpawn), Quaternion.identity);
        }
        else if (enemyToSpawn <= 200)
        {
            Instantiate(LightRangedEnemy, RandomCircle(playerReference.transform.position, distanceToSpawn), Quaternion.identity);
            audioSource.Play();
        }
        else if (enemyToSpawn <= 500)
        {
            Instantiate(MediumMeleeEnemy, RandomCircle(playerReference.transform.position, distanceToSpawn), Quaternion.identity);
        }
        else 
        {
            Instantiate(MediumRangedEnemy, RandomCircle(playerReference.transform.position, distanceToSpawn), Quaternion.identity);

        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
