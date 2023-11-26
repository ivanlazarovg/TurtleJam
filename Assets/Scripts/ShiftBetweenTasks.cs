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
    Task lastPick;
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

        tasks[Random.Range(0,3)].gameObject.SetActive(true);
        
        timerToShift = timeMod;
    }
}
