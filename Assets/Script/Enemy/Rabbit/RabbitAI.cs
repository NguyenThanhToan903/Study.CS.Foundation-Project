using System.Collections;
using UnityEngine;

public class RabbitAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State state;

    private RabbitPathfinding rabbitPathfinding;

    [SerializeField] private Boundary boundary;
    [SerializeField] private float roamDuration = 2f;
    [SerializeField] private float stopDuration = 1f;

    private void Awake()
    {
        rabbitPathfinding = GetComponent<RabbitPathfinding>();
        state = State.Roaming;
    }

    public void StartRoaming()
    {
        StartCoroutine(RoamingRoutine());
    }

    //private IEnumerator RoamingRoutine()
    //{
    //    while (state == State.Roaming)
    //    {
    //        Vector2 roamPosition = GetRoamingPosition();
    //        rabbitPathfinding.MoveTo(roamPosition);
    //        yield return new WaitForSeconds(roamDuration);

    //        rabbitPathfinding.MoveTo(Vector2.zero);
    //        yield return new WaitForSeconds(stopDuration);
    //    }
    //}

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {

            Vector2 roamPosition = GetRoamingPosition();
            rabbitPathfinding.MoveTo(roamPosition);

            yield return new WaitForSeconds(roamDuration);

            rabbitPathfinding.StopMovement();

            yield return new WaitForSeconds(stopDuration);
        }
    }


    private Vector2 GetRoamingPosition()
    {
        float x = Random.Range(boundary.PointA.x, boundary.PointB.x);
        float y = Random.Range(boundary.PointA.y, boundary.PointB.y);
        return new Vector2(x, y);
    }
}
