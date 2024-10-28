//using System.Collections;
//using UnityEngine;

//public class RabbitMovement : MonoBehaviour
//{
//    public Vector2 Velocity { private set; get; }
//    [SerializeField] private GameObject model;
//    private Transform modelTransform;
//    private Animator animator;
//    private Rigidbody2D rb;

//    [SerializeField] private float speed = 2f;
//    [SerializeField] private float moveDuration = 1f;
//    private float stopDuration;

//    private void Start()
//    {
//        modelTransform = model != null ? model.transform : null;
//        if (modelTransform == null)
//        {
//            Debug.LogError("Model object not found!");
//        }

//        animator = model.GetComponent<Animator>();
//        if (animator == null)
//        {
//            Debug.LogError("Animator component not found on model!");
//        }

//        rb = GetComponentInParent<Rigidbody2D>();
//        if (rb == null)
//        {
//            Debug.LogError("Rigidbody2D is missing on parent!");
//        }

//        StartCoroutine(MoveAndStopCycle());
//    }

//    private IEnumerator MoveAndStopCycle()
//    {
//        while (true)
//        {
//            SetRandomDirection();

//            float elapsedTime = 0f;
//            animator.SetBool("RabbitIsMoving", true);
//            while (elapsedTime < moveDuration)
//            {
//                Vector2 targetPosition = rb.position + Velocity * speed * Time.fixedDeltaTime;
//                rb.MovePosition(targetPosition);

//                if (Velocity.x < 0)
//                {
//                    modelTransform.localScale = new Vector3(-1, 1, 1);
//                }
//                else if (Velocity.x > 0)
//                {
//                    modelTransform.localScale = new Vector3(1, 1, 1);
//                }

//                elapsedTime += Time.deltaTime;
//                yield return new WaitForFixedUpdate();
//            }

//            stopDuration = Random.Range(1f, 3f);
//            Velocity = Vector2.zero;
//            animator.SetBool("RabbitIsMoving", false);
//            yield return new WaitForSeconds(stopDuration);
//        }
//    }

//    private void SetRandomDirection()
//    {
//        float randomAngle = Random.Range(0f, 360f);
//        Velocity = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag("Player"))
//        {
//            Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), collision.collider);
//        }
//    }
//}


//using UnityEngine;

//public class RabbitMovement : MonoBehaviour
//{
//    public Vector2 Velocity { private set; get; }
//    [SerializeField] private GameObject model;
//    private Transform modelTransform;
//    private Animator animator;
//    private Rigidbody2D rb;

//    [SerializeField] private float speed = 2f;
//    [SerializeField] private float neighborRadius = 5f;    // Bán kính tìm kiếm thỏ lân cận
//    [SerializeField] private float separationDistance = 1f; // Khoảng cách tối thiểu giữa các thỏ
//    [SerializeField] private float fieldOfView = 120f;      // Góc nhìn của thỏ
//    [SerializeField] private Color neighborColor = Color.green;   // Màu cho bán kính lân cận
//    [SerializeField] private Color fovColor = Color.blue;         // Màu cho vùng góc nhìn
//    [SerializeField] private ListRabbitVariable listRabbitVariable; // Biến chứa danh sách các thỏ

//    private void Start()
//    {
//        modelTransform = model != null ? model.transform : null;
//        animator = model.GetComponent<Animator>();
//        rb = GetComponentInParent<Rigidbody2D>();

//        if (listRabbitVariable == null || listRabbitVariable.rabbitMovement == null)
//        {
//            Debug.LogError("ListRabbitVariable or its rabbits list is missing!");
//            return;
//        }
//    }

//    private void FixedUpdate()
//    {
//        Velocity = BoidsAlgorithm();
//        Vector2 targetPosition = rb.position + Velocity * speed * Time.fixedDeltaTime;
//        rb.MovePosition(targetPosition);

//        // Xoay model của thỏ theo hướng di chuyển
//        if (Velocity.x < 0)
//        {
//            modelTransform.localScale = new Vector3(-1, 1, 1);
//        }
//        else if (Velocity.x > 0)
//        {
//            modelTransform.localScale = new Vector3(1, 1, 1);
//        }

//        animator.SetBool("RabbitIsMoving", Velocity.magnitude > 0.1f);
//    }

//    private Vector2 BoidsAlgorithm()
//    {
//        Vector2 cohesion = Cohesion() * 1f;
//        Vector2 separation = Separation() * 1.5f;
//        Vector2 alignment = Alignment() * 1f;

//        return cohesion + separation + alignment;
//    }

//    private Vector2 Cohesion()
//    {
//        Vector2 centerOfMass = Vector2.zero;
//        int neighborCount = 0;

//        foreach (var rabbit in listRabbitVariable.rabbitMovement)
//        {
//            if (rabbit != this && IsInView(rabbit))
//            {
//                centerOfMass += (Vector2)rabbit.transform.position;
//                neighborCount++;
//            }
//        }

//        if (neighborCount == 0) return Vector2.zero;

//        centerOfMass /= neighborCount;
//        return (centerOfMass - (Vector2)transform.position).normalized;
//    }

//    private Vector2 Separation()
//    {
//        Vector2 separationForce = Vector2.zero;

//        foreach (var rabbit in listRabbitVariable.rabbitMovement)
//        {
//            float distance = Vector2.Distance(transform.position, rabbit.transform.position);
//            if (rabbit != this && distance < separationDistance && IsInView(rabbit))
//            {
//                separationForce += (Vector2)(transform.position - rabbit.transform.position).normalized / distance;
//            }
//        }

//        return separationForce;
//    }

//    private Vector2 Alignment()
//    {
//        Vector2 averageVelocity = Vector2.zero;
//        int neighborCount = 0;

//        foreach (var rabbit in listRabbitVariable.rabbitMovement)
//        {
//            if (rabbit != this && IsInView(rabbit))
//            {
//                averageVelocity += rabbit.Velocity;
//                neighborCount++;
//            }
//        }

//        if (neighborCount == 0) return Vector2.zero;

//        averageVelocity /= neighborCount;
//        return averageVelocity.normalized;
//    }

//    // Kiểm tra xem con thỏ khác có nằm trong góc nhìn hay không
//    private bool IsInView(RabbitMovement rabbit)
//    {
//        Vector2 directionToRabbit = (rabbit.transform.position - transform.position).normalized;
//        float angleToRabbit = Vector2.Angle(transform.up, directionToRabbit);
//        return angleToRabbit < fieldOfView / 2 && Vector2.Distance(transform.position, rabbit.transform.position) < neighborRadius;
//    }

//    // Vẽ Gizmos cho vùng góc nhìn và bán kính tìm kiếm lân cận
//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = neighborColor;
//        Gizmos.DrawWireSphere(transform.position, neighborRadius);

//        Gizmos.color = fovColor;
//        Vector3 leftBoundary = Quaternion.Euler(0, 0, fieldOfView / 2) * transform.up * neighborRadius;
//        Vector3 rightBoundary = Quaternion.Euler(0, 0, -fieldOfView / 2) * transform.up * neighborRadius;
//        Gizmos.DrawRay(transform.position, leftBoundary);
//        Gizmos.DrawRay(transform.position, rightBoundary);
//    }
//}



using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField]
    private ListRabbitVariable rabbits;

    [SerializeField]
    private float radius = 2f;

    [SerializeField]
    private float visionAngle = 270f;

    [SerializeField]
    private float forwardSpeed = 5f;

    [SerializeField]
    private float turnSpeed = 10f;

    [SerializeField] private float separationWeight = 1.5f;
    [SerializeField] private float alignmentWeight = 1f;
    [SerializeField] private float cohesionWeight = 0.5f;

    public Vector3 Velocity { get; private set; }

    private void Start()
    {
        Velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * forwardSpeed;
    }

    private void FixedUpdate()
    {
        Velocity = Vector2.Lerp(Velocity, CalculateVelocity(), turnSpeed / 2 * Time.fixedDeltaTime);
        transform.position += Velocity * Time.fixedDeltaTime;
        Debug.Log(Velocity);
        LookRotation();
    }

    //private void LookRotation()
    //{
    //    transform.rotation = Quaternion.Slerp(transform.localRotation,
    //        Quaternion.LookRotation(Velocity), turnSpeed * Time.fixedDeltaTime);
    //}

    private void LookRotation()
    {
        if (Velocity != Vector3.zero)
        {
            float angle = Mathf.Atan2(Velocity.y, Velocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }

    //private Vector2 CalculateVelocity()
    //{
    //    var rabbitInRange = RabbitInRange();
    //    Debug.Log("========" + rabbitInRange);
    //    Vector2 velocity = ((Vector2)transform.forward
    //        + Separation(rabbitInRange)
    //        + Alignment(rabbitInRange)
    //        + Cohesion(rabbitInRange)
    //        ).normalized * forwardSpeed;
    //    return velocity;
    //}

    private Vector2 CalculateVelocity()
    {
        var rabbitInRange = RabbitInRange();
        Vector2 forwardDirection = new Vector2(transform.forward.x, transform.forward.y);
        Vector2 separation = Separation(rabbitInRange);
        Vector2 alignment = Alignment(rabbitInRange);
        Vector2 cohesion = Cohesion(rabbitInRange);

        // Kiểm tra các giá trị
        Debug.Log("Separation: " + separation);
        Debug.Log("Alignment: " + alignment);
        Debug.Log("Cohesion: " + cohesion);

        Vector2 velocity = (forwardDirection
            + separationWeight * Separation(rabbitInRange)
            + alignmentWeight * Alignment(rabbitInRange)
            + cohesionWeight * Cohesion(rabbitInRange)
            ).normalized * forwardSpeed;

        // Đảm bảo velocity không bằng Vector2.zero
        //if (velocity == Vector2.zero)
        //{
        //    velocity = (Vector2)transform.up * forwardSpeed;
        //}
        return velocity;
    }

    private List<RabbitMovement> RabbitInRange()
    {
        var listRabbit = rabbits.rabbitMovement.FindAll(rabbit => rabbit != this && (rabbit.transform.position - transform.position).magnitude <= radius && InVisionCone(rabbit.transform.position));
        return listRabbit;
    }

    //private bool InVisionCone(Vector2 position)
    //{
    //    Vector2 directionToPosition = position - (Vector2)transform.position;
    //    float dotProduct = Vector2.Dot(transform.forward, directionToPosition);
    //    float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);

    //    return dotProduct >= cosHalfVisionAngle;
    //}

    private bool InVisionCone(Vector2 position)
    {
        Vector2 directionToPosition = (position - (Vector2)transform.position).normalized;
        float dotProduct = Vector2.Dot(transform.up, directionToPosition);
        float cosHalfVisionAngle = Mathf.Cos(visionAngle * 0.5f * Mathf.Deg2Rad);

        return dotProduct >= cosHalfVisionAngle;
    }


    private Vector2 Separation(List<RabbitMovement> rabbitMovements)
    {
        Vector2 direction = Vector2.zero;

        foreach (var rabbit in rabbitMovements)
        {
            float ratio = Mathf.Clamp01((rabbit.transform.position - transform.position).magnitude / radius);
            direction -= ratio * (Vector2)(rabbit.transform.position - transform.position);
        }

        return direction.normalized;
    }

    private Vector2 Alignment(List<RabbitMovement> rabbitMovements)
    {
        Vector2 direction = Vector2.zero;

        foreach (var rabbit in rabbitMovements)
        {
            direction += (Vector2)rabbit.Velocity;
        }
        if (rabbitMovements.Count > 0)
        {
            direction /= rabbitMovements.Count;
        }
        else
        {
            direction = Velocity;
        }

        return direction.normalized;
    }

    private Vector2 Cohesion(List<RabbitMovement> rabbitMovements)
    {
        Vector2 direction;
        Vector2 center = Vector2.zero;

        foreach (var rabbit in rabbitMovements)
        {
            center += (Vector2)(rabbit.transform.position);
        }
        if (rabbitMovements.Count != 0)
        {
            center /= rabbitMovements.Count;
        }
        else
        {
            center = transform.position;
        }

        direction = center - (Vector2)transform.position;

        return direction.normalized;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        var rabbitsInRange = RabbitInRange();

        foreach (var boid in rabbitsInRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, boid.transform.position);
        }
    }
}
