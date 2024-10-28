using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float slowSpeed = 2f; // Tốc độ khi gần biên

    private Transform playerTransform;
    Vector2 directionToPlayer = Vector2.zero;
    [SerializeField]
    private GameObject model;

    private Transform modelTransform;

    [SerializeField]
    private float detectionRadius = 5f;

    [SerializeField]
    private Vector2 velocity;

    private Boundary boundary;

    private float randomDirectionTimer = 0f; // Thời gian để tạo hướng ngẫu nhiên
    private float randomDirectionInterval = 2f; // Thời gian giữa các lần tạo hướng ngẫu nhiên

    private Vector2 directionToCenter; // Vector hướng vào tâm

    private void Start()
    {
        modelTransform = model != null ? model.transform : null;
        if (modelTransform == null)
        {
            Debug.LogError("Model object not found!");
        }
        SetRandomDirection();
        playerTransform = PlayerMovement.Instance.transform;

        // Tính toán vector hướng vào tâm ngay từ đầu
        directionToCenter = (Vector2)(boundary.Center - transform.position).normalized;
    }

    private void Update()
    {
        if (velocity.x < 0)
        {
            modelTransform.localScale = new Vector3(-1, 1, 1);
        }
        else if (velocity.x > 0)
        {
            modelTransform.localScale = new Vector3(1, 1, 1);
        }
        Move();
    }

    public void SetBoundary(Boundary boundary)
    {
        this.boundary = boundary;
        // Cập nhật vector hướng vào tâm mỗi khi thiết lập biên
        directionToCenter = (Vector2)(boundary.Center - transform.position).normalized;
    }

    //private void Move()
    //{
    //    if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
    //    {
    //        AvoidPlayer();
    //    }
    //    CheckBoundary();
    //}

    //private void CheckBoundary()
    //{
    //    float distanceToCenter = Vector3.Distance(transform.position, boundary.Center);
    //    Debug.Log("Distance to center: " + distanceToCenter);

    //    if (distanceToCenter >= boundary.Radius - 1f)
    //    {
    //        Vector2 directionToCenter = (boundary.Center - transform.position).normalized;

    //        float leftAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg - 45f;
    //        float rightAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg + 45f;

    //        float randomAngle = Random.Range(leftAngle, rightAngle);

    //        Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;

    //        velocity = randomDirection;

    //        transform.position += (Vector3)(velocity * slowSpeed) * Time.deltaTime;
    //    }
    //    else
    //    {

    //        transform.position += (Vector3)(velocity * speed) * Time.deltaTime;
    //    }
    //}

    //private void Move()
    //{
    //    if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
    //    {
    //        AvoidPlayer();
    //    }
    //    CheckBoundary();
    //}

    private void Move()
    {
        //if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
        //{
        //    AvoidPlayer(); // Chỉ tránh người chơi nếu trong phạm vi phát hiện
        //}
        //else
        //{
        //    // Di chuyển theo hướng hiện tại
        //    transform.position += (Vector3)(velocity * speed) * Time.deltaTime;
        //}
        CheckBoundary(); // Kiểm tra biên trước khi tránh người chơi
    }


    private void CheckBoundary()
    {
        float distanceToCenter = Vector3.Distance(transform.position, boundary.Center);
        Debug.Log("Distance to center: " + distanceToCenter);

        if (distanceToCenter >= boundary.Radius - 1f)
        {
            // Tính toán vector hướng vào tâm
            Vector2 directionToCenter = (boundary.Center - transform.position).normalized;

            // Mở rộng sang 2 bên 45 độ
            float leftAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg - 45f;
            float rightAngle = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg + 45f;

            // Tạo góc ngẫu nhiên trong khoảng 45 độ sang trái và phải
            float randomAngle = Random.Range(leftAngle, rightAngle);

            // Tính toán vector ngẫu nhiên dựa trên góc
            Vector2 randomDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;

            // Cộng vector ngẫu nhiên với vector hướng vào tâm
            velocity = randomDirection;

            // Giảm tốc độ khi ở gần biên
            transform.position += (Vector3)(velocity * slowSpeed) * Time.deltaTime;
        }
        else
        {
            // Tính toán vector từ người chơi đến thỏ

            if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
            {
                directionToPlayer = (Vector2)(transform.position - playerTransform.position).normalized;
                Vector2 directionToCenter = ((Vector2)boundary.Center - (Vector2)transform.position).normalized;

                //Vector2 playerDirection = (Vector2)(playerTransform.position - transform.position);

                velocity = (directionToPlayer + (directionToCenter * 0.7f) + velocity).normalized;
            }

            // Nếu người chơi tiến gần, thỏ sẽ lấy vector của người chơi trừ vector của thỏ

            // Di chuyển thỏ theo vector hiện tại
            transform.position += (Vector3)(velocity * speed) * Time.deltaTime;
        }
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        velocity = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;
    }

    //private void AvoidPlayer()
    //{
    //    Vector2 directionToPlayer = (Vector2)(transform.position - playerTransform.position);
    //    velocity = directionToPlayer.normalized;
    //}

    private void AvoidPlayer()
    {
        // Tính toán vector hướng từ thỏ đến người chơi
        Vector2 directionToPlayer = (Vector2)(transform.position - playerTransform.position).normalized;

        // Tính toán vector hướng vào tâm
        Vector2 directionToCenter = ((Vector2)boundary.Center - (Vector2)transform.position).normalized;

        // Kết hợp vector tránh người chơi và vector hướng vào tâm
        float avoidanceStrength = 0.5f; // Tùy chỉnh cường độ tránh
        velocity = (directionToPlayer + directionToCenter).normalized;

        // Cập nhật tốc độ dựa trên hướng di chuyển
        transform.position += (Vector3)(velocity * speed) * Time.deltaTime;
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, transform.position + (Vector3)velocity);

    //    // Vẽ vector hướng vào tâm
    //    if (boundary != null)
    //    {
    //        Gizmos.color = Color.magenta; // Màu sắc cho vector hướng vào tâm
    //        Gizmos.DrawLine(transform.position, boundary.Center);
    //    }

    //    if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(transform.position, playerTransform.position);
    //    }

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
    //}

    private void OnDrawGizmos()
    {
        // Vẽ vector hướng hiện tại
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)velocity);

        // Vẽ vector hướng vào tâm
        if (boundary != null)
        {
            Gizmos.color = Color.magenta; // Màu sắc cho vector hướng vào tâm
            Gizmos.DrawLine(transform.position, boundary.Center);

            // Vẽ vector mở rộng
            float leftAngle = Mathf.Atan2((boundary.Center - transform.position).y, (boundary.Center - transform.position).x) * Mathf.Rad2Deg - 45f;
            float rightAngle = Mathf.Atan2((boundary.Center - transform.position).y, (boundary.Center - transform.position).x) * Mathf.Rad2Deg + 45f;

            // Vẽ các đường đến biên
            Vector2 leftDirection = new Vector2(Mathf.Cos(leftAngle * Mathf.Deg2Rad), Mathf.Sin(leftAngle * Mathf.Deg2Rad));
            Vector2 rightDirection = new Vector2(Mathf.Cos(rightAngle * Mathf.Deg2Rad), Mathf.Sin(rightAngle * Mathf.Deg2Rad));

            Gizmos.color = Color.yellow; // Màu sắc cho các vector mở rộng
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * 2f);
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * 2f);
        }

        // Vẽ vector hướng về người chơi
        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }



}
