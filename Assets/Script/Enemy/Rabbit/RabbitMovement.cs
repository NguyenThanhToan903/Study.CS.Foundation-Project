using System.Collections;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    public Vector2 Velocity { private set; get; }
    [SerializeField] private GameObject model;
    private Transform modelTransform;
    private Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float moveDuration = 1f;
    private float stopDuration;

    private void Start()
    {
        modelTransform = model != null ? model.transform : null;
        if (modelTransform == null)
        {
            Debug.LogError("Model object not found!");
        }

        animator = model.GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on model!");
        }

        rb = GetComponentInParent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing on parent!");
        }

        StartCoroutine(MoveAndStopCycle());
    }

    private IEnumerator MoveAndStopCycle()
    {
        while (true)
        {
            SetRandomDirection();

            float elapsedTime = 0f;
            animator.SetBool("RabbitIsMoving", true);
            while (elapsedTime < moveDuration)
            {
                // Di chuyển con thỏ mà không bị đẩy ra
                Vector2 targetPosition = rb.position + Velocity * speed * Time.fixedDeltaTime;
                rb.MovePosition(targetPosition);

                // Xoay hướng model dựa trên Velocity.x
                if (Velocity.x < 0)
                {
                    modelTransform.localScale = new Vector3(-1, 1, 1);
                }
                else if (Velocity.x > 0)
                {
                    modelTransform.localScale = new Vector3(1, 1, 1);
                }

                elapsedTime += Time.deltaTime;
                yield return new WaitForFixedUpdate();
            }

            // Thiết lập thời gian dừng ngẫu nhiên từ 1-3 giây
            stopDuration = Random.Range(1f, 3f);
            Velocity = Vector2.zero;
            animator.SetBool("RabbitIsMoving", false);
            yield return new WaitForSeconds(stopDuration);
        }
    }

    private void SetRandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);
        Velocity = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Ngăn không cho con thỏ bị đẩy ra khi va chạm với người chơi
            Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), collision.collider);
        }
    }
}
