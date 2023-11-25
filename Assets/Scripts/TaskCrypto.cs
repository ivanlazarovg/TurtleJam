using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCrypto : MonoBehaviour
{
    public Transform pointer;
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform centerPoint;
    public GameObject window;
    float timer = 0;
    bool pingpongswitch = false;
    public float pointerSpeed;
    public float pointerSpeedStartState;
    [Range(0, 1)]
    public float pointDifficulty;
    public bool isInWindow = false;

    int interactionPhase = 1;

    float pointerDistanceFromCenter;
    float pointerDistanceFromCenterRaw;

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
        else if(interactionPhase == 2)
        {
            MineState();
        }
        if (!pingpongswitch)
        {
            timer += pointerSpeed * Time.deltaTime;
            if (timer >= 1)
            {
                pingpongswitch = true;
                //pointerSpeed = Random.Range(1.2f, 2.5f);
            }
        }
        else if (pingpongswitch)
        {
            timer -= pointerSpeed * Time.deltaTime;
            if (timer <= 0)
            {
                pingpongswitch = false;
                //pointerSpeed = Random.Range(1.2f, 2.5f);
            }
        }

        pointer.position = Vector2.Lerp(leftPoint.position, rightPoint.position, timer);

    }

    void StartState()
    { 
        if (Input.GetMouseButtonDown(0) && isInWindow)
        {
            Debug.Log("Started");
            interactionPhase++;
        }
        else if (Input.GetMouseButtonDown(0) && !isInWindow)
        {
            Debug.Log("Missed");
        }
        pointerSpeed = pointerSpeedStartState;
        window.SetActive(true);
    }

    void MineState()
    {
        window.SetActive(false);
        if(Mathf.Abs(pointer.position.x - leftPoint.position.x) > Mathf.Abs(pointer.position.x - rightPoint.position.x))
        {
            pointerDistanceFromCenterRaw = Mathf.Abs(pointer.position.x - rightPoint.position.x);
        }
        else
        {
            pointerDistanceFromCenterRaw = Mathf.Abs(pointer.position.x - leftPoint.position.x);
        }
        pointerDistanceFromCenter = Mathf.Round(pointerDistanceFromCenterRaw/(3+pointDifficulty));
        pointerSpeed = 1.3f + pointerDistanceFromCenterRaw / 3;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(pointerDistanceFromCenter);
        }
    }

}
