using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScript : MonoBehaviour
{
    private void Awake()
    {
        this.transform.position = new Vector3 (1000, 0, 0);
        Camera camera = GetComponentInChildren<Camera>();
    }
}
