using UnityEngine;

public class RabbitPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Boundary boundary;
    private Vector2 targetPosition; // Store target position

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 nextPosition = rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime);

        // Restrict movement within the boundary
        nextPosition.x = Mathf.Clamp(nextPosition.x, boundary.PointA.x, boundary.PointB.x);
        nextPosition.y = Mathf.Clamp(nextPosition.y, boundary.PointA.y, boundary.PointB.y);

        rb.MovePosition(nextPosition);
    }

    public void MoveTo(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        moveDir = (targetPosition - rb.position).normalized;
    }

    public void StopMovement()
    {
        moveDir = Vector2.zero;
    }

    public void SetBoundary(Boundary boundary)
    {
        this.boundary = boundary;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(targetPosition, 0.2f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, targetPosition);

        if (boundary != null)
        {
            boundary.OnDrawGizmos();
        }
    }
}
