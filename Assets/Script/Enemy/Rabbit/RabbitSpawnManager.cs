using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject rabbitPrefab;
    [SerializeField] private int rabbitCount = 5;
    [SerializeField] private Boundary boundary;
    [SerializeField] private ListRabbitVariable rabbits;

    private void Start()
    {
        SpawnRabbits();
    }

    private void SpawnRabbits()
    {
        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();

        for (int i = 0; i < rabbitCount; i++)
        {
            Vector2 position = new Vector2(
                Random.Range(boundary.PointA.x, boundary.PointB.x),
                Random.Range(boundary.PointA.y, boundary.PointB.y)
            );
            GameObject rabbit = Instantiate(rabbitPrefab, position, Quaternion.identity);
            rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());
        }
    }
}
