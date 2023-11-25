using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreWindow : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "pointer")
        {
           TaskCrypto.Instance.isInWindow= true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "pointer")
        {
            TaskCrypto.Instance.isInWindow = false;
        }
    }
}
