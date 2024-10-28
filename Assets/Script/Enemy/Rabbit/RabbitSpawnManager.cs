////using UnityEngine;

////public class RabbitSpawnManager : MonoBehaviour
////{
////    [SerializeField]
////    private ListRabbitVariable rabbits;

////    [SerializeField]
////    private GameObject rabbitPrefab;

////    [SerializeField]
////    private float rabbitCount;

////    private void Start()
////    {
////        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();

////        for (int i = 0; i < rabbitCount; i++)
////        {
////            float direction = Random.Range(0f, 360f);

////            Vector3 position = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
////            GameObject rabbit = Instantiate(rabbitPrefab, position, Quaternion.identity);
////            rabbit.transform.SetParent(transform);
////            rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());
////        }
////    }
////}


//using UnityEngine;

//public class RabbitSpawnManager : MonoBehaviour
//{
//    [SerializeField]
//    private ListRabbitVariable rabbits;

//    [SerializeField]
//    private GameObject rabbitPrefab;

//    [SerializeField]
//    private int rabbitCount;

//    [SerializeField]
//    private float spawnRadius = 10f;

//    private void Start()
//    {
//        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();

//        for (int i = 0; i < rabbitCount; i++)
//        {
//            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
//            float radius = Random.Range(0f, spawnRadius);

//            Vector3 position = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
//            position += transform.position;

//            GameObject rabbit = Instantiate(rabbitPrefab, position, Quaternion.identity);
//            rabbit.transform.SetParent(transform);
//            rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.cyan;
//        Gizmos.DrawWireSphere(transform.position, spawnRadius);
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

    [SerializeField]
    private Boundary boundary;

    private void Start()
    {
        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();


        boundary.Center = transform.position;

        for (int i = 0; i < rabbitCount; i++)
        {
            // Tạo vị trí spawn nằm trong vùng boundary
            Vector2 randomPoint = Random.insideUnitCircle * boundary.Radius;
            Vector3 spawnPosition = new Vector3(randomPoint.x, randomPoint.y, 0) + boundary.Center;
            GameObject rabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);
            rabbit.transform.SetParent(transform);
            rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());

            // Gán vùng giới hạn cho RabbitMovement
            rabbit.GetComponent<RabbitMovement>().SetBoundary(boundary);
        }
    }

    private void FixedUpdate()
    {
        boundary.Center = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ vùng giới hạn hình tròn
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(boundary.Center, boundary.Radius); // Vẽ tại vị trí tâm
    }
}
