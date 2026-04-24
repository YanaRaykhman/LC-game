using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector3 startPosition;
    Transform startParent;

    Canvas canvas;
    ResourceIcon icon;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        icon = GetComponent<ResourceIcon>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;

        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool used = false;

        // режим апгрейда
        if (CampfireUpgradeUI.instance != null && CampfireUpgradeUI.instance.IsOpen())
        {
            used = CampfireUpgradeUI.instance.TryAddResource(icon.resourceType);

            if (used)
            {
                Inventory.instance.Remove(icon.resourceType, 1);
            }
        }

        // обычный костёр
        else if (Campfire.instance != null && Campfire.instance.PlayerInRange())
        {
            if (icon.resourceType == ResourceType.Wood)
            {
                Campfire.instance.AddWood();
                Inventory.instance.Remove(ResourceType.Wood, 1);
                used = true;
            }

            if (icon.resourceType == ResourceType.Flesh)
            {
                Campfire.instance.CookMeat();
                Inventory.instance.Remove(ResourceType.Flesh, 1);
                used = true;
            }
        }

        if (used)
        {
            Destroy(gameObject);
        }
        else
        {
            ReturnToSlot();
        }
    }

    void ReturnToSlot()
    {
        transform.SetParent(startParent);
        transform.position = startPosition;
    }
}