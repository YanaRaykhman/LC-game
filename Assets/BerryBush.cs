using UnityEngine;

public class BerryBush : MonoBehaviour
{
    public int hitsToChop = 1;

    int currentHits = 0;
    
    public int berriesPerHit = 4;

    bool active = true;

    public void Chop()
    {

        if(!active)
            return;

        bool berriesAdded = Inventory.instance.Add(ResourceType.Berry, berriesPerHit);
        
        if (!berriesAdded)
            return;

        currentHits++;

        if (currentHits >= hitsToChop){
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
        Debug.Log("Resources disabled");
    }

    void EnableResource()
    {
        active = true;
    }
}