using UnityEngine;

public class RabbitPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Boundary boundary;

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
        moveDir = targetPosition;
    }
    public void SetBoundary(Boundary boundary)
    {
        this.boundary = boundary;
    }
}
