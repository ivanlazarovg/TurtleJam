using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskCrypto : Task
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
    public GameObject betweenTransform;

    int interactionPhase = 0;

    float pointerDistanceFromCenter;
    float pointerDistanceFromCenterRaw;

    private int currentHealth;
    private Color currentColor;

    bool hasHit;
    float timerToTrack = 0;

    Vector3 startOreSize;

    public SpriteRenderer barRenderer;
    public TextMeshProUGUI healthtext;
    public Color firstPhaseBarColor;
    public Color washedColor;

    public TextMeshProUGUI coinsDisplay;

    int currentCoinsMade = 0;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (interactionPhase == 1)
            {
                if (isInWindow)
                {
                    interactionPhase++;
                }
                else if (!isInWindow)
                {
                    GetNextOre();
                }
            }
            else if (interactionPhase == 2)
            {
                if (!hasHit)
                {
                    currentHealth -= (int)pointerDistanceFromCenter;
                    orePlaceHolder.transform.localScale -= new Vector3(pointerDistanceFromCenter
                    / OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex].healthPoints * 0.2f,
                    pointerDistanceFromCenter / OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex].healthPoints * 0.2f);
                    barRenderer.color = firstPhaseBarColor;
                    CoinSpawner.Instance.SpawnCoins();
                    hasHit = true;
                    Invoke("SwitchOreHit", 1f);
                }

            }
        }

        if (hasHit)
        {     
            pointer.gameObject.GetComponent<SpriteRenderer>().color = washedColor;
        }
        else
        {
            pointer.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }


        if(interactionPhase == 0)
        {
            if (currentCoinsMade != 0)
            {
                coinsDisplay.text = currentCoinsMade.ToString();
            }
           
            if (Input.GetKeyDown(KeyCode.Space))
            {
                interactionPhase = 1;
            }
            betweenTransform.SetActive(true);
            healthtext.gameObject.SetActive(false);
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
                pointerSpeed = Random.Range(1.2f, 2.5f);
            }
        }
        else if (pingpongswitch)
        {
            timer -= pointerSpeed * Time.deltaTime;
            if (timer <= 0)
            {
                pingpongswitch = false;
                pointerSpeed = Random.Range(1.2f, 2.6f);
            }
        }

        pointer.position = Vector2.Lerp(leftPoint.position, rightPoint.position, timer);
        
    }

    void StartState()
    {
        betweenTransform.SetActive(false);
        barRenderer.color = firstPhaseBarColor;
        pointerSpeed = pointerSpeedStartState;
        orePlaceHolder.gameObject.SetActive(true);
        bar.SetActive(true);
        window.SetActive(true);
        button.SetActive(true);
        
    }

    public void MineButton()
    {

        
    }

    void MineState()
    {
       
 

        timerToTrack += Time.deltaTime;
        if (!hasHit)
        {
            barRenderer.color = new Color(255, 255, 255, 255);
        }
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
            currentHealth = 0;
            currentCoinsMade = (int)(OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex-1].healthPoints/3 +
                Mathf.Clamp(((OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex-1].healthPoints+2) - timerToTrack),0,40 ));
            Debug.Log(OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex - 1].healthPoints);
            Debug.Log(timerToTrack);
            AddCoins(currentCoinsMade);
            GetNextOre();
        }
        
    }

    void SwitchOreHit()
    {
        hasHit = false;
    }

    public void GetNextOre()
    {
        currentHealth = OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex].healthPoints;
        currentColor = OreGenerator.Instance.orePresets[OreGenerator.Instance.oreIndex].oreColor;
        orePlaceHolder.color = currentColor;
        OreGenerator.Instance.oreIndex++;
        bar.SetActive(false);
        button.SetActive(false);
        orePlaceHolder.gameObject.SetActive(false);
        timerToTrack = 0;
        interactionPhase = 0;
        orePlaceHolder.transform.localScale = startOreSize;

    }
}
