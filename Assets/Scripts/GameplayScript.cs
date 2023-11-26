using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour
{
    private void Awake()
    {
        this.transform.position = new Vector3 (1000, 0, 0);
        Camera camera = GetComponentInChildren<Camera>();
        LevelUpAndLootboxManager manager = FindAnyObjectByType<LevelUpAndLootboxManager>();
        manager.levelUpButt = GameObject.Find("Level Up").GetComponent<Button>();
        manager.lootboxButt = GameObject.Find("Get Lootbox").GetComponent<Button>();
    }
}
