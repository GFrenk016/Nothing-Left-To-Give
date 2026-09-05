using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance { get; private set; }

    [Header("Round Settings")]
    [SerializeField] private float roundDuration = 60f;
    [SerializeField] private int startingTribute = 20;
    [SerializeField] private int tributeIncrease = 10;

    [Header("Production")]
    [SerializeField] private int woodPerSecond = 1;
    [SerializeField] private int foodPerSecond = 1;
    [SerializeField] private int goldPerSecond = 1;

    [Header("UI")]
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text tributeText;
    [SerializeField] private GameObject tributePanel;

    private int currentRound = 1;
    private int currentTribute;

    private float timeRemaining;
    private float productionTimer;

    private bool roundActive = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentTribute = startingTribute;
        StartRound();
    }

    private void Update()
    {
        if (!roundActive)
            return;

        timeRemaining -= Time.deltaTime;
        productionTimer += Time.deltaTime;

        if (productionTimer >= 1f)
        {
            ResourceManager.Instance.AddResources(
                woodPerSecond,
                foodPerSecond,
                goldPerSecond
            );

            productionTimer = 0f;
        }

        UpdateUI();

        if (timeRemaining <= 0f)
        {
            EndRound();
        }
    }

    private void StartRound()
    {
        roundActive = true;

        timeRemaining = roundDuration;
        productionTimer = 0f;

        if (tributePanel != null)
            tributePanel.SetActive(false);

        UpdateUI();
    }

    private void EndRound()
    {
        roundActive = false;

        if (tributePanel != null)
            tributePanel.SetActive(true);

        if (tributeText != null)
        {
            tributeText.text =
                "TRIBUTE REQUIRED: " + currentTribute;
        }
    }

    public void PayTribute()
    {
        if (ResourceManager.Instance.GetTotalValue() < currentTribute)
        {
            GameOverUI.Instance.Show();
            return;
        }

        SpendTributeValue(currentTribute);

        currentRound++;
        currentTribute += tributeIncrease;

        StartRound();
    }

    private void SpendTributeValue(int valueToSpend)
    {
        int remaining = valueToSpend;

        int spendGold = Mathf.Min(
            ResourceManager.Instance.Gold,
            remaining
        );

        ResourceManager.Instance.AddResources(
            0,
            0,
            -spendGold
        );

        remaining -= spendGold;

        if (remaining <= 0)
            return;

        int spendFood = Mathf.Min(
            ResourceManager.Instance.Food,
            remaining
        );

        ResourceManager.Instance.AddResources(
            0,
            -spendFood,
            0
        );

        remaining -= spendFood;

        if (remaining <= 0)
            return;

        int spendWood = Mathf.Min(
            ResourceManager.Instance.Wood,
            remaining
        );

        ResourceManager.Instance.AddResources(
            -spendWood,
            0,
            0
        );
    }

    private void UpdateUI()
    {
        if (roundText != null)
            roundText.text = "ROUND " + currentRound;

        if (timerText != null)
        {
            int seconds =
                Mathf.CeilToInt(timeRemaining);

            timerText.text = seconds.ToString();
        }
    }
}