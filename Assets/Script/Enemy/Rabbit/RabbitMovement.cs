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
                directionToCenter = (boundary.Center - (Vector2)transform.position).normalized;

                float leftAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg - 45f;
                float rightAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg + 45f;
                float randomAngle = Random.Range(leftAngle, rightAngle);

                Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;
                velocity = randomDirection;
                timeSinceLastUpdate = 0f;
            }

            if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
            {
                directionToPlayer = (Vector2)(transform.position - playerTransform.position).normalized;
                velocity = (directionToPlayer + (directionToCenter * 0.7f) + velocity * 1.3f).normalized;
            }

            transform.position += (Vector3)(velocity * slowSpeed) * Time.deltaTime;
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
            {
                directionToPlayer = (Vector2)(transform.position - playerTransform.position).normalized;
                Vector2 directionToCenter = ((Vector2)boundary.Center - (Vector2)transform.position).normalized;

                velocity = (directionToPlayer + (directionToCenter * 0.7f) + velocity * 1.3f).normalized;
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

        if (boundary != null)
        {
            Vector2 center = boundary.Center;
            float halfBoundarySize = boundary.Radius;

            Vector2 topLeft = new(center.x - halfBoundarySize, center.y + halfBoundarySize);
            Vector2 topRight = new(center.x + halfBoundarySize, center.y + halfBoundarySize);
            Vector2 bottomLeft = new(center.x - halfBoundarySize, center.y - halfBoundarySize);
            Vector2 bottomRight = new(center.x + halfBoundarySize, center.y - halfBoundarySize);

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, bottomRight);
            Gizmos.DrawLine(bottomRight, bottomLeft);
            Gizmos.DrawLine(bottomLeft, topLeft);
        }

        if (boundary != null)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position, boundary.Center);

            float leftAngle = Mathf.Atan2((boundary.Center - (Vector2)transform.position).y, (boundary.Center - (Vector2)transform.position).x) * Mathf.Rad2Deg - 45f;
            float rightAngle = Mathf.Atan2((boundary.Center - (Vector2)transform.position).y, (boundary.Center - (Vector2)transform.position).x) * Mathf.Rad2Deg + 45f;

            Vector2 leftDirection = new Vector2(Mathf.Cos(leftAngle * Mathf.Deg2Rad), Mathf.Sin(leftAngle * Mathf.Deg2Rad));
            Vector2 rightDirection = new Vector2(Mathf.Cos(rightAngle * Mathf.Deg2Rad), Mathf.Sin(rightAngle * Mathf.Deg2Rad));

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * 2f);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * 2f);
        }

        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
