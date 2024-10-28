//using UnityEngine;

//public class RabbitSpawnManager : MonoBehaviour
//{
//    [SerializeField] private ListRabbitVariable rabbits;
//    [SerializeField] private GameObject rabbitPrefab;
//    [SerializeField] private int rabbitCount;

//    // Kích thước vùng spawn
//    [SerializeField] private Vector2 spawnAreaSize = new Vector2(20f, 20f); // Kích thước vùng spawn

//    private void Start()
//    {
//        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();

//        for (int i = 0; i < rabbitCount; i++)
//        {
//            SpawnRabbit();
//        }
//    }

//    private void SpawnRabbit()
//    {
//        // Tính toán vị trí spawn dựa trên tâm và kích thước vùng spawn
//        Vector3 randomPosition = new Vector3(
//            transform.position.x + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
//            transform.position.y + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
//            0f
//        );

//        // Instantiate rabbit và thiết lập làm con của GameObject này
//        GameObject rabbit = Instantiate(rabbitPrefab, randomPosition, Quaternion.identity);
//        rabbit.transform.SetParent(transform);

//        RabbitMovement rabbitMovement = rabbit.GetComponentInChildren<RabbitMovement>();
//        if (rabbitMovement != null)
//        {
//            rabbits.rabbitMovement.Add(rabbitMovement);
//        }
//        else
//        {
//            Debug.LogError("RabbitMovement component not found on the spawned rabbit.");
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        // Vẽ hình chữ nhật vùng spawn
//        Gizmos.color = Color.green;
//        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 1f));
//    }
//}


using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    [SerializeField]
    private ListRabbitVariable rabbits;

    [SerializeField]
    private GameObject rabbitPrefab;

    [SerializeField]
    private float rabbitCount;

    private void Start()
    {
        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();

        for (int i = 0; i < rabbitCount; i++)
        {
            float direction = Random.Range(0f, 360f);

            Vector3 position = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            GameObject rabbit = Instantiate(rabbitPrefab, position, Quaternion.identity);
            rabbit.transform.SetParent(transform);
            rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());
        }
    }
}