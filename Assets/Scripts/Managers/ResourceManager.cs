using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [Header("Starting Resources")]
    [SerializeField] private int startingWood = 0;
    [SerializeField] private int startingFood = 0;
    [SerializeField] private int startingGold = 0;

    public int Wood { get; private set; }
    public int Food { get; private set; }
    public int Gold { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Wood = startingWood;
        Food = startingFood;
        Gold = startingGold;
    }

    public void AddResources(int wood, int food, int gold)
    {
        Wood = Mathf.Max(0, Wood + wood);
        Food = Mathf.Max(0, Food + food);
        Gold = Mathf.Max(0, Gold + gold);
    }

    public bool CanAfford(int wood, int food, int gold)
    {
        return Wood >= wood &&
               Food >= food &&
               Gold >= gold;
    }

    public bool TrySpend(int wood, int food, int gold)
    {
        if (!CanAfford(wood, food, gold))
            return false;

        Wood -= wood;
        Food -= food;
        Gold -= gold;

        return true;
    }

    public int GetTotalValue()
    {
        return Wood + Food + Gold;
    }
}