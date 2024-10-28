using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance;
    public static PlayerMovement Instance { get => instance; set => instance = value; }

    [SerializeField] private Vector3 move;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject model;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private bool moving = false;
    private float x, y;

    private void Awake()


    {
        PlayerMovement.instance = this;
        if (model != null)
        {
            animator = model.GetComponent<Animator>();
        }
        rb = GetComponentInParent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError(message: "Rigidbody2D is missing on parent!");
        }
    }

    private void Update()
    {
        GetInput();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        move = InputManager.Instance.Input.normalized;
        x = move.x;
        y = move.y;
    }

    private void Move()
    {
        if (move != Vector3.zero)
        {
            Vector2 targetPosition = rb.position + moveSpeed * Time.fixedDeltaTime * (Vector2)move;
            rb.MovePosition(targetPosition);
        }
    }

    private void Animate()
    {
        moving = move.magnitude > 0.1f;

        if (moving)
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);
        }
        animator.SetBool("Moving", moving);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Rabbit"))
    //    {
    //        Physics2D.IgnoreCollision(rb.GetComponent<Collider2D>(), collision.collider);
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green; // Màu của gizmos cho người chơi
    //    Gizmos.DrawWireSphere(transform.position, 5f); // Vẽ hình cầu xung quanh người chơi

    //}

}
