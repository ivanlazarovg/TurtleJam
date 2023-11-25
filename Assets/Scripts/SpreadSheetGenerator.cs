using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpreadSheetGenerator : MonoBehaviour
{
    public Color[] boxColorPicks;
    public List<BoxTable> boxPicksList;
    int boxPick;

    [HideInInspector]
    public List<BoxTable> boxPicks;
    private static SpreadSheetGenerator instance;
    public static SpreadSheetGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<SpreadSheetGenerator>();
            }

            return instance;

        }
    }

    public void GenerateTable()
    {
        boxPicks = new List<BoxTable>(boxPicksList);
        TaskSpreadSheet.Instance.timerSinceStart = 0;
        TaskSpreadSheet.Instance.won = false;
        foreach (SlotSpreadsheet slot in Resources.FindObjectsOfTypeAll(typeof(SlotSpreadsheet)))
        {
            boxPick = Random.Range(0, boxPicks.Count);
            slot.tier.amount = boxPicks[boxPick].tier.amount;
            slot.tier.tier = boxPicks[boxPick].tier.tier;
            slot.isActive = true;

            boxPicks.RemoveAt(boxPick);

            slot.slotColor = boxColorPicks[Random.Range(0, boxColorPicks.Length)];
            slot.amountText.text = slot.tier.amount.ToString();
        }
    }
}
