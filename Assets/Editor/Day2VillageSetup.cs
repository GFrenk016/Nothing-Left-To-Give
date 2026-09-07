using System;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Day2VillageSetup
{
    private const string ScenePath = "Assets/Scenes/SampleScene.unity";
    private const string PrefabFolder = "Assets/Prefabs";

    private static readonly Color PanelColor = new Color32(24, 30, 38, 245);
    private static readonly Color SectionColor = new Color32(39, 49, 60, 255);
    private static readonly Color AccentColor = new Color32(232, 174, 71, 255);
    private static readonly Color ButtonColor = new Color32(69, 91, 107, 255);
    private static readonly Color DangerColor = new Color32(151, 62, 58, 255);

    [InitializeOnLoadMethod]
    private static void AutoSetupWhenScriptsCompile()
    {
        if (Application.isBatchMode)
            return;

        EditorApplication.delayCall += () =>
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode && !IsAlreadyConfigured())
                SetupDay2Village();
        };
    }

    [MenuItem("Pixel Jam/Setup Day 2 Village")]
    public static void SetupDay2Village()
    {
        EnsureTag("Tree");
        EnsureTag("Woodcutter");
        EnsureTag("Farm");
        EnsureTag("Market");
        EnsureTag("House");

        if (!AssetDatabase.IsValidFolder(PrefabFolder))
            AssetDatabase.CreateFolder("Assets", "Prefabs");

        GameObject housePrefab = CreateBuildingPrefab("House", "Assets/Sprites/House.png");
        GameObject woodcutterPrefab = CreateBuildingPrefab("Woodcutter", "Assets/Sprites/Woodcutter.png");
        GameObject farmPrefab = CreateBuildingPrefab("Farm", "Assets/Sprites/Farm.png");
        GameObject marketPrefab = CreateBuildingPrefab("Market", "Assets/Sprites/Market.png");

        Scene scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);

        BuildingManager buildingManager = FindInScene<BuildingManager>(scene);
        ResourceManager resourceManager = FindInScene<ResourceManager>(scene);
        Canvas canvas = FindInScene<Canvas>(scene);
        GameObject townHall = FindByName(scene, "TownHall");

        if (buildingManager == null || resourceManager == null || canvas == null || townHall == null)
            throw new InvalidOperationException("SampleScene is missing BuildingManager, ResourceManager, Canvas, or TownHall.");

        ConfigureExistingBuildings(scene);
        ConfigureTownHallVisual(townHall);

        BuildingSystem buildingSystem = ConfigureBuildingSystem(scene, buildingManager.transform.parent, housePrefab, woodcutterPrefab, farmPrefab, marketPrefab);
        TownHallUI townHallUI = ConfigureVillageUI(canvas, townHall, buildingSystem);

        EditorUtility.SetDirty(buildingManager);
        EditorUtility.SetDirty(buildingSystem);
        EditorUtility.SetDirty(townHallUI);
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Selection.activeGameObject = townHall;
        Debug.Log("Day 2 Village Management configured successfully.");
    }

    public static void SetupFromCommandLine()
    {
        SetupDay2Village();
    }

    public static void ValidateFromCommandLine()
    {
        Scene scene = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Single);
        TownHallUI ui = FindInScene<TownHallUI>(scene);
        BuildingSystem system = FindInScene<BuildingSystem>(scene);
        BuildingManager buildingManager = FindInScene<BuildingManager>(scene);
        ResourceManager resourceManager = FindInScene<ResourceManager>(scene);
        GameObject townHall = FindByName(scene, "TownHall");

        Require(ui != null && system != null && buildingManager != null && resourceManager != null, "Runtime components are missing.");
        Require(townHall != null && townHall.GetComponent<Collider2D>() != null, "TownHall Collider2D is missing.");
        Require(FindByName(scene, "Statistics") != null && FindByName(scene, "Build") != null, "UI sections are missing.");

        SerializedObject serializedUI = new SerializedObject(ui);
        foreach (string property in new[]
                 {
                     "villageManagementPanel", "statisticsPanel", "buildPanel", "populationText", "woodText", "foodText",
                     "goldText", "woodPerSecondText", "foodPerSecondText", "goldPerSecondText", "selectionStatusText"
                 })
        {
            Require(serializedUI.FindProperty(property).objectReferenceValue != null, "Unassigned TownHallUI field: " + property);
        }

        string[] expectedCallbacks =
        {
            "ShowStatistics", "ShowBuild", "CloseVillageManagement", "BuildHouse", "BuildWoodcutter", "BuildFarm", "BuildMarket", "RemoveBuilding"
        };
        string[] callbacks = scene.GetRootGameObjects()
            .SelectMany(AllChildren)
            .Select(item => item.GetComponent<Button>())
            .Where(button => button != null)
            .SelectMany(button => Enumerable.Range(0, button.onClick.GetPersistentEventCount()).Select(button.onClick.GetPersistentMethodName))
            .ToArray();
        foreach (string callback in expectedCallbacks)
            Require(callbacks.Contains(callback), "Missing button callback: " + callback);

        foreach (string buildingName in new[] { "House", "Woodcutter", "Farm", "Market" })
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabFolder + "/" + buildingName + ".prefab");
            Require(prefab != null && prefab.CompareTag(buildingName) && prefab.GetComponent<Collider2D>() != null, "Invalid prefab: " + buildingName);
        }

        typeof(ResourceManager).GetField("startingWood", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(resourceManager, 100);
        InvokeAwake(resourceManager);
        InvokeAwake(buildingManager);
        InvokeAwake(system);

        int woodBefore = resourceManager.Wood;
        Vector3 smokePosition = new Vector3(1000f, 1000f, 0f);
        system.SelectHouse();
        Require(system.TryPlaceSelectedBuildingAt(smokePosition), "House placement smoke test failed.");
        Require(resourceManager.Wood == woodBefore - 20, "House cost was not spent correctly.");

        GameObject placedHouse = scene.GetRootGameObjects()
            .SelectMany(AllChildren)
            .FirstOrDefault(item => item.CompareTag("House") && Vector3.Distance(item.transform.position, smokePosition) < 0.01f);
        Require(placedHouse != null, "Placed House was not created or tagged.");
        system.SelectRemoveBuilding();
        Require(system.TryRemoveBuilding(placedHouse), "Remove Building smoke test failed.");

        Debug.Log("Day 2 Village validation passed: compile, references, callbacks, placement, spending, registration, and removal.");
    }

    private static void InvokeAwake(Component component)
    {
        MethodInfo method = component.GetType().GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic);
        Require(method != null, "Awake missing on " + component.GetType().Name);
        method.Invoke(component, null);
    }

    private static void Require(bool condition, string message)
    {
        if (!condition)
            throw new InvalidOperationException(message);
    }

    private static bool IsAlreadyConfigured()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (!scene.IsValid() || scene.path != ScenePath)
            return false;

        GameObject townHall = FindByName(scene, "TownHall");
        return townHall != null &&
               townHall.GetComponent<TownHallUI>() != null &&
               FindInScene<BuildingSystem>(scene) != null &&
               FindByName(scene, "VillageManagementPanel") != null &&
               AssetDatabase.LoadAssetAtPath<GameObject>(PrefabFolder + "/House.prefab") != null &&
               AssetDatabase.LoadAssetAtPath<GameObject>(PrefabFolder + "/Woodcutter.prefab") != null &&
               AssetDatabase.LoadAssetAtPath<GameObject>(PrefabFolder + "/Farm.prefab") != null &&
               AssetDatabase.LoadAssetAtPath<GameObject>(PrefabFolder + "/Market.prefab") != null;
    }

    private static void EnsureTag(string tag)
    {
        UnityEngine.Object[] assets = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset");
        if (assets == null || assets.Length == 0)
            throw new InvalidOperationException("TagManager.asset could not be loaded.");

        SerializedObject tagManager = new SerializedObject(assets[0]);
        SerializedProperty tags = tagManager.FindProperty("tags");

        for (int i = 0; i < tags.arraySize; i++)
        {
            if (tags.GetArrayElementAtIndex(i).stringValue == tag)
                return;
        }

        int index = tags.arraySize;
        tags.InsertArrayElementAtIndex(index);
        tags.GetArrayElementAtIndex(index).stringValue = tag;
        tagManager.ApplyModifiedProperties();
    }

    private static GameObject CreateBuildingPrefab(string buildingName, string spritePath)
    {
        Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
        if (sprite == null)
            throw new InvalidOperationException("Missing sprite: " + spritePath);

        GameObject root = new GameObject(buildingName);
        root.tag = buildingName;

        SpriteRenderer renderer = root.AddComponent<SpriteRenderer>();
        renderer.sprite = sprite;
        renderer.sortingLayerName = "Buildings";

        BoxCollider2D collider = root.AddComponent<BoxCollider2D>();
        collider.isTrigger = false;

        string path = PrefabFolder + "/" + buildingName + ".prefab";
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(root, path);
        UnityEngine.Object.DestroyImmediate(root);
        return prefab;
    }

    private static void ConfigureExistingBuildings(Scene scene)
    {
        foreach (string buildingName in new[] { "House", "Woodcutter", "Farm", "Market" })
        {
            foreach (GameObject candidate in scene.GetRootGameObjects().SelectMany(AllChildren).Where(item => item.name == buildingName))
            {
                candidate.tag = buildingName;
                if (candidate.GetComponent<Collider2D>() == null)
                    candidate.AddComponent<BoxCollider2D>();
                EditorUtility.SetDirty(candidate);
            }
        }
    }

    private static void ConfigureTownHallVisual(GameObject townHall)
    {
        if (townHall.GetComponent<Collider2D>() == null)
            townHall.AddComponent<BoxCollider2D>();

        SpriteRenderer renderer = townHall.GetComponent<SpriteRenderer>();
        if (renderer != null)
            renderer.sortingLayerName = "Buildings";
    }

    private static BuildingSystem ConfigureBuildingSystem(
        Scene scene,
        Transform managersParent,
        GameObject house,
        GameObject woodcutter,
        GameObject farm,
        GameObject market)
    {
        BuildingSystem system = FindInScene<BuildingSystem>(scene);
        if (system == null)
        {
            GameObject systemObject = new GameObject("BuildingSystem");
            if (managersParent != null)
                systemObject.transform.SetParent(managersParent, false);
            system = systemObject.AddComponent<BuildingSystem>();
        }

        SerializedObject serializedSystem = new SerializedObject(system);
        serializedSystem.FindProperty("housePrefab").objectReferenceValue = house;
        serializedSystem.FindProperty("woodcutterPrefab").objectReferenceValue = woodcutter;
        serializedSystem.FindProperty("farmPrefab").objectReferenceValue = farm;
        serializedSystem.FindProperty("marketPrefab").objectReferenceValue = market;
        serializedSystem.ApplyModifiedPropertiesWithoutUndo();
        return system;
    }

    private static TownHallUI ConfigureVillageUI(Canvas canvas, GameObject townHall, BuildingSystem buildingSystem)
    {
        Transform oldPanel = canvas.transform.Find("VillageManagementPanel");
        if (oldPanel != null)
            UnityEngine.Object.DestroyImmediate(oldPanel.gameObject);

        TownHallUI ui = townHall.GetComponent<TownHallUI>();
        if (ui == null)
            ui = townHall.AddComponent<TownHallUI>();

        GameObject panel = CreateUIObject("VillageManagementPanel", canvas.transform);
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = panelRect.anchorMax = new Vector2(0.5f, 0.5f);
        panelRect.sizeDelta = new Vector2(820f, 560f);
        panelRect.anchoredPosition = Vector2.zero;
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = PanelColor;

        CreateText(panel.transform, "Title", "VILLAGE MANAGEMENT", new Vector2(0f, 238f), new Vector2(560f, 54f), 30f, AccentColor, TextAlignmentOptions.Center);

        Button statisticsTab = CreateButton(panel.transform, "StatisticsTab", "STATISTICS", new Vector2(-160f, 178f), new Vector2(280f, 50f), ButtonColor);
        Button buildTab = CreateButton(panel.transform, "BuildTab", "BUILD", new Vector2(160f, 178f), new Vector2(280f, 50f), ButtonColor);
        Button closeButton = CreateButton(panel.transform, "CloseButton", "X", new Vector2(372f, 244f), new Vector2(46f, 42f), DangerColor);

        GameObject statistics = CreateSection(panel.transform, "Statistics", new Vector2(0f, -52f), new Vector2(740f, 380f));
        TMP_Text population = CreateStatRow(statistics.transform, "PopulationText", "Population", 125f);
        TMP_Text wood = CreateStatRow(statistics.transform, "WoodText", "Wood", 65f);
        TMP_Text food = CreateStatRow(statistics.transform, "FoodText", "Food", 5f);
        TMP_Text gold = CreateStatRow(statistics.transform, "GoldText", "Gold", -55f);
        CreateText(statistics.transform, "ProductionHeader", "PRODUCTION / SECOND", new Vector2(0f, -112f), new Vector2(600f, 36f), 20f, AccentColor, TextAlignmentOptions.Center);
        TMP_Text woodRate = CreateRate(statistics.transform, "WoodPerSecondText", "Wood", -220f);
        TMP_Text foodRate = CreateRate(statistics.transform, "FoodPerSecondText", "Food", 0f);
        TMP_Text goldRate = CreateRate(statistics.transform, "GoldPerSecondText", "Gold", 220f);

        GameObject build = CreateSection(panel.transform, "Build", new Vector2(0f, -52f), new Vector2(740f, 380f));
        Button houseButton = CreateBuildRow(build.transform, "HouseButton", "HOUSE", "20 Wood", 125f);
        Button woodcutterButton = CreateBuildRow(build.transform, "WoodcutterButton", "WOODCUTTER", "30 Wood", 55f);
        Button farmButton = CreateBuildRow(build.transform, "FarmButton", "FARM", "25 Wood", -15f);
        Button marketButton = CreateBuildRow(build.transform, "MarketButton", "MARKET", "30 Wood + 20 Gold", -85f);
        Button removeButton = CreateButton(build.transform, "RemoveBuildingButton", "REMOVE BUILDING", new Vector2(0f, -150f), new Vector2(330f, 48f), DangerColor);

        TMP_Text status = CreateText(canvas.transform, "BuildingModeStatus", "", new Vector2(0f, 34f), new Vector2(760f, 44f), 20f, Color.white, TextAlignmentOptions.Center);
        RectTransform statusRect = status.rectTransform;
        statusRect.anchorMin = new Vector2(0.5f, 0f);
        statusRect.anchorMax = new Vector2(0.5f, 0f);
        statusRect.anchoredPosition = new Vector2(0f, 34f);

        UnityEventTools.AddPersistentListener(statisticsTab.onClick, ui.ShowStatistics);
        UnityEventTools.AddPersistentListener(buildTab.onClick, ui.ShowBuild);
        UnityEventTools.AddPersistentListener(closeButton.onClick, ui.CloseVillageManagement);
        UnityEventTools.AddPersistentListener(houseButton.onClick, ui.BuildHouse);
        UnityEventTools.AddPersistentListener(woodcutterButton.onClick, ui.BuildWoodcutter);
        UnityEventTools.AddPersistentListener(farmButton.onClick, ui.BuildFarm);
        UnityEventTools.AddPersistentListener(marketButton.onClick, ui.BuildMarket);
        UnityEventTools.AddPersistentListener(removeButton.onClick, ui.RemoveBuilding);

        SerializedObject serializedUI = new SerializedObject(ui);
        serializedUI.FindProperty("villageManagementPanel").objectReferenceValue = panel;
        serializedUI.FindProperty("statisticsPanel").objectReferenceValue = statistics;
        serializedUI.FindProperty("buildPanel").objectReferenceValue = build;
        serializedUI.FindProperty("populationText").objectReferenceValue = population;
        serializedUI.FindProperty("woodText").objectReferenceValue = wood;
        serializedUI.FindProperty("foodText").objectReferenceValue = food;
        serializedUI.FindProperty("goldText").objectReferenceValue = gold;
        serializedUI.FindProperty("woodPerSecondText").objectReferenceValue = woodRate;
        serializedUI.FindProperty("foodPerSecondText").objectReferenceValue = foodRate;
        serializedUI.FindProperty("goldPerSecondText").objectReferenceValue = goldRate;
        serializedUI.FindProperty("selectionStatusText").objectReferenceValue = status;
        serializedUI.ApplyModifiedPropertiesWithoutUndo();

        build.SetActive(false);
        statistics.SetActive(true);
        panel.SetActive(false);
        return ui;
    }

    private static TMP_Text CreateStatRow(Transform parent, string name, string label, float y)
    {
        CreateText(parent, name + "Label", label, new Vector2(-210f, y), new Vector2(260f, 42f), 23f, Color.white, TextAlignmentOptions.Left);
        return CreateText(parent, name, "0", new Vector2(210f, y), new Vector2(260f, 42f), 23f, AccentColor, TextAlignmentOptions.Right);
    }

    private static TMP_Text CreateRate(Transform parent, string name, string label, float x)
    {
        return CreateText(parent, name, label + "  +0.0/s", new Vector2(x, -154f), new Vector2(200f, 38f), 18f, Color.white, TextAlignmentOptions.Center);
    }

    private static Button CreateBuildRow(Transform parent, string name, string label, string cost, float y)
    {
        Button button = CreateButton(parent, name, label, new Vector2(-160f, y), new Vector2(330f, 52f), ButtonColor);
        CreateText(parent, name + "Cost", cost, new Vector2(210f, y), new Vector2(280f, 48f), 19f, AccentColor, TextAlignmentOptions.Left);
        return button;
    }

    private static GameObject CreateSection(Transform parent, string name, Vector2 position, Vector2 size)
    {
        GameObject section = CreateUIObject(name, parent);
        RectTransform rect = section.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;
        Image image = section.AddComponent<Image>();
        image.color = SectionColor;
        return section;
    }

    private static Button CreateButton(Transform parent, string name, string label, Vector2 position, Vector2 size, Color color)
    {
        GameObject root = CreateUIObject(name, parent);
        RectTransform rect = root.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        Image image = root.AddComponent<Image>();
        image.color = color;
        Button button = root.AddComponent<Button>();
        button.targetGraphic = image;

        TMP_Text text = CreateText(root.transform, "Label", label, Vector2.zero, size, 19f, Color.white, TextAlignmentOptions.Center);
        text.raycastTarget = false;
        return button;
    }

    private static TMP_Text CreateText(Transform parent, string name, string value, Vector2 position, Vector2 size, float fontSize, Color color, TextAlignmentOptions alignment)
    {
        GameObject root = CreateUIObject(name, parent);
        RectTransform rect = root.GetComponent<RectTransform>();
        rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        TextMeshProUGUI text = root.AddComponent<TextMeshProUGUI>();
        text.text = value;
        text.fontSize = fontSize;
        text.color = color;
        text.alignment = alignment;
        text.enableWordWrapping = false;
        return text;
    }

    private static GameObject CreateUIObject(string name, Transform parent)
    {
        GameObject root = new GameObject(name, typeof(RectTransform));
        root.layer = LayerMask.NameToLayer("UI");
        root.transform.SetParent(parent, false);
        return root;
    }

    private static T FindInScene<T>(Scene scene) where T : Component
    {
        return scene.GetRootGameObjects()
            .SelectMany(AllChildren)
            .Select(item => item.GetComponent<T>())
            .FirstOrDefault(component => component != null);
    }

    private static GameObject FindByName(Scene scene, string name)
    {
        return scene.GetRootGameObjects().SelectMany(AllChildren).FirstOrDefault(item => item.name == name);
    }

    private static System.Collections.Generic.IEnumerable<GameObject> AllChildren(GameObject root)
    {
        yield return root;
        foreach (Transform child in root.transform)
        {
            foreach (GameObject descendant in AllChildren(child.gameObject))
                yield return descendant;
        }
    }
}
