using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] Button startButt;
    [SerializeField] Button quitButt;
    void Start()
    {
        startButt.onClick.AddListener(StartGame);
        quitButt.onClick.AddListener(Application.Quit);
    }

    void StartGame() 
    {
        SceneManager.LoadScene("Mini Games", LoadSceneMode.Single);
    }
    
}
