using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsHub : MonoBehaviour
{
    public float coins;

    private static CoinsHub _instance;

    public static CoinsHub Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CoinsHub>();
            }

            return _instance;
        }
    }

}
