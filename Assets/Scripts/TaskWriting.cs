using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Xml;

public class TaskWriting : Task
{
    private string[] words;
    [SerializeField] TextAsset wordsFile;
    string currentWords = string.Empty;
    public TextMeshProUGUI textStencil;
    public TextMeshProUGUI textWrittenTmPro;
    string textWritten = string.Empty;
    int index = 0;
    float timer = 0;
    bool isMistake = false;

    public float taskTimer;
    bool isTaskRunning = false;

    private static TaskWriting _instance;

    public Vector2 textSizeBounds;

    public static TaskWriting Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TaskWriting>();
            }

            return _instance;
        }
    }

    void Start()
    {
        words = wordsFile.ToString().ToLower().Split("\n");
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = words[i].Replace("\r", "");

        }
        Debug.Log(words);
        GenerateWordString();
        index = 0;

    }
    void Update()
    {
        //CompareAndUpdateText();
        textStencil.text = currentWords;
        textWrittenTmPro.text =  textWritten;
        

        if (isMistake)
        {
            timer += Time.deltaTime;
            if(timer > 1)
            {
                isMistake = false;
                timer = 0;
            }
        }

        if (isTaskRunning)
        {
            taskTimer += Time.deltaTime;
            if (index == currentWords.Length)
            {
                isTaskRunning = false;
                AddCoins(Mathf.Round((currentWords.Length / (taskTimer*2)) / 25));


            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GenerateWordString();
            }
        }
        
    }

    public void GenerateWordString()
    {
        currentWords = string.Empty;
        int wordlength = (int)Random.Range(textSizeBounds.x, textSizeBounds.y);
        currentWords += words[Random.Range(1, words.Length)];

        for (int i = 0; i < wordlength; i++)
        {
             currentWords += ' ' + words[Random.Range(1, words.Length)];
        }
        textStencil.text = string.Empty;
        textWritten = string.Empty;
        textStencil.text = currentWords;
        taskTimer = 0; 
        index = 0;
        isTaskRunning = true;
        
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown && e.keyCode != KeyCode.RightShift && !ShiftFocus.Instance.gameInFocus && e.keyCode != KeyCode.None && isTaskRunning)
        {
            Debug.Log(e.keyCode);
            Debug.Log(currentWords[index]);


            if (((char)e.keyCode) == currentWords[index])
            {
                Debug.Log("isin");
                textWritten += "<color=\"green\">" + currentWords[index];
                if (currentWords[index + 1] == ' ' && index < currentWords.Length)
                {
                    index+=2;
                    textWritten += ' ';
                }
                else
                {
                    index++;
                }

                
            }
            else
            {
                while (currentWords[index] != ' ')
                {
                    textWritten += "<color=\"red\">" + currentWords[index];
                    index++;
                    if (index == currentWords.Length)
                    {
                        isTaskRunning = false;
                        AddCoins(Mathf.Round(currentWords.Length / 3 + (currentWords.Length / taskTimer) / 25));

                    }
                }
                index++;
                textWritten += ' ';
                timer = 0;
                if(CoinsHub.Instance.coins >= 0)
                {
                    AddCoins(-1);
                }
                else
                {
                    CoinsHub.Instance.coins = 0;
                }
                

            }

        }

    }


}
