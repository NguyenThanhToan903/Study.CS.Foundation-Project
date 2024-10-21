using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public GameObject rabbitPrefab;  // Prefab của con thỏ
    public int numberOfRabbits = 5;  // Số lượng thỏ spawn
    public Vector2 spawnAreaMin;  // Điểm bắt đầu của vùng spawn (x, y)
    public Vector2 spawnAreaMax;  // Điểm kết thúc của vùng spawn (x, y)
    public Color gizmoColor = Color.green;

    void Start()
    {
        // Spawn số lượng thỏ quy định
        for (int i = 0; i < numberOfRabbits; i++)
        {
            SpawnRabbit();
        }
    }

    void SpawnRabbit()
    {
        GameObject rabbit = Instantiate(rabbitPrefab);
        RabbitAI rabbitAI = rabbit.GetComponent<RabbitAI>();

        // Thiết lập vùng spawn cho thỏ
        rabbitAI.SetSpawnArea(spawnAreaMin, spawnAreaMax);

        // Tạo vị trí spawn ngẫu nhiên trong vùng spawn
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnAreaMin.x, spawnAreaMax.x),
            Random.Range(spawnAreaMin.y, spawnAreaMax.y)
        );

        // Instantiate con thỏ tại vị trí spawn
        rabbit.transform.position = spawnPosition;  // Gán vị trí spawn cho thỏ
        rabbit.transform.parent = this.transform;  // Gán RabbitSpawner làm parent của thỏ
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        // Tính toán kích thước của vùng spawn
        Vector3 spawnAreaSize = new Vector3(spawnAreaMax.x - spawnAreaMin.x, spawnAreaMax.y - spawnAreaMin.y, 0);
        Vector3 spawnAreaCenter = new Vector3((spawnAreaMin.x + spawnAreaMax.x) / 2, (spawnAreaMin.y + spawnAreaMax.y) / 2, 0);

        // Vẽ hình chữ nhật biểu thị vùng spawn
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
