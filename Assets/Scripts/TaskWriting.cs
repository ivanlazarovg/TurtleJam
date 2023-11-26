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
    public TextMeshProUGUI textWrittenWronglyTmPro;
    string textWritten = string.Empty;
    string textWrittenWrongly = string.Empty;
    int index = 0;
    float timer = 0;
    bool isMistake = false;

    public float taskTimer;
    bool isTaskRunning = false;

    private static TaskWriting _instance;
    public TextMeshProUGUI coinDisplay;

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
        }
        else
        {
            coinDisplay.text = CoinsHub.Instance.coins.ToString();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GenerateWordString();
            }
        }
    }

    void GenerateWordString()
    {
        int wordlength = Random.Range(1, 5);
        currentWords += words[Random.Range(1, words.Length)];

        for (int i = 0; i < wordlength; i++)
        {
            currentWords += " " + words[Random.Range(1, words.Length)];

        }
        textStencil.text = currentWords;
        isTaskRunning = true;
        
    }

    private void OnGUI()
    {
        if (isTaskRunning)
        {
            Event e = Event.current;
            if (e.isKey && e.type == EventType.KeyDown && e.keyCode != KeyCode.None && !isMistake && isTaskRunning)
            {
                Debug.Log(currentWords.Length);
                Debug.Log(index);


                if (((char)e.keyCode) == currentWords[index])
                {
                    textWritten += "<color=\"green\">" + currentWords[index];
                    if (currentWords[index + 1] == ' ' && currentWords[index + 1] < currentWords.Length)
                    {
                        index++;
                        textWritten += ' ';
                    }
                    index++;
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
                            AddCoins(currentWords.Length / 6 + Mathf.Abs(currentWords.Length - taskTimer));

                        }
                    }
                    timer = 0;
                    isMistake = true;
                    coinYield -= 0.1f;
                    
                    index++;
                    textWritten += ' ';
                }
                if (currentWords[index] == currentWords.Length)
                {
                    isTaskRunning = false;
                    AddCoins(currentWords.Length / 3 + (currentWords.Length/taskTimer));
                    

                    Debug.Log(CoinsHub.Instance.coins);

                }
            }
        }
        
    }


}
