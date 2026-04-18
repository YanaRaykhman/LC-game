using UnityEngine;

public class TreeResource : MonoBehaviour
{
    public int hitsToChop = 2;
    int currentHits = 0;

    bool active = true;

    public int woodPerHit = 2;

    public void Chop()
    {
        if(!active)
        {
            return;
        }

        bool added = Inventory.instance.Add(ResourceType.Wood, woodPerHit);

        if (!added)
            return;

        currentHits++;

        if (currentHits >= hitsToChop)
        {
            Destroy(gameObject);
        }
        
    }

    void OnEnable()
    {
        DayNightEvents.OnNightStart += DisableResource;
        DayNightEvents.OnDayStart += EnableResource;
    }

    void OnDisable()
    {
        DayNightEvents.OnNightStart -= DisableResource;
        DayNightEvents.OnDayStart -= EnableResource;
    }

    void DisableResource()
    {
        active = false;
    }

    void EnableResource()
    {
        active = true;
    }
}