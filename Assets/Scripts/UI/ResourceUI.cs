using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text woodText;
    [SerializeField] private TMP_Text foodText;
    [SerializeField] private TMP_Text goldText;

    private void Update()
    {
        if (ResourceManager.Instance == null)
            return;

        woodText.text = "WOOD: " + ResourceManager.Instance.Wood;
        foodText.text = "FOOD: " + ResourceManager.Instance.Food;
        goldText.text = "GOLD: " + ResourceManager.Instance.Gold;
    }
}