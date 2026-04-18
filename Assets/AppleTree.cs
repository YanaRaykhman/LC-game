using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public int hitsToChop = 3;

    int currentHits = 0;

    public int woodPerHit = 1;
    public int applesPerHit = 2;

    public void Chop()
    {
        bool woodAdded = Inventory.instance.Add(ResourceType.Wood, woodPerHit);
        bool applesAdded = Inventory.instance.Add(ResourceType.Apple, applesPerHit);

        if (!woodAdded && !applesAdded)
            return;

        currentHits++;

        if (currentHits >= hitsToChop)
        {
            Destroy(gameObject);
        }
    }
}