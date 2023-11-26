using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShiftBetweenTasks : MonoBehaviour
{
    float timerToShift;
    public Task[] tasks;
    public TextMeshProUGUI countdown;
    Task currentPick;
    public float timeMod;

    private void Start()
    {
        tasks = Resources.FindObjectsOfTypeAll<Task>();
        Shift();
    }

    private void Update()
    {
        if(timerToShift <= 0f)
        {
            Shift();      
        }
        timerToShift -= Time.deltaTime;
        //countdown.text = Mathf.Round(timerToShift).ToString();
    }

    void Shift()
    {
        foreach (Task task in tasks)
        {
            task.gameObject.SetActive(false);
        }

        currentPick = tasks[Random.Range(0, 3)];
        currentPick.gameObject.SetActive(true);
        if(currentPick.gameObject.GetComponent<TaskWriting>() != null )
        {
            TaskWriting.Instance.GenerateWordString();
        }
        else if(currentPick.gameObject.GetComponent<TaskCrypto>() != null)
        {
            TaskCrypto.Instance.GetNextOre();
        }
        else if(currentPick.gameObject.GetComponent<TaskSpreadSheet>() != null)
        {
            SpreadSheetGenerator.Instance.GenerateTable();
        }

        timerToShift = timeMod;
    }
}
