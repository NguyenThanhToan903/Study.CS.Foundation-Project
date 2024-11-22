using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distanceAvoid = 1f;
    [SerializeField] private float distancePlayerAvoid = 2f;
    [SerializeField] private float wallDetectionDistance = 3f;
    [SerializeField] private float sideRayAngle = 40f;

    private Vector2 movementDirection;
    private float waitTimer;
    private float waitDuration;
    private bool isWaiting = false;

    private void Start()
    {
        SetRandomDirection();
    }

    private void Update()
    {
        this.transform.position += (Vector3)(speed * Time.deltaTime * movementDirection);

        if (isWaiting)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitDuration)
            {
                isWaiting = false;
                waitTimer = 0f;

                Debug.Log("Changing direction after waiting...");
                SetRandomDirection();
            }
        }
        else
        {
            AvoidWalls();

            AvoidOtherRabbits();
        }
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    private void AvoidWalls()
    {
        Vector2 currentPosition = transform.position;

        if (CastRay(currentPosition, movementDirection, wallDetectionDistance))
        {
            AdjustDirectionToAvoidWall();
            return;
        }

        Vector2 leftDirection = RotateVector(movementDirection, -sideRayAngle);
        if (CastRay(currentPosition, leftDirection, distanceAvoid))
        {
            AdjustDirectionToAvoidWall();
            return;
        }

        Vector2 rightDirection = RotateVector(movementDirection, sideRayAngle);
        if (CastRay(currentPosition, rightDirection, distanceAvoid))
        {
            AdjustDirectionToAvoidWall();
            return;
        }
    }

    private void AdjustDirectionToAvoidWall()
    {
        // Phát tia ray từ vị trí hiện tại theo hướng di chuyển
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, movementDirection, wallDetectionDistance);

        if (hit.collider != null)
        {
            // Vector pháp tuyến tại điểm va chạm
            Vector2 normal = hit.normal;

            // Tính hướng mới bằng cách cộng hướng hiện tại với vector pháp tuyến
            movementDirection = (movementDirection + normal).normalized;

            Debug.Log($"Wall detected, adjusting direction. New Direction = {movementDirection}");

            Debug.DrawLine(hit.point, hit.point + normal, Color.red, 1f);
            Debug.DrawLine(hit.point, hit.point + movementDirection, Color.green, 1f); // Hướng mới
        }
    }

    //private void AdjustDirectionToAvoidWall()
    //{
    //    // Phát tia ray từ vị trí hiện tại theo hướng di chuyển
    //    Vector2 origin = transform.position;
    //    RaycastHit2D hit = Physics2D.Raycast(origin, movementDirection, wallDetectionDistance);

    //    if (hit.collider != null)
    //    {
    //        // Vector pháp tuyến tại điểm va chạm
    //        Vector2 normal = hit.normal;

    //        // Tính vector phản chiếu
    //        Vector2 reflectedDirection = Vector2.Reflect(movementDirection, normal);

    //        // Kết hợp hướng di chuyển hiện tại với vector phản chiếu
    //        movementDirection = (movementDirection + reflectedDirection).normalized;

    //        Debug.Log($"Wall detected, adjusting direction. New Direction = {movementDirection}");

    //        // Debug: Vẽ vector pháp tuyến và hướng di chuyển mới
    //        Debug.DrawLine(hit.point, hit.point + normal, Color.red, 1f); // Pháp tuyến
    //        Debug.DrawLine(hit.point, hit.point + reflectedDirection, Color.blue, 1f); // Hướng phản chiếu
    //        Debug.DrawLine(hit.point, hit.point + movementDirection, Color.green, 1f); // Hướng mới
    //    }
    //}




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

    private void AvoidOtherRabbits()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, distanceAvoid);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == this.gameObject) continue;

            if (hit.CompareTag("Rabbit"))
            {
                Vector2 otherRabbitPosition = hit.transform.position;
                Vector2 selfPosition = transform.position;

                Vector2 avoidVector = (selfPosition - otherRabbitPosition).normalized;
                movementDirection = (movementDirection + avoidVector).normalized;

                Debug.Log("Avoiding other rabbit...");
            }
        }
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
