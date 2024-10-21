using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector3 move;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject model;

    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private bool moving = false;
    private float x, y;

    private void Awake()
    {
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
        if (move.magnitude > 0.1f || move.magnitude < -0.1f)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        if (moving)
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);
        }
        animator.SetBool("Moving", moving);
    }
}
