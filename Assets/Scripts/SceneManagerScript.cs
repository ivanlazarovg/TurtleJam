using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    
    void Start()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Additive);
    }

}
