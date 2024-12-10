using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    [SerializeField]
    private Boundary boundary;

    [SerializeField]
    private GameObject rabbitPrefab;

    [SerializeField]
    private ListRabbitVariable rabbits;

    [SerializeField]
    private int rabbitCount = 5;

    [SerializeField]
    private float rabbitSpeed = 2.0f;

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
        RabbitMovement rabbitMovement = rabbit.GetComponent<RabbitMovement>();
        rabbitMovement.speed = rabbitSpeed;
        rabbits.rabbitMovement.Add(rabbitMovement);
    }

    public float GetRabbitCount()
    {
        return this.rabbitCount;
    }
}
