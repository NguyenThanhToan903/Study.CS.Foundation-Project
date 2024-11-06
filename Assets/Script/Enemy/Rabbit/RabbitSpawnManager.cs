//using System.Collections.Generic;
//using UnityEngine;

//public class RabbitSpawnManager : MonoBehaviour
//{
//    [SerializeField] private GameObject rabbitPrefab;
//    [SerializeField] private int rabbitCount = 5;
//    [SerializeField] private Boundary boundary;

//    private List<GameObject> rabbitList;

//    private void Start()
//    {
//        rabbitList = new List<GameObject>();
//        SpawnRabbits();
//    }

//    private void SpawnRabbits()
//    {
//        for (int i = 0; i < rabbitCount; i++)
//        {
//            Vector2 spawnPosition = new Vector2(
//                Random.Range(boundary.PointA.x, boundary.PointB.x),
//                Random.Range(boundary.PointA.y, boundary.PointB.y)
//            );

//            GameObject rabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);
//            rabbitList.Add(rabbit);

//            RabbitAI rabbitAI = rabbit.GetComponent<RabbitAI>();
//            rabbitAI.StartRoaming();

//            rabbit.GetComponent<RabbitPathfinding>().SetBoundary(boundary);
//        }
//    }
//}


using UnityEngine;

public class RabbitSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject rabbitPrefab;
    [SerializeField] private int rabbitCount = 5;
    [SerializeField] private Boundary boundary;
    [SerializeField] private ListRabbitVariable listRabbitVariable;

    private void Start()
    {
        SpawnRabbits();
    }

    //private void SpawnRabbits()
    //{
    //    for (int i = 0; i < rabbitCount; i++)
    //    {
    //        Vector2 spawnPosition = new Vector2(
    //            Random.Range(boundary.PointA.x, boundary.PointB.x),
    //            Random.Range(boundary.PointA.y, boundary.PointB.y)
    //        );

    //        GameObject rabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);

    //        RabbitAI rabbitAI = rabbit.GetComponent<RabbitAI>();
    //        rabbitAI.StartRoaming();

    //        RabbitPathfinding rabbitPathfinding = rabbit.GetComponent<RabbitPathfinding>();
    //        rabbitPathfinding.SetBoundary(boundary);

    //        RabbitMovement rabbitMovement = rabbit.GetComponent<RabbitMovement>();
    //        listRabbitVariable.rabbitMovement.Add(rabbitMovement);
    //    }
    //}

    private void SpawnRabbits()
    {
        for (int i = 0; i < rabbitCount; i++)
        {
            Vector2 spawnPosition = new Vector2(
                Random.Range(boundary.PointA.x, boundary.PointB.x),
                Random.Range(boundary.PointA.y, boundary.PointB.y)
            );

            GameObject rabbit = Instantiate(rabbitPrefab, spawnPosition, Quaternion.identity);

            RabbitAI rabbitAI = rabbit.GetComponent<RabbitAI>();
            rabbitAI.StartRoaming();

            RabbitPathfinding rabbitPathfinding = rabbit.GetComponent<RabbitPathfinding>();
            rabbitPathfinding.SetBoundary(boundary);

            // Add the RabbitPathfinding component to the ListRabbitVariable
            listRabbitVariable.rabbitPathfinding.Add(rabbitPathfinding);  // Add the RabbitPathfinding to the list
        }
    }
}
