using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCoin : MonoBehaviour
{
    public Vector3 direction;

    private void OnEnable()
    {
        Invoke("Destroy", 2f);
    }
    private void Update()
    {
        transform.position += direction.normalized * 0.1f;

    }

    void Destroy()
    {
        Destroy(gameObject); 
    }
}
