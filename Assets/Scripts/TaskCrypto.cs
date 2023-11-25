using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCrypto : MonoBehaviour
{
    public Transform pointer;
    public Transform leftPoint;
    public Transform rightPoint;
    float timer = 0;
    bool pingpongswitch = false;
    public bool isInWindow = false;

    int interactionPhase = 1;

    private static TaskCrypto _instance;

    public static TaskCrypto Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TaskCrypto>();
            }

            return _instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(interactionPhase == 1)
        {
            StartState();
        }
        
    }

    void StartState()
    {
        if (!pingpongswitch)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                pingpongswitch = true;
            }
        }
        else if (pingpongswitch)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                pingpongswitch = false;
            }
        }

        pointer.position = Vector2.Lerp(leftPoint.position, rightPoint.position, timer);

        if (Input.GetMouseButtonDown(0) && isInWindow)
        {
            Debug.Log("Started");
            interactionPhase++;
        }
        else if (Input.GetMouseButtonDown(0) && !isInWindow)
        {
            Debug.Log("Missed");
        }
    }

}
