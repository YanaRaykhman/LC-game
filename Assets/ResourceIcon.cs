using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceIcon : MonoBehaviour, IPointerClickHandler
{
    public ResourceType resourceType;

    float lastClickTime;
    float doubleClickDelay = 0.3f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Time.time - lastClickTime < doubleClickDelay)
        {
            TryEat();
        }

        lastClickTime = Time.time;
    }

    void TryEat()
    {
        if (resourceType == ResourceType.Apple ||
            resourceType == ResourceType.Berry ||
            resourceType == ResourceType.Meat)
        {
            bool removed = Inventory.instance.Remove(resourceType, 1);

            if (removed)
            {
                HungerSystem.instance.Eat(resourceType);
                Destroy(gameObject);
            }
        }
    }
}