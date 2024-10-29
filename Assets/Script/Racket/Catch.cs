using UnityEngine;

public class Catch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<RabbitMovement>())
        {
            Debug.Log("AAA");
        }
    }

}
