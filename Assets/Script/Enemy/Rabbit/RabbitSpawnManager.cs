using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    [SerializeField]
    private ListRabbitVariable rabbits;

    [SerializeField]
    private GameObject rabbitPrefab;

    [SerializeField]
    private float rabbitCount;

    [SerializeField]
    private Boundary boundary;

    private void Start()
    {
        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();


        boundary.Center = transform.position;

        for (int i = 0; i < rabbitCount; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * boundary.Radius;
            Vector3 spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0) + boundary.Center;
            GameObject rabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);
            rabbit.transform.SetParent(transform);
            rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());

            rabbit.GetComponent<RabbitMovement>().SetBoundary(boundary);
        }
    }

    private void FixedUpdate()
    {
        boundary.Center = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(boundary.Center, boundary.Radius); // Vẽ tại vị trí tâm
    }
}
