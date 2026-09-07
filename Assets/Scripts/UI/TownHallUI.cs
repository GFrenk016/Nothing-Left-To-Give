using TMPro;
using UnityEngine;

public class TownHallUI : MonoBehaviour
{
    [Header("Main Panel")]
    [SerializeField] private GameObject villageManagementPanel;

    [Header("Sections")]
    [SerializeField] private GameObject statisticsPanel;
    [SerializeField] private GameObject buildPanel;

    [Header("Population UI")]
    [SerializeField] private TMP_Text populationText;

    [Header("Resources UI")]
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text goldText;

    [Header("Production UI")]
    [SerializeField] private TMP_Text woodPerSecondText;
    [SerializeField] private TMP_Text foodPerSecondText;
    [SerializeField] private TMP_Text goldPerSecondText;

    [Header("Feedback")]
    [SerializeField] private TMP_Text selectionStatusText;

    private void Start()
    {
        villageManagementPanel.SetActive(false);
    }

    private void Update()
    {
        if (!villageManagementPanel.activeSelf)
            return;

        UpdateStatistics();
    }

    // =========================
    // TOWH HALL
    // =========================

    private void OnMouseDown()
    {
        OpenVillageManagement();
    }

    public void OpenVillageManagement()
    {
        villageManagementPanel.SetActive(true);

        ShowStatistics();

        UpdateStatistics();
    }

    public void CloseVillageManagement()
    {
        villageManagementPanel.SetActive(false);
    }


    // =========================
    // SECTIONS
    // =========================

    public void ShowStatistics()
    {
        statisticsPanel.SetActive(true);
        buildPanel.SetActive(false);

        UpdateStatistics();
    }

    public void ShowBuild()
    {
        statisticsPanel.SetActive(false);
        buildPanel.SetActive(true);
    }


    // =========================
    // STATISTICS
    // =========================

    private void UpdateStatistics()
    {
        if (ResourceManager.Instance == null ||
            BuildingManager.Instance == null)
            return;

        populationText.text =
            BuildingManager.Instance.Population +
            " / " +
            BuildingManager.Instance.MaxPopulation;

        woodText.text =
            ResourceManager.Instance.Wood.ToString();

        foodText.text =
            ResourceManager.Instance.Food.ToString();

        goldText.text =
            ResourceManager.Instance.Gold.ToString();

        woodPerSecondText.text =
            "+" +
            BuildingManager.Instance
                .GetWoodPerSecond()
                .ToString("0.0") +
            "/s";

        foodPerSecondText.text =
            "+" +
            BuildingManager.Instance
                .GetFoodPerSecond()
                .ToString("0.0") +
            "/s";

        goldPerSecondText.text =
            "+" +
            BuildingManager.Instance
                .GetGoldPerSecond()
                .ToString("0.0") +
            "/s";
    }


    // =========================
    // BUILD BUTTONS
    // =========================

    public void BuildHouse()
    {
        BuildingSystem.Instance.SelectHouse();

        ShowPlacementMessage("House selected - left click to place, right click to cancel");

        CloseVillageManagement();
    }

    public void BuildWoodcutter()
    {
        BuildingSystem.Instance.SelectWoodcutter();

        ShowPlacementMessage("Woodcutter selected - left click to place, right click to cancel");

        CloseVillageManagement();
    }

    public void BuildFarm()
    {
        BuildingSystem.Instance.SelectFarm();

        ShowPlacementMessage("Farm selected - left click to place, right click to cancel");

        CloseVillageManagement();
    }

    public void BuildMarket()
    {
        BuildingSystem.Instance.SelectMarket();

        ShowPlacementMessage("Market selected - left click to place, right click to cancel");

        CloseVillageManagement();
    }

    public void RemoveBuilding()
    {
        BuildingSystem.Instance.SelectRemoveBuilding();
        ShowPlacementMessage("Remove mode - click a building, right click to cancel");
        CloseVillageManagement();
    }

    private void ShowPlacementMessage(string message)
    {
        if (selectionStatusText != null)
            selectionStatusText.text = message;
    }
}
