//using System.Collections;
//using UnityEngine;

//public class RabbitAI : MonoBehaviour
//{
//    private enum State
//    {
//        Roaming
//    }

//    private State state;
//    private RabbitPathfinding rabbitPathfinding;
//    [SerializeField] private Boundary boundary;
//    [SerializeField] private float roamDuration = 2f;
//    [SerializeField] private float stopDuration = 1f;
//    [SerializeField] private float detectionRadius = 2f;
//    [SerializeField] private float fieldOfView = 270f;

//    private void Awake()
//    {
//        rabbitPathfinding = GetComponent<RabbitPathfinding>();
//        state = State.Roaming;
//    }

//    public void StartRoaming()
//    {
//        StartCoroutine(RoamingRoutine());
//    }

//    private IEnumerator RoamingRoutine()
//    {
//        while (state == State.Roaming)
//        {
//            Vector2 roamPosition = GetRoamingPosition();
//            rabbitPathfinding.MoveTo(roamPosition);
//            yield return new WaitForSeconds(roamDuration);

//            AvoidOtherRabbits();
//            yield return new WaitForSeconds(stopDuration);
//        }
//    }

//    private Vector2 GetRoamingPosition()
//    {
//        float x = Random.Range(boundary.PointA.x, boundary.PointB.x);
//        float y = Random.Range(boundary.PointA.y, boundary.PointB.y);
//        return new Vector2(x, y);
//    }

//    private void AvoidOtherRabbits()
//    {
//        Collider2D[] nearbyRabbits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
//        foreach (Collider2D otherRabbit in nearbyRabbits)
//        {
//            if (otherRabbit.gameObject != gameObject)
//            {
//                Vector2 directionToOtherRabbit = otherRabbit.transform.position - transform.position;
//                float angleToOtherRabbit = Vector2.Angle(transform.up, directionToOtherRabbit);

//                if (angleToOtherRabbit < fieldOfView / 2f)
//                {
//                    Vector2 avoidanceDir = (transform.position - otherRabbit.transform.position).normalized;
//                    rabbitPathfinding.MoveTo(avoidanceDir * roamDuration);
//                }
//            }
//        }
//    }

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, detectionRadius);

//        Collider2D[] nearbyRabbits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
//        foreach (Collider2D otherRabbit in nearbyRabbits)
//        {
//            if (otherRabbit.gameObject != gameObject)
//            {
//                Gizmos.color = Color.blue;
//                Gizmos.DrawLine(transform.position, otherRabbit.transform.position);
//            }
//        }
//    }
//}


using System.Collections;
using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Stopped
    }

    private State state;
    private RabbitPathfinding rabbitPathfinding;
    [SerializeField] private Boundary boundary;
    [SerializeField] private float roamDuration = 2f;
    [SerializeField] private float stopDuration = 1f;
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private float fieldOfView = 270f;

    private void Awake()
    {
        rabbitPathfinding = GetComponent<RabbitPathfinding>();
        state = State.Roaming;
    }

    public void StartRoaming()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (true)
        {
            if (state == State.Roaming)
            {
                Vector2 roamPosition = GetRoamingPosition();
                rabbitPathfinding.MoveTo(roamPosition);
                yield return new WaitForSeconds(roamDuration);

                rabbitPathfinding.MoveTo(Vector2.zero);
                state = State.Stopped;
                yield return new WaitForSeconds(stopDuration);

                state = State.Roaming;
            }
        }
    }

    private Vector2 GetRoamingPosition()
    {
        float x = Random.Range(boundary.PointA.x, boundary.PointB.x);
        float y = Random.Range(boundary.PointA.y, boundary.PointB.y);
        return new Vector2(x, y);
    }

    private void AvoidOtherRabbits()
    {
        Collider2D[] nearbyRabbits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D otherRabbit in nearbyRabbits)
        {
            if (otherRabbit.gameObject != gameObject)
            {
                Vector2 directionToOtherRabbit = otherRabbit.transform.position - transform.position;
                float angleToOtherRabbit = Vector2.Angle(transform.up, directionToOtherRabbit);

                if (angleToOtherRabbit < fieldOfView / 2f)
                {
                    Vector2 avoidanceDir = (transform.position - otherRabbit.transform.position).normalized;
                    rabbitPathfinding.MoveTo(avoidanceDir * roamDuration);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Collider2D[] nearbyRabbits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D otherRabbit in nearbyRabbits)
        {
            if (otherRabbit.gameObject != gameObject)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position, otherRabbit.transform.position);
            }
        }
    }
}
