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
    public bool isActive = true;

    private void Start()
    {
        isActive = true; 
        slotSprite = GetComponent<SpriteRenderer>();
    }

    public void Nullify()
    {
        isActive= false;
        amountText.text = string.Empty;
        slotColor.a = 0;
    }
}
