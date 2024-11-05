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


    private void Awake()
    {
        rabbitPathfinding = GetComponent<RabbitPathfinding>();
        state = State.Roaming;
    }

    public void StartRoaming()
    {
        StartCoroutine(RoamingRountine());
    }

    private IEnumerator RoamingRountine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            rabbitPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

}
