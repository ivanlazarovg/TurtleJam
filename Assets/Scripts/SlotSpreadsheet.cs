using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlotSpreadsheet : MonoBehaviour
{
    public int slotId;
    public Color slotColor;
    public Tier tier;
    public bool isQuickTimeEvent;
    public TextMeshProUGUI amountText;
    public SpriteRenderer slotSprite;

    private void Start()
    {
        slotSprite = GetComponent<SpriteRenderer>();
    }
}
