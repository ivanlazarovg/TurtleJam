using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreGenerator : MonoBehaviour
{
    public List<OreObject> orePresets = new List<OreObject>();
    public Color[] oreColors;
    public Vector2 sizeBounds;
    public int oreIndex = 0;

    private static OreGenerator instance;

    public static OreGenerator Instance
    {
        get 
        { 
            
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<OreGenerator>();
            }

            return instance;
        
        }
    }

    private void Start()
    {

        for (int i = 0; i < 50; i++)
        {
            orePresets.Add(new OreObject());
            orePresets[i].oreColor = oreColors[Random.Range(0, 3)];
            orePresets[i].healthPoints = (int)Random.Range(sizeBounds.x, sizeBounds.y);
        }
        TaskCrypto.Instance.GetNextOre();
    }
}
