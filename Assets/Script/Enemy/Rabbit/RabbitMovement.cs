using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private ListRabbitVariable rabbits;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float visionAngle = 270f;
    public float Velocity { get; private set; }

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rabbits != null && !rabbits.rabbitMovement.Contains(this))
        {
            rabbits.rabbitMovement.Add(this);
        }
    }

    private void FixedUpdate()
    {
        Vector2 target = new Vector2(5, 5);
        MoveTowards(target, speed);
        RandomTarget();
    }

    private void OnDestroy()
    {
        if (rabbits != null)
        {
            rabbits.rabbitMovement.Remove(this);
        }
    }

    public void MoveTowards(Vector2 targetPosition, float moveSpeed)
    {
        if (rb != null)
        {
            Vector2 direction = (targetPosition - rb.position).normalized;

            rb.velocity = direction * moveSpeed;
        }
    }

    public Vector2 RandomTarget()

    {
        float halfVisionAngle = visionAngle * 0.5f;
        Vector2 normalizedVelocity = rb.velocity.normalized;
        Vector2 leftDirection = Quaternion.Euler(0, 0, -halfVisionAngle) * normalizedVelocity;
        Vector2 rightDirection = Quaternion.Euler(0, 0, halfVisionAngle) * normalizedVelocity;

        float leftPoint = Vector2.Dot(leftDirection, normalizedVelocity);
        float rightPoint = Vector2.Dot(rightDirection, normalizedVelocity);
        Debug.Log("Left" + leftPoint + "Right" + rightPoint);
        float random = Random.Range(leftDirection.x, rightDirection.x);
        Debug.Log("Random" + random);
        Vector2 newDirection = new Vector2(random, transform.position.y);
        Debug.Log(newDirection);
        return newDirection;
    }


    private void OnDrawGizmos()
    {
        Vector2 normalizedVelocity = rb.velocity.normalized;

        float halfVisionAngle = visionAngle * 0.5f;

        Vector2 leftDirection = Quaternion.Euler(0, 0, -halfVisionAngle) * normalizedVelocity;
        Vector2 rightDirection = Quaternion.Euler(0, 0, halfVisionAngle) * normalizedVelocity;

        float leftPoint = Vector2.Dot(leftDirection, normalizedVelocity);
        float rightPoint = Vector2.Dot(rightDirection, normalizedVelocity);
        Debug.Log("Left" + leftPoint + "Right" + rightPoint);
        float random = Random.Range(leftDirection.x, rightDirection.x);
        Vector2 newDirection = new Vector2(random, transform.position.y);

        Debug.Log("Random" + random);

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)newDirection * 5f);


        Gizmos.DrawLine(new(0, 0, 0), (Vector3)leftDirection);

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * 1f);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * 1f);

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)normalizedVelocity * 5f);

    }

}

