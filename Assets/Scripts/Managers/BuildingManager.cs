using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance { get; private set; }

    // =========================
    // POPULATION
    // =========================

    [Header("Population Settings")]
    [SerializeField] private int population = 2;
    [SerializeField] private int maxPopulation = 2;
    [SerializeField] private int populationPerHouse = 3;

    public int Population => population;
    public int MaxPopulation => maxPopulation;


    // =========================
    // WOODCUTTER
    // =========================

    [Header("Woodcutter Settings")]
    [SerializeField] private float woodcutterRange = 5f;
    [SerializeField] private float woodcutterWorkTime = 3f;
    [SerializeField] private int woodByTree = 5;

    private List<GameObject> woodcutters = new List<GameObject>();

    private readonly List<GameObject> houses = new List<GameObject>();


    // =========================
    // FARM
    // =========================

    [Header("Farm Settings")]
    [SerializeField] private float farmProductionTime = 3f;
    [SerializeField] private int foodPerFarm = 2;

    private List<GameObject> farms = new List<GameObject>();


    // =========================
    // TOWN HALL
    // =========================

    [Header("Town Hall Settings")]
    [SerializeField] private float townHallProductionTime = 5f;
    [SerializeField] private int goldPerCitizen = 1;


    // =========================
    // MARKET
    // =========================

    [Header("Market Settings")]
    [SerializeField] private float goldBoostPerMarket = 0.25f;

    private List<GameObject> markets = new List<GameObject>();


    // =========================
    // UNITY
    // =========================

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        GameObject[] foundHouses =
            GameObject.FindGameObjectsWithTag("House");

        foreach (GameObject house in foundHouses)
        {
            RegisterHouse(house);
        }

        GameObject[] foundWoodcutters =
            GameObject.FindGameObjectsWithTag("Woodcutter");

        foreach (GameObject woodcutter in foundWoodcutters)
        {
            RegisterWoodcutter(woodcutter);
        }

        GameObject[] foundFarms =
            GameObject.FindGameObjectsWithTag("Farm");

        foreach (GameObject farm in foundFarms)
        {
            RegisterFarm(farm);
        }

        GameObject[] foundMarkets =
            GameObject.FindGameObjectsWithTag("Market");

        foreach (GameObject market in foundMarkets)
        {
            RegisterMarket(market);
        }

        StartCoroutine(TownHallWork());
    }


    // =====================================================
    // WOODCUTTER
    // =====================================================

    public void RegisterWoodcutter(GameObject woodcutter)
    {
        if (!woodcutters.Contains(woodcutter))
        {
            woodcutters.Add(woodcutter);

            StartCoroutine(WoodcutterWork(woodcutter));
        }
    }

    private IEnumerator WoodcutterWork(GameObject woodcutter)
    {
        while (woodcutter != null)
        {
            GameObject tree = FindNearestTree(woodcutter);

            if (tree != null)
            {
                yield return new WaitForSeconds(woodcutterWorkTime);

                // Controlliamo che l'albero esista ancora
                if (tree != null)
                {
                    ResourceManager.Instance.AddResources(
                        woodByTree,
                        0,
                        0
                    );

                    Destroy(tree);
                }
            }
            else
            {
                // Nessun albero disponibile.
                // Riprova tra un secondo.
                yield return new WaitForSeconds(1f);
            }
        }
    }

    private GameObject FindNearestTree(GameObject woodcutter)
    {
        GameObject[] trees =
            GameObject.FindGameObjectsWithTag("Tree");

        GameObject nearestTree = null;

        float nearestDistance = Mathf.Infinity;

        foreach (GameObject tree in trees)
        {
            float distance = Vector2.Distance(
                woodcutter.transform.position,
                tree.transform.position
            );

            if (distance <= woodcutterRange &&
                distance < nearestDistance)
            {
                nearestTree = tree;
                nearestDistance = distance;
            }
        }

        return nearestTree;
    }


    // =====================================================
    // FARM
    // =====================================================

    public void RegisterFarm(GameObject farm)
    {
        if (!farms.Contains(farm))
        {
            farms.Add(farm);

            StartCoroutine(FarmWork(farm));
        }
    }

    private IEnumerator FarmWork(GameObject farm)
    {
        while (farm != null)
        {
            yield return new WaitForSeconds(farmProductionTime);

            if (farm != null)
            {
                ResourceManager.Instance.AddResources(
                    0,
                    foodPerFarm,
                    0
                );
            }
        }
    }


    // =====================================================
    // TOWN HALL
    // =====================================================

    private IEnumerator TownHallWork()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                townHallProductionTime
            );

            float marketMultiplier =
                1f + (markets.Count * goldBoostPerMarket);

            int goldProduced = Mathf.RoundToInt(
                population *
                goldPerCitizen *
                marketMultiplier
            );

            ResourceManager.Instance.AddResources(
                0,
                0,
                goldProduced
            );
        }
    }


    // =====================================================
    // MARKET
    // =====================================================

    public void RegisterMarket(GameObject market)
    {
        if (!markets.Contains(market))
        {
            markets.Add(market);
        }
    }


    // =====================================================
    // HOUSE
    // =====================================================

    public void RegisterHouse(GameObject house)
    {
        if (!houses.Contains(house))
        {
            houses.Add(house);

            maxPopulation += populationPerHouse;

            AddPopulation(1);
        }
    }

    public void UnregisterBuilding(GameObject building)
    {
        if (building == null)
            return;

        if (houses.Remove(building))
        {
            maxPopulation = Mathf.Max(0, maxPopulation - populationPerHouse);
            population = Mathf.Clamp(population, 0, maxPopulation);
        }

        woodcutters.Remove(building);
        farms.Remove(building);
        markets.Remove(building);
    }

    public float GetWoodPerSecond()
    {
        woodcutters.RemoveAll(item => item == null);
        return woodcutterWorkTime > 0f
            ? woodcutters.Count * woodByTree / woodcutterWorkTime
            : 0f;
    }

    public float GetFoodPerSecond()
    {
        farms.RemoveAll(item => item == null);
        return farmProductionTime > 0f
            ? farms.Count * foodPerFarm / farmProductionTime
            : 0f;
    }

    public float GetGoldPerSecond()
    {
        markets.RemoveAll(item => item == null);

        if (townHallProductionTime <= 0f)
            return 0f;

        float multiplier = 1f + markets.Count * goldBoostPerMarket;
        return population * goldPerCitizen * multiplier / townHallProductionTime;
    }

    public void AddPopulation(int amount)
    {
        population = Mathf.Clamp(
            population + amount,
            0,
            maxPopulation
        );
    }
}
