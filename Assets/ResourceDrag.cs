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
        if (Campfire.instance != null && Campfire.instance.PlayerInRange())
        {
            bool removed = Inventory.instance.Remove(icon.resourceType, 1);

            if (removed)
            {
                if (icon.resourceType == ResourceType.Wood)
                {
                    Campfire.instance.AddWood();
                }

                if (icon.resourceType == ResourceType.Flesh)
                {
                    Campfire.instance.CookMeat();
                }

                Destroy(gameObject);
                return;
            }
        }

        ReturnToSlot();
    }

    void ReturnToSlot()
    {
        transform.SetParent(startParent);
        transform.position = startPosition;
    }
}