using UnityEngine;

public class BerryBush : MonoBehaviour
{
    public int hitsToChop = 1;

    int currentHits = 0;
    
    public int berriesPerHit = 4;

    public void Chop()
    {
        bool berriesAdded = Inventory.instance.Add(ResourceType.Berry, berriesPerHit);
        
        if (!berriesAdded)
            return;

        currentHits++;

        if (currentHits >= hitsToChop){
            Destroy(gameObject);
        }
    }
}