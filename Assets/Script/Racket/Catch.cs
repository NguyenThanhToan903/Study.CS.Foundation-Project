using UnityEngine;

public class Catch : MonoBehaviour
{
    [SerializeField]
    private ScoreSystem scoreSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<RabbitMovement>())
        {
            scoreSystem.takeScore();
            Destroy(other.gameObject);
        }
    }
}
