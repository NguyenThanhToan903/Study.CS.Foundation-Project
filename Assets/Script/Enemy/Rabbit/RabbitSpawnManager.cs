////using UnityEngine;

////public class RabbitSpawnManager : MonoBehaviour
////{
////    [SerializeField]
////    private ListRabbitVariable rabbits;

////    [SerializeField]
////    private GameObject rabbitPrefab;

////    [SerializeField]
////    private float rabbitCount;

////    [SerializeField]
////    private Boundary boundary;

////    private void Start()
////    {
////        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();

////        boundary.Center = transform.position;
////        Vector2 randomPoint;
////        for (int i = 0; i < rabbitCount; i++)
////        {
////            randomPoint.x = Random.Range(boundary.PointA.x, boundary.PointB.x);
////            randomPoint.y = Random.Range(boundary.PointA.y, boundary.PointB.y);
////            Vector3 spawnPosition = (Vector3)randomPoint + (Vector3)boundary.Center;
////            GameObject rabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);
////            rabbit.transform.SetParent(transform);
////            rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());

////            rabbit.GetComponent<RabbitMovement>().SetBoundary(boundary);
////        }
////    }

////    private void FixedUpdate()
////    {
////        boundary.Center = transform.position;
////    }

////    private void OnDrawGizmosSelected()
////    {
////        Gizmos.color = Color.yellow;


////        Vector3 center = (boundary.PointA + boundary.PointB) / 2;
////        Vector3 size = new Vector3(Mathf.Abs(boundary.PointA.x - boundary.PointB.x), Mathf.Abs(boundary.PointA.y - boundary.PointB.y), 0);


////        Gizmos.DrawWireCube(center, size);
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
//    private float rabbitCount;

//    [SerializeField]
//    private Boundary boundary;

//    private void Start()
//    {
//        if (rabbits.rabbitMovement.Count > 0) rabbits.rabbitMovement.Clear();

//        //boundary.Center = transform.position;
//        Vector2 randomPoint;
//        for (int i = 0; i < rabbitCount; i++)
//        {
//            randomPoint.x = Random.Range(boundary.PointA.x, boundary.PointB.x);
//            randomPoint.y = Random.Range(boundary.PointA.y, boundary.PointB.y);
//            Vector3 spawnPosition = (Vector3)randomPoint + (Vector3)boundary.Center;
//            GameObject rabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);
//            rabbit.transform.SetParent(transform);
//            rabbits.rabbitMovement.Add(rabbit.GetComponent<RabbitMovement>());

//            rabbit.GetComponent<RabbitMovement>().SetBoundary(boundary);
//        }
//    }

//    private void FixedUpdate()
//    {
//        boundary.Center = transform.position;
//    }

//    private void OnDrawGizmosSelected()
//    {
//        Gizmos.color = Color.yellow;

//        Vector3 adjustedCenter = (Vector3)(boundary.PointA + boundary.PointB) / 2 + (Vector3)boundary.Center;
//        Vector3 size = (boundary.PointB - boundary.PointA) / 2;

//        Gizmos.DrawWireCube(adjustedCenter, new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), 0));
//    }
//}


using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject rabbitPrefab;
    [SerializeField] private int rabbitCount = 5;
    [SerializeField] private Boundary boundary;

    private List<GameObject> rabbitList;

    private void Start()
    {
        rabbitList = new List<GameObject>();
        SpawnRabbits();
    }

    private void SpawnRabbits()
    {
        for (int i = 0; i < rabbitCount; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(boundary.PointA.x, boundary.PointB.x),
                Random.Range(boundary.PointA.y, boundary.PointB.y)
            );

            GameObject rabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);
            rabbitList.Add(rabbit);

            RabbitAI rabbitAI = rabbit.GetComponent<RabbitAI>();
            rabbitAI.StartRoaming();

            rabbit.GetComponent<RabbitPathfinding>().SetBoundary(boundary);
        }
    }
}
