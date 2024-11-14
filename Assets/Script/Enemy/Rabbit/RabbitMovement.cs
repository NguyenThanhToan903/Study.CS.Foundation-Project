//using System.Collections.Generic;
//using UnityEngine;

//public class RabbitMovement : MonoBehaviour
//{
//    [SerializeField] private float speed = 2f;
//    [SerializeField] private float updateInterval = 2f;
//    [SerializeField] private ListRabbitVariable rabbits;
//    [SerializeField] private List<RabbitMovement> listInRange;

//    private Vector2 currentTarget;
//    private readonly float visionAngle = 120f;
//    private readonly float avoidAngle = 270f;

//    private Rigidbody2D rb;
//    private float updateTimer = 0f;
//    private float randomTime;

//    public Vector2 Direction { get; private set; }


//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();
//        if (rabbits != null && !rabbits.rabbitMovement.Contains(this))
//        {
//            rabbits.rabbitMovement.Add(this);
//        }

//        randomTime = Random.Range(0f, updateInterval);
//    }

//    private void FixedUpdate()
//    {
//        Move();
//        var lists = RabbitInRange();
//        Debug.Log(lists);
//    }


//    private void OnDestroy()
//    {
//        if (rabbits != null)
//        {
//            rabbits.rabbitMovement.Remove(this);
//        }
//    }

//    public void Move()
//    {
//        updateTimer += Time.fixedDeltaTime;

//        if (updateTimer >= randomTime)
//        {
//            updateTimer = 0f;
//            currentTarget = RandomTarget();
//            randomTime = Random.Range(0f, updateInterval);
//        }

//        MoveTowards(currentTarget, speed);
//    }

//    public void MoveTowards(Vector2 targetPosition, float moveSpeed)
//    {
//        if (rb != null)
//        {
//            Direction = (targetPosition - rb.position).normalized;
//            rb.velocity = Direction * moveSpeed;
//        }
//    }

//    public Vector2 RandomTarget()
//    {
//        float halfVisionAngle = visionAngle * 0.5f;

//        Vector2 normalizedVelocity = rb.velocity.normalized;
//        Vector2 leftDirection = Quaternion.Euler(0, 0, -halfVisionAngle) * normalizedVelocity;
//        Vector2 rightDirection = Quaternion.Euler(0, 0, halfVisionAngle) * normalizedVelocity;

//        float randomFactor = Random.Range(0f, 1f);
//        Vector2 randomDirection = Vector2.Lerp(leftDirection, rightDirection, randomFactor).normalized;

//        float moveDistance = 5f;
//        Vector2 targetPosition = rb.position + randomDirection * moveDistance;

//        return targetPosition;
//    }

//    private List<RabbitMovement> RabbitInRange()
//    {
//        listInRange = rabbits.rabbitMovement.FindAll(rabbit => rabbit != this && (rabbit.transform.position - transform.position).magnitude <= 5 && InVisionAngle(rabbit.transform.position));

//        foreach (var e in listInRange)
//        {

//            Gizmos.color = Color.blue;
//            Gizmos.DrawLineList(e.transform.position - transform.position);
//        }
//        return listInRange;
//    }

//    private bool InVisionAngle(Vector2 position)
//    {
//        Vector2 directionToPosition = position - (Vector2)transform.position;
//        float dotProduct = Vector2.Dot(transform.forward, directionToPosition);
//        float cosHalfAvoidAngle = Mathf.Cos(avoidAngle * 0.5f * Mathf.Deg2Rad);

//        return dotProduct >= cosHalfAvoidAngle;
//    }

//    private void OnDrawGizmos()
//    {
//        Vector2 normalizedVelocity = rb.velocity.normalized;

//        float halfVisionAngle = visionAngle * 0.5f;

//        Vector2 leftDirection = Quaternion.Euler(0, 0, -halfVisionAngle) * normalizedVelocity;
//        Vector2 rightDirection = Quaternion.Euler(0, 0, halfVisionAngle) * normalizedVelocity;

//        Gizmos.color = Color.blue;
//        Gizmos.DrawLine(transform.position, currentTarget);

//        Gizmos.color = Color.white;

//        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * 1f);
//        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * 1f);
//    }
//}


using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float updateInterval = 2f;
    [SerializeField] private ListRabbitVariable rabbits;
    [SerializeField] private List<RabbitMovement> listInRange;

    private Vector2 currentTarget;
    private readonly float visionAngle = 120f;
    private readonly float avoidAngle = 270f;
    private readonly float collisionRadius = 5f;  // Bán kính va chạm

    private Rigidbody2D rb;
    private float updateTimer = 0f;
    private float randomTime;

    public Vector2 Direction { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rabbits != null && !rabbits.rabbitMovement.Contains(this))
        {
            rabbits.rabbitMovement.Add(this);
        }

        randomTime = Random.Range(0f, updateInterval);
    }

    private void FixedUpdate()
    {
        Move();
        var lists = RabbitInRange();  // Kiểm tra những con thỏ xung quanh
        Debug.Log(lists);
    }

    private void OnDestroy()
    {
        if (rabbits != null)
        {
            rabbits.rabbitMovement.Remove(this);
        }
    }

    public void Move()
    {
        updateTimer += Time.fixedDeltaTime;

        if (updateTimer >= randomTime)
        {
            updateTimer = 0f;
            currentTarget = RandomTarget();
            randomTime = Random.Range(0f, updateInterval);
        }

        MoveTowards(currentTarget, speed);
    }

    public void MoveTowards(Vector2 targetPosition, float moveSpeed)
    {
        if (rb != null)
        {
            Direction = (targetPosition - rb.position).normalized;
            rb.velocity = Direction * moveSpeed;
        }
    }

    public Vector2 RandomTarget()
    {
        float halfVisionAngle = visionAngle * 0.5f;

        Vector2 normalizedVelocity = rb.velocity.normalized;
        Vector2 leftDirection = Quaternion.Euler(0, 0, -halfVisionAngle) * normalizedVelocity;
        Vector2 rightDirection = Quaternion.Euler(0, 0, halfVisionAngle) * normalizedVelocity;

        float randomFactor = Random.Range(0f, 1f);
        Vector2 randomDirection = Vector2.Lerp(leftDirection, rightDirection, randomFactor).normalized;

        float moveDistance = 5f;
        Vector2 targetPosition = rb.position + randomDirection * moveDistance;

        return targetPosition;
    }

    private List<RabbitMovement> RabbitInRange()
    {
        // Lọc các con thỏ trong phạm vi tầm nhìn và trong khoảng cách 5 đơn vị
        listInRange = rabbits.rabbitMovement.FindAll(rabbit => rabbit != this && (rabbit.transform.position - transform.position).magnitude <= collisionRadius);

        // Xử lý các con thỏ trong phạm vi, tránh xa nếu cần thiết
        foreach (var e in listInRange)
        {
            if (InVisionAngle(e.transform.position))
            {
                AvoidRabbit(e);  // Tránh xa con thỏ này
            }
        }

        return listInRange;
    }

    // Kiểm tra xem con thỏ này có trong phạm vi góc nhìn không
    private bool InVisionAngle(Vector2 position)
    {
        Vector2 directionToPosition = position - (Vector2)transform.position;
        float dotProduct = Vector2.Dot(transform.up, directionToPosition);  // Sử dụng hướng di chuyển của con thỏ
        float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);

        return dotProduct >= cosHalfVisionAngle;
    }

    // Kiểm tra xem có cần tránh con thỏ này không
    //private bool ShouldAvoid(RabbitMovement otherRabbit)
    //{
    //    Vector2 directionToOtherRabbit = otherRabbit.transform.position - transform.position;
    //    float angle = Vector2.Angle(Direction, directionToOtherRabbit.normalized);  // Tính góc giữa hướng di chuyển và vị trí của con thỏ

    //    return angle <= avoidAngle;  // Nếu góc nhỏ hơn góc tránh, cần tránh con thỏ này
    //}

    // Di chuyển tránh xa con thỏ
    private void AvoidRabbit(RabbitMovement otherRabbit)
    {
        Vector2 directionToOtherRabbit = otherRabbit.transform.position - transform.position;
        Vector2 avoidDirection = -directionToOtherRabbit.normalized;  // Hướng ngược lại với con thỏ cần tránh

        rb.velocity = avoidDirection * speed;  // Di chuyển theo hướng tránh xa
    }

    // Vẽ Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);  // Vẽ vùng va chạm

        // Vẽ đường thẳng từ con thỏ này đến các con thỏ khác trong phạm vi va chạm
        Gizmos.color = Color.red;
        foreach (var otherRabbit in listInRange)
        {
            Gizmos.DrawLine(transform.position, otherRabbit.transform.position);
        }

        // Vẽ vùng tầm nhìn của con thỏ
        Vector2 normalizedVelocity = rb.velocity.normalized;
        float halfVisionAngle = visionAngle * 0.5f;
        Vector2 leftDirection = Quaternion.Euler(0, 0, -halfVisionAngle) * normalizedVelocity;
        Vector2 rightDirection = Quaternion.Euler(0, 0, halfVisionAngle) * normalizedVelocity;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * 1f);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * 1f);
    }
}
