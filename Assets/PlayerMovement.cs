using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float interactDistance = 1.3f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private GameObject targetObject;
    Tent currentTent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
{
    /*if (SleepSystem.instance.sleeping)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (currentTent != null)
            {
                currentTent.ExitTent(worldPos);
                currentTent = null;
            }
        }
        return;
    }*/

    if (Input.GetMouseButtonDown(0))
    {
        // Если клик по UI — игнорируем движение, но UI продолжает работать
        if (EventSystem.current.IsPointerOverGameObject())
            return;  
        
        if (SleepSystem.instance.sleeping)
        {
            Vector3 worldPos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos1 = new Vector2(worldPos1.x, worldPos1.y);

            if (currentTent != null)
            {
                currentTent.ExitTent();
                currentTent = null;
            }

            targetObject = null;
            targetPosition = clickPos1;

            return;
        }
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);

        RaycastHit2D hit = Physics2D.Raycast(clickPos, Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log("Clicked: " + hit.collider.name);

            if (
                hit.collider.CompareTag("Tree") ||
                hit.collider.CompareTag("Deer") ||
                hit.collider.CompareTag("Tent")
            )
            {
                targetObject = hit.collider.gameObject;
                targetPosition = targetObject.transform.position;
            }
            else
            {
                targetObject = null;
                targetPosition = clickPos;
            }
        }
        else
        {
            targetObject = null;
            targetPosition = clickPos;
        }
    }
}

    void FixedUpdate()
    {
        Vector2 direction = targetPosition - rb.position;

        if (direction.magnitude <= interactDistance)
        {
            rb.linearVelocity = Vector2.zero;

            if (targetObject != null)
            {
                TryChop();
            }

            return;
        }

        rb.linearVelocity = direction.normalized * speed;
    }

    void TryChop()
    {
        TreeResource tree = targetObject.GetComponent<TreeResource>();

        if (tree != null)
        {
            tree.Chop();
            targetObject = null;
            return;
        }

        AppleTree appleTree = targetObject.GetComponent<AppleTree>();

        if (appleTree != null)
        {
            appleTree.Chop();
            targetObject = null;
        }

        BerryBush berryBush = targetObject.GetComponent<BerryBush>();

        if (berryBush != null)
        {
            berryBush.Chop();
            targetObject = null;
        }

        Deer deer = targetObject.GetComponent<Deer>();

        if (deer != null)
        {
            deer.Hunt();
            targetObject = null;
        }

        Tent tent = targetObject.GetComponent<Tent>();

        if (tent != null)
        {
            currentTent = tent;
            tent.EnterTent();
            targetObject = null;
        }

    }

    public void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
        targetObject = null;
        targetPosition = rb.position;
    }

}