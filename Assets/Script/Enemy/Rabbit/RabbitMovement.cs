using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    [SerializeField] private float slowSpeed = 2f;

    [SerializeField] private GameObject player;

    [SerializeField] private Transform playerTransform;

    [SerializeField] private GameObject model;

    [SerializeField] private Transform modelTransform;

    [SerializeField] private float detectionRadius = 2f;

    [SerializeField] private float visionAngle = 270f;

    [SerializeField] private Boundary boundary;

    [SerializeField] private float timeSinceLastUpdate = 0f;

    [SerializeField] private Vector2 directionToCenter;

    [SerializeField] private ListRabbitVariable rabbits;

    [SerializeField] private float moveDuration;

    [SerializeField] private float stopDuration;

    //Vector2 directionToPlayer = Vector2.zero;

    public Vector3 Velocity { private set; get; }

    private Vector2 currentDirection;

    private float stateTimer;

    private bool isMoving = true;

    private void Start()
    {
        modelTransform = model != null ? transform.Find("Model") : null;
        playerTransform = player != null ? player.GetComponent<Transform>() : null;

        if (modelTransform == null)
        {
            Debug.LogError("Model object not found!");
        }

        currentDirection = GetRandomDirection();
        stateTimer = moveDuration;
    }

    private void Update()
    {
        SetSpriteScale();
    }

    private void FixedUpdate()
    {
        stateTimer -= Time.fixedDeltaTime;

        Vector3 nextPosition = transform.position + (Vector3)(currentDirection * speed * Time.fixedDeltaTime);
        Debug.Log("IsWithinBoundary: " + IsWithinBoundary(nextPosition));

        if (!IsWithinBoundary(nextPosition))
        {
            currentDirection = GetDirectionToCenter();
            Velocity = currentDirection * speed;
        }

        if (stateTimer <= 0 && isMoving)
        {
            isMoving = false;
            stateTimer = GetRandomTime(); // Reset for stop phase
            Velocity = Vector3.zero;
        }
        else if (stateTimer <= 0 && !isMoving)
        {
            isMoving = true;
            stateTimer = GetRandomTime(); // Reset for move phase
            currentDirection = GetRandomDirection();
        }

    }

    private Vector2 GetDirectionToCenter()
    {
        if (boundary == null) return Vector2.zero;

        Vector2 center = new Vector2(
            (boundary.PointA.x + boundary.PointB.x) / 2,
            (boundary.PointA.y + boundary.PointB.y) / 2
        );
        Debug.Log(center);
        return (center - (Vector2)transform.position).normalized;
    }

    private bool IsWithinBoundary(Vector3 position)
    {
        if (boundary == null) return true;
        Debug.Log("IsWithinBoundary");
        return position.x >= boundary.PointA.x && position.x <= boundary.PointB.x &&
               position.y >= boundary.PointA.y && position.y <= boundary.PointB.y;
    }

    private float GetRandomTime()
    {
        return Random.Range(1f, 3f);
    }

    private Vector2 GetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);
        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
        return randomDirection == Vector2.zero ? Vector2.up : randomDirection; // Avoid zero vector
    }


    private Vector2 CalculateVelocity()
    {
        var rabbitsInRange = RabbitsInRange();

        Vector2 velocity = ((Vector2)transform.forward).normalized * speed;

        return velocity;
    }

    public void SetSpriteScale()
    {
        if (Velocity.x < 0)
        {
            modelTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Velocity.x > 0)
        {
            modelTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    private List<RabbitMovement> RabbitsInRange()
    {
        var listRabbit = rabbits.rabbitMovement.FindAll(rabbit => rabbit != this && (rabbit.transform.position - transform.position).magnitude <= detectionRadius && InVisionCone(rabbit.transform.position));
        return listRabbit;
    }

    private bool InVisionCone(Vector2 position)
    {
        Vector2 directionToPosition = position - (Vector2)transform.position;
        float dotProduct = Vector2.Dot(transform.forward, directionToPosition);
        float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);
        return dotProduct >= cosHalfVisionAngle;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)Velocity);

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

        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }

        Gizmos.color = Color.red;

        Gizmos.color = Color.red;

        Vector2 normalizedVelocity = Velocity.normalized;

        float halfVisionAngle = visionAngle * 0.5f;

        Vector2 leftDirection = Quaternion.Euler(0, 0, -halfVisionAngle) * normalizedVelocity;
        Vector2 rightDirection = Quaternion.Euler(0, 0, halfVisionAngle) * normalizedVelocity;

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * detectionRadius);

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)normalizedVelocity * detectionRadius);
    }
}
