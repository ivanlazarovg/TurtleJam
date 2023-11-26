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
        transform.position += direction.normalized * 8f * Time.deltaTime;

    }

    void Destroy()
    {
        Destroy(gameObject); 
    }
}
