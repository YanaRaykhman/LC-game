using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    public Transform gridParent;

    public GameObject woodIconPrefab;
    public GameObject stoneIconPrefab;
    public GameObject appleIconPrefab;
    public GameObject berryIconPrefab;
    public GameObject fleshIconPrefab;
    public GameObject meatIconPrefab;
    public GameObject coalIconPrefab;
    public GameObject crystalIconPrefab;

    List<GameObject> icons = new List<GameObject>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Inventory.instance.OnInventoryChanged += Refresh;
    }

    void Refresh()
    {
        foreach (var icon in icons)
        {
            Destroy(icon);
        }

        icons.Clear();

        CreateIcons(ResourceType.Wood, Inventory.instance.Get(ResourceType.Wood));
        CreateIcons(ResourceType.Stone, Inventory.instance.Get(ResourceType.Stone));
        CreateIcons(ResourceType.Coal, Inventory.instance.Get(ResourceType.Coal));
        CreateIcons(ResourceType.Apple, Inventory.instance.Get(ResourceType.Apple));
        CreateIcons(ResourceType.Berry, Inventory.instance.Get(ResourceType.Berry));
        CreateIcons(ResourceType.Flesh, Inventory.instance.Get(ResourceType.Flesh));
        CreateIcons(ResourceType.Meat, Inventory.instance.Get(ResourceType.Meat));
        CreateIcons(ResourceType.Crystal, Inventory.instance.Get(ResourceType.Crystal));
    }

    void CreateIcons(ResourceType type, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject prefab = GetPrefab(type);

            GameObject icon = Instantiate(prefab, gridParent);

            ResourceIcon iconScript = icon.GetComponent<ResourceIcon>();
            iconScript.resourceType = type;

            icons.Add(icon);
        }
    }

    GameObject GetPrefab(ResourceType type)
    {
        switch (type)
        {
            case ResourceType.Wood: return woodIconPrefab;
            case ResourceType.Stone: return stoneIconPrefab;
            case ResourceType.Coal: return coalIconPrefab;
            case ResourceType.Apple: return appleIconPrefab;
            case ResourceType.Berry: return berryIconPrefab;
            case ResourceType.Flesh: return fleshIconPrefab;
            case ResourceType.Meat: return meatIconPrefab;
            case ResourceType.Crystal: return crystalIconPrefab;
        }

        return woodIconPrefab;
    }
}