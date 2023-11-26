using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskSpreadSheet : Task
{
    private bool boxSelected;
    SlotSpreadsheet selectedSlot;
    private bool isFlashed = false;
    public float flashSpeedSelect;
    public float flashSpeedFail;
    public float flashSpeedSuccess;
    float t = 0;
    bool canChangeColor = false;
    public SpriteRenderer backdrop;
    public float timerSinceStart;
    public bool won = false;

    public GameObject winPanel;
    public TextMeshProUGUI scoreWinCondition;

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

    private void Start()
    {
        SpreadSheetGenerator.Instance.boxPicks = SpreadSheetGenerator.Instance.boxPicksList;
        SpreadSheetGenerator.Instance.GenerateTable();
    }

    void UpdateSlots()
    {
        if (!won)
            timerSinceStart += Time.deltaTime;
        foreach (SlotSpreadsheet slot in Resources.FindObjectsOfTypeAll(typeof(SlotSpreadsheet)))
        {
            if(!canChangeColor)
            slot.slotSprite.color = slot.slotColor;
        }
    }

    void Update()
    {
        UpdateSlots();


        if (boxSelected == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (Input.GetMouseButtonDown(0) && hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<SlotSpreadsheet>() != null)
                {
                    selectedSlot = hit.collider.gameObject.GetComponent<SlotSpreadsheet>();
                    StartCoroutine(ColorFlash(selectedSlot.slotColor, Color.yellow, selectedSlot.slotSprite, flashSpeedSelect));
                    boxSelected = true;

                }
            }
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider != null)

                {
                    if (hit.collider.gameObject.GetComponent<SlotSpreadsheet>() != null)
                    {
                        SlotSpreadsheet currentSlot = hit.collider.gameObject.GetComponent<SlotSpreadsheet>();
                        if (currentSlot.tier.tier == selectedSlot.tier.tier && currentSlot != selectedSlot)
                        {              
                            currentSlot.tier.tier++;
                            currentSlot.tier.amount *= 2;
                            currentSlot.amountText.text = currentSlot.tier.amount.ToString();
                            selectedSlot.Nullify();
                            StartCoroutine(ColorFlash(currentSlot.slotColor, Color.yellow, currentSlot.slotSprite, flashSpeedSelect));
                            if (currentSlot.tier.amount >= 80)
                            {
                                currentSlot.Nullify();
                                CheckIfAllDone();
                            }
                        }
                        else
                        {
                            StartCoroutine(ColorFlash(backdrop.color, Color.red,
                               backdrop, flashSpeedFail));
                        }

                        boxSelected = false;
                    }
                }
            }
        }


        
    }

    public IEnumerator ColorFlash(Color baseColor, Color desiredColor, SpriteRenderer renderer, float timeSpeed)
    {
        canChangeColor = true;
        StartCoroutine(ChangeFrameColor(baseColor, desiredColor, renderer, timeSpeed));

        yield return new WaitUntil(() => isFlashed);

        StartCoroutine(ChangeFrameColor(desiredColor, baseColor, renderer, timeSpeed));
        canChangeColor= false;
    }


    public IEnumerator ChangeFrameColor(Color color1, Color color2, SpriteRenderer renderer, float timeSpeed)
    {
        while (renderer.color != color2)
        {
            t += Time.deltaTime * timeSpeed;
            renderer.color = Color.Lerp(color1, color2, t);
            yield return new WaitForEndOfFrame();

        }
        t = 0;
        isFlashed = !isFlashed;

    }

    void WinCondition()
    {
        won = true;
        this.transform.GetChild(0).gameObject.SetActive(false);
        winPanel.SetActive(true);
        AddCoins(5 + (20 - timerSinceStart));
        scoreWinCondition.text = (5 + (20 - timerSinceStart)).ToString();
    }

    public void NewTable()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        winPanel.SetActive(false);
        SpreadSheetGenerator.Instance.GenerateTable();
    }

    void CheckIfAllDone()
    {
        foreach (SlotSpreadsheet slot in Resources.FindObjectsOfTypeAll(typeof(SlotSpreadsheet)))
        {
            if (slot.isActive)
            {
                return;
            }

        }
        WinCondition();

    }

}
