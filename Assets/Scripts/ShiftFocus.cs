using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftFocus : MonoBehaviour
{
    public bool gameInFocus;
    void Start()
    {
        gameInFocus = false;     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            gameInFocus = !gameInFocus;
        }

        if (gameInFocus)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 1);
        }
        else 
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
    }
}
