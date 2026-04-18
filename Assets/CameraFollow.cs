using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.2f;
    public Vector2 lookAheadDistance = new Vector2(1.5f, 1f);

    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 velocity = Vector3.zero;
    private Vector3 lastTargetPosition;

    void Start()
    {
        lastTargetPosition = target.position;
    }

    void LateUpdate()
    {
        Vector3 moveDelta = target.position - lastTargetPosition;

        Vector3 lookAhead = new Vector3(
            moveDelta.x * lookAheadDistance.x,
            moveDelta.y * lookAheadDistance.y,
            0
        );

        Vector3 targetPosition = target.position + lookAhead;

        targetPosition.z = -10;

        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothTime
        );

        smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minBounds.x, maxBounds.x);
        smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minBounds.y, maxBounds.y);

        transform.position = smoothedPosition;

        lastTargetPosition = target.position;
    }
}