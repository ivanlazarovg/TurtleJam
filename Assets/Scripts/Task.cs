using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public float coinYield = 0;

    public static void AddCoins(float coinYield)
    {
        CoinsHub.Instance.coins += coinYield;
    }
}
