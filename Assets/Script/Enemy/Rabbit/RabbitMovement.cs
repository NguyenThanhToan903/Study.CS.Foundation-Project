using UnityEngine;



public class RabbitMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distanceAvoid = 3f;
    [SerializeField] private float wallDetectionDistance = 3f;

    //List Rabbit Variable
    [SerializeField] private ListRabbitVariable rabbits;

    private Vector2 movementDirection;



    private void Start()
    {
        SetRandomDirection();
    }

    private void Update()
    {
        this.transform.position += (Vector3)(speed * Time.deltaTime * movementDirection);
        Debug.Log(this.transform.position);
        CheckWallCollision();
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        movementDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
    }

    private void CheckWallCollision()
    {
        Vector2 currentPosition = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(currentPosition, movementDirection, wallDetectionDistance);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            movementDirection = -movementDirection;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Vector2 currentPosition = transform.position;
            Gizmos.DrawLine(currentPosition, currentPosition + movementDirection * wallDetectionDistance);
        }
    }

}
