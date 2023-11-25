using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyManagerScript : MonoBehaviour
{
    [SerializeField] GameObject LightMeleeEnemy; 
    [SerializeField] GameObject LightRangedEnemy;
    [SerializeField] float distanceToSpawn;

    public float difficulty;
    public float maxDifficulty;
    public float timeTillNextEnemy;
    public float currentTime;

    GameObject playerReference;

    private void Start()
    {
        playerReference = FindAnyObjectByType<PlayerHealthScript>().gameObject;
    }

    void FixedUpdate()
    {
        difficulty += Time.deltaTime;
        currentTime -= Time.deltaTime;

        if (currentTime<= 0) 
        {
            SpawnEnemy();
            currentTime = timeTillNextEnemy - difficulty / 100;
        }

        if (difficulty >= maxDifficulty) 
        {
            difficulty = maxDifficulty / 3;
            maxDifficulty += maxDifficulty / 2;
        }

    }

    void SpawnEnemy() 
    {
        int enemyToSpawn = Random.Range(0, 100);
        if (enemyToSpawn + difficulty <= 100)
        {
            Instantiate(LightMeleeEnemy, RandomCircle(playerReference.transform.position, distanceToSpawn), Quaternion.identity);
        }
        else 
        {
            Instantiate(LightRangedEnemy, RandomCircle(playerReference.transform.position, distanceToSpawn), Quaternion.identity);
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
