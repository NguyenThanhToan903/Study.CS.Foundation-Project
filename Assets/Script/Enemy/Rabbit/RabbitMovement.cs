using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distanceAvoid = 1f; // Bán kính phát hiện thỏ khác
    [SerializeField] private float distancePlayerAvoid = 2f; // Bán kính phát hiện thỏ khác
    [SerializeField] private float wallDetectionDistance = 3f;
    [SerializeField] private float sideRayAngle = 40f; // Góc lệch của hai tia bên

    private Vector2 movementDirection;
    private float waitTimer = 0f;
    private float waitDuration = 0f;
    private bool isWaiting = false;
    private bool wallDetected = false;

    private void Start()
    {
        SetRandomDirection();
    }

    private void Update()
    {
        // Di chuyển thỏ
        this.transform.position += (Vector3)(speed * Time.deltaTime * movementDirection);

        if (isWaiting)
        {
            // Tăng bộ đếm thời gian khi đang chờ đổi hướng
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitDuration)
            {
                // Khi hết thời gian chờ, đổi hướng và reset cờ
                isWaiting = false;
                waitTimer = 0f;
                wallDetected = false;

                Debug.Log("Changing direction after waiting...");
                SetRandomDirection();
            }
        }
        else
        {
            // Kiểm tra va chạm với tường nếu không trong thời gian chờ
            CheckWallCollision();

            // Kiểm tra va chạm với thỏ khác
            CheckRabbitCollision();

            //
            //CheckPlayerCollision();
        }
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    private void CheckWallCollision()
    {
        Vector2 currentPosition = transform.position;

        if (CastRay(currentPosition, movementDirection, wallDetectionDistance))
        {
            StartWaiting();
            return;
        }

        // Kiểm tra va chạm tia bên trái
        Vector2 leftDirection = RotateVector(movementDirection, -sideRayAngle);
        if (CastRay(currentPosition, leftDirection, distanceAvoid))
        {
            StartWaiting();
            return;
        }

        // Kiểm tra va chạm tia bên phải
        Vector2 rightDirection = RotateVector(movementDirection, sideRayAngle);
        if (CastRay(currentPosition, rightDirection, distanceAvoid))
        {
            StartWaiting();
            return;
        }
    }


            if (Mathf.Abs(currentAngle) > halfAngle) // Tia ở cạnh
            {
                adjustedDistance = Mathf.Lerp(wallDetectionDistance, distanceAvoid, Mathf.Abs(currentAngle) / halfAngle);
            }

            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, adjustedDistance);

            if (hit.collider != null && hit.collider.CompareTag("Wall"))
            {
                movementDirection = -movementDirection;
                break;

            }
        }
    }



    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Vector2 currentPosition = transform.position;

            // Tia giữa
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(currentPosition, movementDirection * wallDetectionDistance);

            // Tia bên trái
            Vector2 leftDirection = RotateVector(movementDirection, -sideRayAngle);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(currentPosition, leftDirection * distanceAvoid);

            // Tia bên phải
            Vector2 rightDirection = RotateVector(movementDirection, sideRayAngle);
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(currentPosition, rightDirection * distanceAvoid);

            // Vẽ bán kính va chạm với thỏ khác
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(currentPosition, distanceAvoid);
        }
    }
}
