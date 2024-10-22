using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    public Vector2 Velocity { private set; get; }
    [SerializeField] private GameObject model;
    private Transform modelTransform;

    void Start()
    {
        float randomAngle = Random.Range(0f, 360f);
        Velocity = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        modelTransform = model.transform;

        if (modelTransform == null)
        {
            Debug.LogError("Model object not found!");
        }
    }

    private void FixedUpdate()
    {
        transform.parent.position += (Vector3)Velocity * Time.fixedDeltaTime;

        if (Velocity.x != 0)
        {
            if (Velocity.x < 0)
            {
                modelTransform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                modelTransform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
