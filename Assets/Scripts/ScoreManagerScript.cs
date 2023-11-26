using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagerScript : MonoBehaviour
{
    [SerializeField] private int score;

    public void AddScore(int add) 
    {
        score += add;
        UpdateInfo();
    }

    private void UpdateInfo() 
    {
        // add ui
        //Debug.Log("Score: " + score);
    }
}
