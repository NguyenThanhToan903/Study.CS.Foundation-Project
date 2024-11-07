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

    [SerializeField] public Vector3 Velocity { private set; get; }

    [SerializeField] private Boundary boundary;

    [SerializeField] private float timeSinceLastUpdate = 0f;

    [SerializeField] private Vector2 directionToCenter;

    [SerializeField] private ListRabbitVariable rabbits;

    Vector2 directionToPlayer = Vector2.zero;


    [SerializeField] private float moveDuration;

    [SerializeField] private float stopDuration;

    private float stateTimer;

    private bool isMoving = true;

    private Vector2 currentDirection;

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
        //Move();
        SetSpriteScale();
    }




    private void FixedUpdate()
    {
        stateTimer -= Time.fixedDeltaTime;

        if (isMoving)
        {
            Velocity = currentDirection * speed;
            transform.position += (Vector3)Velocity * Time.fixedDeltaTime;

            //LookRotation();

            if (stateTimer <= 0)
            {
                isMoving = false;
                stateTimer = GetRandomTime();
                Velocity = Vector3.zero;
            }
        }
        else
        {
            if (stateTimer <= 0)
            {
                isMoving = true;
                stateTimer = GetRandomTime();
                currentDirection = GetRandomDirection();
            }
        }

    }

    private float GetRandomTime()
    {
        return Random.Range(1f, 3f);
    }

    private Vector2 GetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 3 / 2f * Mathf.PI);
        return new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized + CalculateVelocity();
    }

    private void LookRotation()
    {
        //if (Velocity != Vector3.zero)
        //    transform.rotation = Quaternion.Slerp(transform.localRotation,
        //        Quaternion.LookRotation(Velocity), speed * Time.fixedDeltaTime);
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, transform.position + (Vector3)Velocity);

    //    if (boundary != null)
    //    {
    //        Vector2 topLeft = new(boundary.PointA.x, boundary.PointB.y);
    //        Vector2 topRight = boundary.PointB;
    //        Vector2 bottomLeft = boundary.PointA;
    //        Vector2 bottomRight = new(boundary.PointB.x, boundary.PointA.y);

    //        Gizmos.color = Color.cyan;
    //        Gizmos.DrawLine(topLeft, topRight);
    //        Gizmos.DrawLine(topRight, bottomRight);
    //        Gizmos.DrawLine(bottomRight, bottomLeft);
    //        Gizmos.DrawLine(bottomLeft, topLeft);
    //    }

    //    if (boundary != null)
    //    {
    //        Gizmos.color = Color.magenta;
    //        Gizmos.DrawLine(transform.position, boundary.Center);

    //        float leftAngle = Mathf.Atan2((boundary.Center - (Vector2)transform.position).y, (boundary.Center - (Vector2)transform.position).x) * Mathf.Rad2Deg - 45f;
    //        float rightAngle = Mathf.Atan2((boundary.Center - (Vector2)transform.position).y, (boundary.Center - (Vector2)transform.position).x) * Mathf.Rad2Deg + 45f;

    //        Vector2 leftDirection = new(Mathf.Cos(leftAngle * Mathf.Deg2Rad), Mathf.Sin(leftAngle * Mathf.Deg2Rad));
    //        Vector2 rightDirection = new(Mathf.Cos(rightAngle * Mathf.Deg2Rad), Mathf.Sin(rightAngle * Mathf.Deg2Rad));

    //        Gizmos.color = Color.yellow;
    //        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * 2f);
    //        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * 2f);
    //    }

    //    if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
    //    {
    //        Gizmos.color = Color.green;
    //        Gizmos.DrawLine(transform.position, playerTransform.position);
    //    }

    //    //Gizmos.color = Color.red;
    //    //Gizmos.DrawWireSphere(transform.position, detectionRadius);

    //    Gizmos.color = Color.magenta;
    //    Vector2 targetPosition = (Vector2)transform.position + (Vector2)Velocity * 2f;
    //    //Gizmos.DrawSphere(targetPosition, 0.2f);
    //    Gizmos.DrawLine(transform.position, targetPosition);
    //}


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

        //if (boundary != null)
        //{
        //    Gizmos.color = Color.magenta;
        //    Gizmos.DrawLine(transform.position, boundary.Center);

        //    float leftAngle = Mathf.Atan2((boundary.Center - (Vector2)transform.position).y, (boundary.Center - (Vector2)transform.position).x) * Mathf.Rad2Deg - 45f;
        //    float rightAngle = Mathf.Atan2((boundary.Center - (Vector2)transform.position).y, (boundary.Center - (Vector2)transform.position).x) * Mathf.Rad2Deg + 45f;

        //    Vector2 leftDirection = new(Mathf.Cos(leftAngle * Mathf.Deg2Rad), Mathf.Sin(leftAngle * Mathf.Deg2Rad));
        //    Vector2 rightDirection = new(Mathf.Cos(rightAngle * Mathf.Deg2Rad), Mathf.Sin(rightAngle * Mathf.Deg2Rad));

        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * 2f);
        //    Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * 2f);
        //}

        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }

        // Vẽ góc nhìn 270°
        Gizmos.color = Color.red;

        // Tính toán hướng trái và phải trong phạm vi góc nhìn
        float halfVisionAngle = visionAngle * 0.5f; // Góc 135° (nửa của 270°)

        // Vẽ hai đường (tạo thành một vùng nhìn 270°)
        Vector2 leftDirection = Quaternion.Euler(0, 0, -halfVisionAngle) * transform.right;
        Vector2 rightDirection = Quaternion.Euler(0, 0, halfVisionAngle) * transform.right;

        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftDirection * detectionRadius);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightDirection * detectionRadius);

        // Vẽ trung tâm của vùng nhìn (để dễ theo dõi)
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)transform.right * detectionRadius);
    }

    //luu lai huong truoc do, tao huong moi dua tren huong truoc do



}
