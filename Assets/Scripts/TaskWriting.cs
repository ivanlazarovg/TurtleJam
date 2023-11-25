using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TaskWriting : Task
{
    private string[] words;
    [SerializeField] TextAsset wordsFile;
    string currentWords = string.Empty;
    public TextMeshProUGUI textStencil;
    public TextMeshProUGUI textWrittenTmPro;
    string textWritten = string.Empty;
    int index = 0;

    void Start()
    {
        words = wordsFile.ToString().ToLower().Split("\n");
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = words[i].Replace("\r", "");

        }
        Debug.Log(words);
        GenerateWordString();

    }
    void Update()
    {
        //CompareAndUpdateText();
        textStencil.text = currentWords;
        textWrittenTmPro.text = textWritten;
    }

    void GenerateWordString()
    {
        int wordlength = Random.Range(10, 40);

        for (int i = 0; i < wordlength; i++)
        {
            currentWords += words[Random.Range(1, words.Length)] + " ";

        }
        textStencil.text = currentWords;
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (((char)e.keyCode) == currentWords[index])
        {
            currentWords.Replace(currentWords[index], ' ');
            textWritten += currentWords[index];
            index++;
        }
    }


}
