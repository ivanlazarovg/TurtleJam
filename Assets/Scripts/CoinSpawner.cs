using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;

    private static CoinSpawner instance;

    public static CoinSpawner Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CoinSpawner>();
            }

            return instance;

        }
    }
    public void SpawnCoins()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
            coin.GetComponent<FlyingCoin>().direction = new Vector3(Random.Range(Random.Range(-1f, -0.5f),Random.Range(1f, 0.5f)), Random.Range(-1f,1f), 0f);
        }
    }
    
}
