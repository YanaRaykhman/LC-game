using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float interactDistance = 1.3f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private GameObject targetObject;

    private Tent currentTent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleClick();
        }
    }

    void HandleClick()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);

        if (SleepSystem.instance.sleeping)
        {
            HandleSleepClick(clickPos);
            return;
        }

        HandleNormalClick(clickPos);
    }

    void HandleSleepClick(Vector2 clickPos)
    {
        if (currentTent != null)
        {
            currentTent.ExitTent();
            currentTent = null;
        }

        targetObject = null;
        targetPosition = clickPos;
    }

    void HandleNormalClick(Vector2 clickPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Clicked: " + hit.collider.name);

            if (IsInteractable(hit.collider))
            {
                targetObject = hit.collider.gameObject;
                targetPosition = targetObject.transform.position;
                return;
            }
        }

        targetObject = null;
        targetPosition = clickPos;
    }

    bool IsInteractable(Collider2D col)
    {
        return col.CompareTag("Tree") ||
               col.CompareTag("Deer") ||
               col.CompareTag("Tent") ||
               col.CompareTag("BerryBush") ||
               col.CompareTag("AppleTree");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 direction = targetPosition - rb.position;

        if (direction.magnitude <= interactDistance)
        {
            rb.linearVelocity = Vector2.zero;

            if (targetObject != null)
            {
                TryInteract();
            }

            return;
        }

        rb.linearVelocity = direction.normalized * speed;
    }

    void TryInteract()
    {
        if (targetObject == null) return;

        if (targetObject.TryGetComponent(out TreeResource tree))
        {
            tree.Chop();
        }
        else if (targetObject.TryGetComponent(out AppleTree appleTree))
        {
            appleTree.Chop();
        }
        else if (targetObject.TryGetComponent(out BerryBush berryBush))
        {
            berryBush.Chop();
        }
        else if (targetObject.TryGetComponent(out Deer deer))
        {
            deer.Hunt();
        }
        else if (targetObject.TryGetComponent(out Tent tent))
        {
            currentTent = tent;
            tent.EnterTent();
        }

        targetObject = null;
    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
        targetObject = null;
        targetPosition = rb.position;
    }
}