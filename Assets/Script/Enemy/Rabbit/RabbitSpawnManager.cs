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
            SummonTheRabbits(RandomPosition());
        }
    }


    private Vector2 RandomPosition()
    {
        return new Vector2(
               Random.Range(boundary.PointA.x, boundary.PointB.x),
               Random.Range(boundary.PointA.y, boundary.PointB.y)
           );
    }

    private void SummonTheRabbits(Vector2 position)
    {
        GameObject rabbit = Instantiate(rabbitPrefab, position, Quaternion.identity);
        rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());
    }
}
