using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    [SerializeField] private ListRabbitVariable rabbits;
    [SerializeField] private GameObject rabbitPrefab;
    [SerializeField] private int rabbitCount;

    private void Start()
    {
        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();

        for (int i = 0; i < rabbitCount; i++)
        {
            SpawnRabbit();
        }
    }

    private void SpawnRabbit()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);

        GameObject rabbit = Instantiate(rabbitPrefab, randomPosition, Quaternion.identity);

        rabbit.transform.SetParent(transform);

        RabbitMovement rabbitMovement = rabbit.GetComponentInChildren<RabbitMovement>();
        if (rabbitMovement != null)
        {
            rabbits.rabbitMovement.Add(rabbitMovement);
        }
        else
        {
            Debug.LogError("RabbitMovement component not found on the spawned rabbit.");
        }
    }
}
