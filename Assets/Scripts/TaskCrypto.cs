using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskCrypto : MonoBehaviour
{
    public Transform pointer;
    public Transform leftPoint;
    public Transform rightPoint;
    public Transform centerPoint;
    public GameObject window;
    public GameObject bar;
    public GameObject button;
    public SpriteRenderer orePlaceHolder;
    float timer = 0;
    bool pingpongswitch = false;
    public float pointerSpeed;
    public float pointerSpeedStartState;
    [Range(0, 1)]
    public float pointDifficulty;
    public bool isInWindow = false;

    int interactionPhase = 0;

    float pointerDistanceFromCenter;
    float pointerDistanceFromCenterRaw;

    private int currentHealth;
    private Color currentColor;

    Vector3 startOreSize;

    public SpriteRenderer barRenderer;
    public TextMeshProUGUI healthtext;
    public Color firstPhaseBarColor;

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

    private void Start()
    {
        startOreSize = orePlaceHolder.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        if(interactionPhase == 0)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                interactionPhase = 1;
            }
        }
        else if(interactionPhase == 1)
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
                pointerSpeed = Random.Range(1.2f, 2.3f);
            }
        }
        else if (pingpongswitch)
        {
            timer -= pointerSpeed * Time.deltaTime;
            if (timer <= 0)
            {
                pingpongswitch = false;
                pointerSpeed = Random.Range(1.2f, 2.3f);
            }
        }

        pointer.position = Vector2.Lerp(leftPoint.position, rightPoint.position, timer);

    }

    void StartState()
    {
        barRenderer.color = firstPhaseBarColor;
        pointerSpeed = pointerSpeedStartState;
        bar.SetActive(true);
        window.SetActive(true);
        button.SetActive(true);
    }

    public void MineButton()
    {
        if (interactionPhase == 1)
        {
           
            if (isInWindow)
            {
                Debug.Log("Started");
                interactionPhase++;
            }
            else if (!isInWindow)
            {
                Debug.Log("Missed");
                GetNextOre();
            }
        }
        else if(interactionPhase == 2)
        {
            currentHealth -= (int)pointerDistanceFromCenter;
            orePlaceHolder.transform.localScale -= new Vector3(pointerDistanceFromCenter 
                / OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex].healthPoints * 0.25f, 
                pointerDistanceFromCenter / OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex].healthPoints * 0.25f);
        }
    }

    void MineState()
    {
        barRenderer.color = new Color(255, 255, 255, 255);
        healthtext.gameObject.SetActive(true);
        healthtext.text = "Hit points: " + currentHealth;

        window.SetActive(false);
        if(Mathf.Abs(pointer.position.x - leftPoint.position.x) > Mathf.Abs(pointer.position.x - rightPoint.position.x))
        {
            pointerDistanceFromCenterRaw = Mathf.Abs(pointer.position.x - rightPoint.position.x);
        }
        else
        {
            pointerDistanceFromCenterRaw = Mathf.Abs(pointer.position.x - leftPoint.position.x);
        }
        pointerDistanceFromCenter = Mathf.Round(pointerDistanceFromCenterRaw/(1 + pointDifficulty));
        pointerSpeed = 1.3f + pointerDistanceFromCenterRaw / 3;

        if(currentHealth <= 0)
        {
            GetNextOre();
        }
        
    }

    public void GetNextOre()
    {
        currentHealth = OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex].healthPoints;
        currentColor = OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex].oreColor;
        orePlaceHolder.color = currentColor;
        OreGenerator.Instance.oreIndex++;
        bar.SetActive(false);
        button.SetActive(false);
        interactionPhase = 0;
        orePlaceHolder.transform.localScale = startOreSize;

    }
}
