using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public int maxSlots = 20;

    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    public System.Action OnInventoryChanged;

    void Awake()
    {
        instance = this;

        foreach (ResourceType type in System.Enum.GetValues(typeof(ResourceType)))
        {
            resources[type] = 0;
        }
    }

    public int Get(ResourceType type)
    {
        return resources[type];
    }

    public int GetTotal()
    {
        int total = 0;

        foreach (var r in resources)
        {
            total += r.Value;
        }

        return total;
    }

    public bool CanAdd(ResourceType type, int amount)
    {
        return GetTotal() + amount <= maxSlots;
    }

    public bool Add(ResourceType type, int amount)
    {
        if (!CanAdd(type, amount))
            return false;

        resources[type] += amount;

        OnInventoryChanged?.Invoke();

        return true;
    }

    public bool Remove(ResourceType type, int amount)
    {
        if (resources[type] < amount)
            return false;

        resources[type] -= amount;

        OnInventoryChanged?.Invoke();

        return true;
    }
}