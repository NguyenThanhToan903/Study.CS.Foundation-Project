using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float slowSpeed = 2f;

    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private GameObject model;

    [SerializeField]
    private Transform modelTransform;

    [SerializeField]
    private float detectionRadius = 5f;

    [SerializeField]
    private Vector2 velocity;

    [SerializeField]
    private Boundary boundary;

    [SerializeField]
    private float timeSinceLastUpdate = 0f;

    [SerializeField]
    private Vector2 directionToCenter;


    Vector2 directionToPlayer = Vector2.zero;

    private void Start()
    {
        modelTransform = model != null ? model.transform : null;
        if (modelTransform == null)
        {
            Debug.LogError("Model object not found!");
        }

        SetRandomDirection();
        boundary.Radius = Mathf.Min(Mathf.Min(Mathf.Abs(boundary.PointA.x), Mathf.Abs(boundary.PointA.y)), Mathf.Min(Mathf.Abs(boundary.PointB.x), Mathf.Abs(boundary.PointB.y)));
        playerTransform = PlayerMovement.Instance.transform;

        directionToCenter = (Vector2)(boundary.Center - (Vector2)transform.position).normalized;
    }

    private void Update()
    {
        Move();
        SetSpriteScale();
    }

    public void SetSpriteScale()
    {
        if (velocity.x < 0)
        {
            modelTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (velocity.x > 0)
        {
            modelTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetBoundary(Boundary boundary)
    {
        this.boundary = boundary;
        directionToCenter = (Vector2)(boundary.Center - (Vector2)transform.position).normalized;
    }

    private void Move()
    {
        CheckBoundary();
    }

    private void CheckBoundary()
    {
        float distanceToCenter = Vector3.Distance(transform.position, boundary.Center);

        if (distanceToCenter >= boundary.Radius - 2f)
        {
            timeSinceLastUpdate += Time.fixedDeltaTime;
            if (timeSinceLastUpdate >= 2f)
            {
                SetRandomDirection();
                timeSinceLastUpdate = 0f;
            }

            if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
            {
                directionToPlayer = (Vector2)(transform.position - playerTransform.position).normalized;
                velocity = (directionToPlayer + velocity * 1.3f).normalized;
            }

            transform.position += (Vector3)(velocity * slowSpeed) * Time.deltaTime;
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
            {
                directionToPlayer = (Vector2)(transform.position - playerTransform.position).normalized;
                velocity = (directionToPlayer + velocity * 1.3f).normalized;
            }

            transform.position += (Vector3)(velocity * speed) * Time.deltaTime;
        }
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        velocity = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;
    }
    private void AvoidPlayer()
    {
        Vector2 directionToPlayer = (Vector2)(transform.position - playerTransform.position).normalized;

        Vector2 directionToCenter = ((Vector2)boundary.Center - (Vector2)transform.position).normalized;

        velocity = (directionToPlayer + directionToCenter).normalized;

        transform.position += (Vector3)(velocity * speed) * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)velocity);

        // Draw boundary square
        if (boundary != null)
        {
            Vector2 topLeft = new(boundary.PointA.x, boundary.PointB.y);
            Vector2 topRight = boundary.PointB;
            Vector2 bottomLeft = boundary.PointA;
            Vector2 bottomRight = new(boundary.PointB.x, boundary.PointA.y);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }

        // Draw line to boundary center
        if (boundary != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, boundary.Center);

            float leftAngle = Mathf.Atan2((boundary.Center - (Vector2)transform.position).y, (boundary.Center - (Vector2)transform.position).x) * Mathf.Rad2Deg - 45f;
            float rightAngle = Mathf.Atan2((boundary.Center - (Vector2)transform.position).y, (boundary.Center - (Vector2)transform.position).x) * Mathf.Rad2Deg + 45f;

            Vector2 leftDirection = new(Mathf.Cos(leftAngle * Mathf.Deg2Rad), Mathf.Sin(leftAngle * Mathf.Deg2Rad));
            Vector2 rightDirection = new(Mathf.Cos(rightAngle * Mathf.Deg2Rad), Mathf.Sin(rightAngle * Mathf.Deg2Rad));

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * 2f);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * 2f);
        }

        // Draw detection radius around rabbit
        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw target position and path
        Gizmos.color = Color.magenta;
        Vector2 targetPosition = (Vector2)transform.position + velocity * 2f; // Adjust 2f based on range
        Gizmos.DrawSphere(targetPosition, 0.2f);  // Mark target position with sphere
        Gizmos.DrawLine(transform.position, targetPosition); // Path line to target
    }


}
