using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "boxPreset")]
public class BoxTable : ScriptableObject
{
    //public Color slotColor;
    public Tier tier;
    public bool isQuickTimeEvent;
}

[System.Serializable]
public class Tier
{
    public int tier;
    public int amount;
}
