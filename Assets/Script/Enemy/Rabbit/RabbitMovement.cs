using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Transform playerTransform;

    [SerializeField]
    private GameObject model;

    private Transform modelTransform;

    [SerializeField]
    private float detectionRadius = 5f;

    private Vector2 velocity;

    private void Start()
    {
        modelTransform = model != null ? model.transform : null;
        if (modelTransform == null)
        {
            Debug.LogError("Model object not found!");
        }
        SetRandomDirection();
        playerTransform = PlayerMovement.Instance.transform;
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
        Debug.Log("Player:" + PlayerMovement.Instance.transform.position.x + "\n" + PlayerMovement.Instance.transform.position.y);
    }


    private void Move()
    {
        transform.position += (Vector3)velocity * speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            AvoidPlayer();
        }
        else
        {
            if (Random.Range(0f, 1f) < 0.001f)
            {
                SetRandomDirection();
            }
        }
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        velocity = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad)).normalized;
    }

    private void AvoidPlayer()
    {
        Vector2 directionToPlayer = (Vector2)(transform.position - playerTransform.position);
        velocity = directionToPlayer.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)velocity);

        if (Vector2.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
