using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSpreadSheet : MonoBehaviour
{
    private bool boxSelected;
    SlotSpreadsheet selectedSlot;

    private static TaskSpreadSheet instance;
    public static TaskSpreadSheet Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TaskSpreadSheet>();
            }

            return instance;

        }
    }


    void Update()
    {
        foreach (SlotSpreadsheet slot in Resources.FindObjectsOfTypeAll(typeof(SlotSpreadsheet)))
        {
            slot.amountText.text = slot.tier.amount.ToString();
            slot.slotSprite.color = slot.slotColor;
        }

        if (boxSelected == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.GetComponent<SlotSpreadsheet>() != null)
                {
                    boxSelected = true;
                    selectedSlot = hit.collider.gameObject.GetComponent<SlotSpreadsheet>();
}
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.GetComponent<SlotSpreadsheet>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<SlotSpreadsheet>().tier.tier == selectedSlot.tier.tier)
                    {
                        hit.collider.gameObject.GetComponent<SlotSpreadsheet>().tier.tier++;
                        hit.collider.gameObject.GetComponent<SlotSpreadsheet>().tier.amount *= 2;
                        selectedSlot.amountText.enabled = false;
                        selectedSlot.slotColor.a = 0f;
                    }
                            
                    boxSelected = false;
                }
            }
        }
        
    }
}
