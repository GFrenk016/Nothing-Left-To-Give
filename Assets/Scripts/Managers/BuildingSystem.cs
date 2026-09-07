using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem Instance { get; private set; }

    public enum BuildingType
    {
        House,
        Woodcutter,
        Farm,
        Market
    }

    [Header("Building Prefabs")]
    [SerializeField] private GameObject housePrefab;
    [SerializeField] private GameObject woodcutterPrefab;
    [SerializeField] private GameObject farmPrefab;
    [SerializeField] private GameObject marketPrefab;

    [Header("House Cost")]
    [SerializeField] private int houseWoodCost = 20;
    [SerializeField] private int houseFoodCost = 0;
    [SerializeField] private int houseGoldCost = 0;

    [Header("Woodcutter Cost")]
    [SerializeField] private int woodcutterWoodCost = 30;
    [SerializeField] private int woodcutterFoodCost = 0;
    [SerializeField] private int woodcutterGoldCost = 0;

    [Header("Farm Cost")]
    [SerializeField] private int farmWoodCost = 25;
    [SerializeField] private int farmFoodCost = 0;
    [SerializeField] private int farmGoldCost = 0;

    [Header("Market Cost")]
    [SerializeField] private int marketWoodCost = 30;
    [SerializeField] private int marketFoodCost = 0;
    [SerializeField] private int marketGoldCost = 20;

    private BuildingType? selectedBuilding;
    private bool removeMode;

    public bool IsPlacing => selectedBuilding.HasValue;
    public bool IsRemoving => removeMode;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
            return;

        if (selectedBuilding == null)
        {
            if (removeMode && mouse.leftButton.wasPressedThisFrame && !IsPointerOverUI())
                RemoveBuildingAtMouse();

            if (removeMode && mouse.rightButton.wasPressedThisFrame)
                CancelBuilding();

            return;
        }

        if (mouse.leftButton.wasPressedThisFrame && !IsPointerOverUI())
        {
            PlaceSelectedBuilding();
        }

        if (mouse.rightButton.wasPressedThisFrame)
        {
            CancelBuilding();
        }
    }

    // UI BUTTONS

    public void SelectHouse()
    {
        removeMode = false;
        selectedBuilding = BuildingType.House;
    }

    public void SelectWoodcutter()
    {
        removeMode = false;
        selectedBuilding = BuildingType.Woodcutter;
    }

    public void SelectFarm()
    {
        removeMode = false;
        selectedBuilding = BuildingType.Farm;
    }

    public void SelectMarket()
    {
        removeMode = false;
        selectedBuilding = BuildingType.Market;
    }

    public void CancelBuilding()
    {
        selectedBuilding = null;
        removeMode = false;
    }

    public void SelectRemoveBuilding()
    {
        selectedBuilding = null;
        removeMode = true;
    }

    private void PlaceSelectedBuilding()
    {
        if (selectedBuilding == null || Camera.main == null)
            return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0f;

        if (!TryPlaceSelectedBuildingAt(mousePosition))
            Debug.Log("Building could not be placed. Check prefab and resources.");
    }

    public bool TryPlaceSelectedBuildingAt(Vector3 worldPosition)
    {
        if (!selectedBuilding.HasValue ||
            ResourceManager.Instance == null ||
            BuildingManager.Instance == null)
            return false;

        GetBuildingData(
            selectedBuilding.Value,
            out GameObject prefab,
            out int woodCost,
            out int foodCost,
            out int goldCost
        );

        if (prefab == null || !ResourceManager.Instance.TrySpend(woodCost, foodCost, goldCost))
            return false;

        worldPosition.z = 0f;
        GameObject building = Instantiate(prefab, worldPosition, Quaternion.identity);
        RegisterBuilding(selectedBuilding.Value, building);
        selectedBuilding = null;
        return true;
    }

    private void RemoveBuildingAtMouse()
    {
        if (Camera.main == null)
            return;

        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D[] hits = Physics2D.OverlapPointAll(worldPosition);

        foreach (Collider2D hit in hits)
        {
            GameObject candidate = hit.gameObject;

            if (!IsRemovableBuilding(candidate))
                continue;

            TryRemoveBuilding(candidate);
            return;
        }
    }

    public bool TryRemoveBuilding(GameObject candidate)
    {
        if (candidate == null || !IsRemovableBuilding(candidate))
            return false;

        if (BuildingManager.Instance != null)
            BuildingManager.Instance.UnregisterBuilding(candidate);

        if (Application.isPlaying)
            Destroy(candidate);
        else
            DestroyImmediate(candidate);

        removeMode = false;
        return true;
    }

    private static bool IsRemovableBuilding(GameObject candidate)
    {
        return candidate.CompareTag("House") ||
               candidate.CompareTag("Woodcutter") ||
               candidate.CompareTag("Farm") ||
               candidate.CompareTag("Market");
    }

    private static bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }

    private void RegisterBuilding(
        BuildingType type,
        GameObject building)
    {
        switch (type)
        {
            case BuildingType.House:
                BuildingManager.Instance.RegisterHouse(building);
                break;

            case BuildingType.Woodcutter:
                BuildingManager.Instance.RegisterWoodcutter(building);
                break;

            case BuildingType.Farm:
                BuildingManager.Instance.RegisterFarm(building);
                break;

            case BuildingType.Market:
                BuildingManager.Instance.RegisterMarket(building);
                break;
        }
    }

    private void GetBuildingData(
        BuildingType type,
        out GameObject prefab,
        out int wood,
        out int food,
        out int gold)
    {
        prefab = null;
        wood = 0;
        food = 0;
        gold = 0;

        switch (type)
        {
            case BuildingType.House:
                prefab = housePrefab;
                wood = houseWoodCost;
                food = houseFoodCost;
                gold = houseGoldCost;
                break;

            case BuildingType.Woodcutter:
                prefab = woodcutterPrefab;
                wood = woodcutterWoodCost;
                food = woodcutterFoodCost;
                gold = woodcutterGoldCost;
                break;

            case BuildingType.Farm:
                prefab = farmPrefab;
                wood = farmWoodCost;
                food = farmFoodCost;
                gold = farmGoldCost;
                break;

            case BuildingType.Market:
                prefab = marketPrefab;
                wood = marketWoodCost;
                food = marketFoodCost;
                gold = marketGoldCost;
                break;
        }
    }
}
