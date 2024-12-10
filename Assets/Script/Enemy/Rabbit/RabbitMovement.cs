using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField]
    public float speed;

    [SerializeField]
    private float sideRayAngle = 40f;

    [SerializeField]
    private float distanceAvoid = 1f;

    [SerializeField]
    private float wallDetectionDistance = 3f;

    private float waitTimer;

    private float waitDuration;

    private bool isWaiting = false;

    private Vector2 Velocity;
    private Vector2 movementDirection;

    private void Start()
    {
        SetRandomDirection();
    }

    private void Update()
    {
        Velocity = (speed * Time.deltaTime * movementDirection);
        this.transform.position += (Vector3)Velocity;

        if (Velocity.x > 0)
        {
            this.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
        }
        else
        {
            this.transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        }

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitDuration)
            {
                isWaiting = false;
                waitTimer = 0f;

                SetRandomDirection();
            }
        }
        else
        {
            AvoidWallsAndRabbits();

            ResolveStuckSituation();
        }

    }


    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    private void AvoidWallsAndRabbits()
    {
        bool nearWall = CastRay(transform.position, movementDirection, wallDetectionDistance);

        if (nearWall)
        {
            AdjustDirectionToAvoidWall();
        }
        else
        {
            AvoidOtherRabbits();
        }
    }

    private void AdjustDirectionToAvoidWall()
    {
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, movementDirection, wallDetectionDistance);

        if (hit.collider != null)
        {
            Vector2 wallNormal = hit.normal;
            Vector2 reflectedDirection = Vector2.Reflect(movementDirection, wallNormal);

            float randomAngle = Random.Range(-30f, 30f);
            Vector2 adjustedDirection = RotateVector(reflectedDirection, randomAngle);

            Vector2 finalDirection = CombineAvoidance(adjustedDirection);

            movementDirection = finalDirection.normalized;

            Debug.DrawLine(hit.point, hit.point + wallNormal, Color.red, 1f);
            Debug.DrawLine(hit.point, hit.point + adjustedDirection, Color.green, 1f);
            Debug.DrawLine(hit.point, hit.point + reflectedDirection, Color.yellow, 1f);
        }
    }

    private Vector2 CombineAvoidance(Vector2 baseDirection)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, distanceAvoid);
        Vector2 avoidanceVector = Vector2.zero;

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            if (hit.CompareTag("Rabbit"))
            {
                Vector2 otherRabbitPosition = hit.transform.position;
                Vector2 selfPosition = transform.position;

                Vector2 avoidVector = (selfPosition - otherRabbitPosition).normalized;
                avoidanceVector += avoidVector;
            }
        }

        Vector2 combinedDirection = (baseDirection + avoidanceVector * 0.5f).normalized;
        return combinedDirection;
    }

    private void AvoidOtherRabbits()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, distanceAvoid);
        Vector2 mainAvoidVector = Vector2.zero;

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            if (hit.CompareTag("Rabbit"))
            {
                Vector2 otherRabbitPosition = hit.transform.position;
                Vector2 selfPosition = transform.position;

                Vector2 avoidVector = (selfPosition - otherRabbitPosition).normalized;
<<<<<<< HEAD
                mainAvoidVector += avoidVector;
=======
                accumulatedAvoidVector += avoidVector;
>>>>>>> da0bf1fbf8a0b16594e703b3a108bd0baebbc87f
            }
        }

        if (hits.Length > 1)
        {
<<<<<<< HEAD
            movementDirection = ClampDirection(movementDirection, mainAvoidVector.normalized, 60f);
=======
            movementDirection = ClampDirection(movementDirection, accumulatedAvoidVector.normalized, 60f);
>>>>>>> da0bf1fbf8a0b16594e703b3a108bd0baebbc87f
        }
        else if (mainAvoidVector != Vector2.zero)
        {
<<<<<<< HEAD
            movementDirection = ClampDirection(movementDirection, mainAvoidVector.normalized, 30f);
=======
            movementDirection = ClampDirection(movementDirection, accumulatedAvoidVector.normalized, 30f);
>>>>>>> da0bf1fbf8a0b16594e703b3a108bd0baebbc87f
        }
    }

    private Vector2 ClampDirection(Vector2 currentDirection, Vector2 targetDirection, float maxAngle)
    {
        float angle = Vector2.SignedAngle(currentDirection, targetDirection);

        if (angle > maxAngle)
            angle = maxAngle;
        else if (angle < -maxAngle)
            angle = -maxAngle;

        return RotateVector(currentDirection, angle).normalized;
    }

    private void ResolveStuckSituation()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, distanceAvoid);
        bool isNearWall = Physics2D.Raycast(transform.position, movementDirection, wallDetectionDistance).collider != null;

        if (hits.Length > 1 && isNearWall)
        {
            float randomAngle = Random.Range(90f, 180f);
            Vector2 escapeDirection = RotateVector(movementDirection, randomAngle).normalized;

            if (!CastRay(transform.position, escapeDirection, wallDetectionDistance))
            {
                movementDirection = escapeDirection;
            }
            else
            {
                randomAngle = Random.Range(-30f, 30f);
                movementDirection = RotateVector(movementDirection, randomAngle).normalized;
            }

            isWaiting = true;
            waitDuration = Random.Range(0.5f, 1f);
        }
    }

    private bool CastRay(Vector2 origin, Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            Debug.DrawLine(origin, origin + direction * distance, Color.red);
            return true;
        }
        Debug.DrawLine(origin, origin + direction * distance, Color.green);
        return false;
    }

    private Vector2 RotateVector(Vector2 vector, float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        );
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Vector2 currentPosition = transform.position;

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(currentPosition, movementDirection * wallDetectionDistance);

            Vector2 leftDirection = RotateVector(movementDirection, -sideRayAngle);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(currentPosition, leftDirection * distanceAvoid);

            Vector2 rightDirection = RotateVector(movementDirection, sideRayAngle);
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(currentPosition, rightDirection * distanceAvoid);

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(currentPosition, distanceAvoid);
        }
    }
}
