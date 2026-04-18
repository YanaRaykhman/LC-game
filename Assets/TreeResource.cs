using UnityEngine;

public class TreeResource : MonoBehaviour
{
    public int hitsToChop = 2;
    int currentHits = 0;

    public int woodPerHit = 2;

    public void Chop()
    {
        bool added = Inventory.instance.Add(ResourceType.Wood, woodPerHit);

        if (!added)
            return;

        currentHits++;

        if (currentHits >= hitsToChop)
        {
            Destroy(gameObject);
        }
    }
}