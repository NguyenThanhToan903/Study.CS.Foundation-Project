//using UnityEngine;
//public class RabbitMovement : MonoBehaviour
//{
//    [SerializeField] private float speed = 2f;
//    [SerializeField] private int numberOfRay = 2;
//    [SerializeField] private float angle = 270f;
//    [SerializeField] private float distanceAvoid = 3f;
//    [SerializeField] private float wallDetectionDistance = 3f;

//    //List Rabbit Variable
//    [SerializeField] private ListRabbitVariable rabbits;

//    private Vector2 movementDirection;

//    private void Start()
//    {
//        SetRandomDirection();
//    }

//    private void Update()
//    {
//        this.transform.position += (Vector3)(speed * Time.deltaTime * movementDirection);
//        Debug.Log(this.transform.position);
//        CheckWallCollision();
//    }

//    private void SetRandomDirection()
//    {
//        float randomAngle = Random.Range(0f, 360f);
//        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
//    }

//    private void CheckWallCollision()
//    {
//        Vector2 currentPosition = transform.position;
//        for (int i = 0; i < numberOfRay; i++)
//        {
//            float halfAngle = angle / 2;
//            float currentAngle = ((i / (float)(numberOfRay - 1)) * angle) - halfAngle;
//            float baseAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
//            float finalAngle = baseAngle + currentAngle;

//            float radians = finalAngle * Mathf.Deg2Rad;
//            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
//            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, wallDetectionDistance - Mathf.Abs(i - wallDetectionDistance));

//            if (hit.collider != null && hit.collider.CompareTag("Wall")) // Assumes walls have the "Wall" tag
//            {
//                movementDirection = -movementDirection;
//                break; // Exit loop if collision detected
//            }
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        if (Application.isPlaying)
//        {
//            // Draw the main movement direction ray
//            Gizmos.color = Color.red;
//            Vector2 currentPosition = transform.position;
//            Gizmos.DrawLine(currentPosition, currentPosition + movementDirection * wallDetectionDistance);

//            float halfAngle = angle / 2;

//            for (int i = 0; i < numberOfRay; i++)
//            {
//                Gizmos.color = Color.yellow;

//                // Calculate the angle for the ray in the range [-halfAngle, halfAngle]
//                float currentAngle = ((i / (float)(numberOfRay - 1)) * angle) - halfAngle;
//                float baseAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
//                float finalAngle = baseAngle + currentAngle;

//                // Calculate the ray's direction
//                float radians = finalAngle * Mathf.Deg2Rad;
//                Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

//                // Adjust the ray's length based on the angle
//                float adjustedDistance = Mathf.Lerp(wallDetectionDistance, distanceAvoid, Mathf.Abs(currentAngle) / halfAngle);

//                // Draw the rays for wall collision detection
//                Gizmos.DrawRay(currentPosition, direction * adjustedDistance);

//                // Visualize the detection distance for the rays
//                RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, adjustedDistance);
//                if (hit.collider != null && hit.collider.CompareTag("Wall"))
//                {
//                    // If the ray hits a wall, draw the hit point
//                    Gizmos.color = Color.green;
//                    Gizmos.DrawSphere(hit.point, 0.1f); // Draw a sphere at the hit point
//                }
//            }
//        }
//    }



//}


using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int numberOfRay = 5;  // Tăng số lượng tia nếu cần
    [SerializeField] private float angle = 270f;
    [SerializeField] private float distanceAvoid = 3f;
    [SerializeField] private float wallDetectionDistance = 3f;

    //List Rabbit Variable
    [SerializeField] private ListRabbitVariable rabbits;

    private Vector2 movementDirection;
    private float timeSinceLastCollisionCheck = 0f; // Timer to track the time since the last collision check
    private float collisionCheckInterval = 2f; // Time interval for collision check

    private void Start()
    {
        SetRandomDirection();
    }

    private void Update()
    {
        this.transform.position += (Vector3)(speed * Time.deltaTime * movementDirection);
        Debug.Log(this.transform.position);

        // Update the timer
        timeSinceLastCollisionCheck += Time.deltaTime;

        // Check for collisions if the interval has passed
        if (timeSinceLastCollisionCheck >= collisionCheckInterval)
        {
            CheckWallCollision();
            timeSinceLastCollisionCheck = 0f; // Reset the timer after checking for collisions
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

        for (int i = 0; i < numberOfRay; i++)
        {
            float halfAngle = angle / 2;
            float currentAngle = ((i / (float)(numberOfRay - 1)) * angle) - halfAngle;
            float baseAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            float finalAngle = baseAngle + currentAngle;

            float radians = finalAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            // Tính khoảng cách tia theo dải từ ngắn nhất đến dài nhất
            // Sử dụng Mathf.Lerp để tạo dải khoảng cách từ wallDetectionDistance đến distanceAvoid
            float adjustedDistance = Mathf.Lerp(wallDetectionDistance, distanceAvoid, Mathf.Abs(currentAngle) / halfAngle);

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
            Gizmos.color = Color.red;
            Vector2 currentPosition = transform.position;
            Gizmos.DrawLine(currentPosition, currentPosition + movementDirection * wallDetectionDistance);

            float halfAngle = angle / 2;

            for (int i = 0; i < numberOfRay; i++)
            {
                Gizmos.color = Color.yellow;

                // Tính góc cho tia trong phạm vi [-halfAngle, halfAngle]
                float currentAngle = ((i / (float)(numberOfRay - 1)) * angle) - halfAngle;
                float baseAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                float finalAngle = baseAngle + currentAngle;

                // Tính hướng của tia
                float radians = finalAngle * Mathf.Deg2Rad;
                Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

                // Tính khoảng cách tia thay đổi từ ngắn nhất đến dài nhất
                float adjustedDistance = Mathf.Lerp(wallDetectionDistance, distanceAvoid, Mathf.Abs(currentAngle) / halfAngle);

                // Vẽ các tia va chạm
                Gizmos.DrawRay(currentPosition, direction * adjustedDistance);

                // Kiểm tra va chạm và vẽ điểm va chạm nếu có
                RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, adjustedDistance);
                if (hit.collider != null && hit.collider.CompareTag("Wall"))
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawSphere(hit.point, 0.1f); // Vẽ một hình cầu tại điểm va chạm
                }
            }
        }
    }
}

