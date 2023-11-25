using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpreadSheetGenerator : MonoBehaviour
{
    public Color[] boxColorPicks;
    public List<BoxTable> boxPicksList;
    int boxPick;

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
        foreach (SlotSpreadsheet slot in Resources.FindObjectsOfTypeAll(typeof(SlotSpreadsheet)))
        {
            boxPick = Random.Range(0, boxPicksList.Count);
            slot.tier.amount = boxPicksList[boxPick].tier.amount;
            slot.tier.tier = boxPicksList[boxPick].tier.tier;

            boxPicksList.RemoveAt(boxPick);

            slot.slotColor = boxColorPicks[Random.Range(0, boxColorPicks.Length)];
            slot.amountText.text = slot.tier.amount.ToString();
        }
    }
}
