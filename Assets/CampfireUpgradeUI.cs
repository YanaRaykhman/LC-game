using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CampfireUpgradeUI : MonoBehaviour
{
    public static CampfireUpgradeUI instance;

    public GameObject panel;
    public Transform requirementsParent;
    public GameObject requirementPrefab;
    public Button upgradeButton;

    int woodAdded = 0;
    int stoneAdded = 0;
    int coalAdded = 0;
    int crystalAdded = 0;

    private List<UpgradeRequirementUI> requirements = new List<UpgradeRequirementUI>();

    void Awake()
    {
        instance = this;
        panel.SetActive(false);
    }

    public void Open()
    {
        panel.SetActive(true);
        BuildRequirements();
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    void BuildRequirements()
    {
        foreach (Transform child in requirementsParent)
            Destroy(child.gameObject);

        requirements.Clear();

        var data = Campfire.instance.GetNextLevelData();

        if (data == null)
            return;

        if (data.requiredWood > 0)
            CreateRequirement(ResourceType.Wood, data.requiredWood - woodAdded);

        if (data.requiredStone > 0)
            CreateRequirement(ResourceType.Stone, data.requiredStone - stoneAdded);

        if (data.requiredCrystal > 0)
            CreateRequirement(ResourceType.Crystal, data.requiredCrystal - crystalAdded);

        if (data.requiredCoal > 0)
            CreateRequirement(ResourceType.Coal, data.requiredCoal - coalAdded);

        UpdateButton();
    }

    void CreateRequirement(ResourceType type, int amount)
    {
        GameObject obj = Instantiate(requirementPrefab, requirementsParent);
        var req = obj.GetComponent<UpgradeRequirementUI>();
        req.Setup(type, amount);
        requirements.Add(req);
    }

    public bool TryAddResource(ResourceType type)
    {
        foreach (var req in requirements)
        {
            if (req.resourceType == type && req.remainingAmount > 0)
            {
                req.Decrease();
                UpdateButton();
                Debug.Log(type);
                if (req.resourceType == ResourceType.Wood)
                    woodAdded++;
                if (req.resourceType == ResourceType.Stone)
                    stoneAdded++;
                if (req.resourceType == ResourceType.Coal)
                    coalAdded++;
                if (req.resourceType == ResourceType.Crystal)
                    crystalAdded++;
                /*switch (type)
                {
                    case ResourceType.Wood: woodAdded++;
                    case Stone:  stoneAdded++;
                    case Crystal: crystalAdded++;
                    case Coal: coalAdded++;
                }*/
                return true;
            }
        }

        return false;
    }

    void UpdateButton()
    {
        bool canUpgrade = true;

        foreach (var req in requirements)
        {
            if (req.remainingAmount > 0)
                canUpgrade = false;
        }

        upgradeButton.interactable = canUpgrade;
    }

    public void OnUpgradeClicked()
    {
        Campfire.instance.Upgrade();
        Close();
        woodAdded = 0;
        stoneAdded = 0;
        coalAdded = 0;
        crystalAdded = 0;
    }

    public bool IsOpen()
    {
        return panel.activeSelf;
    }
}