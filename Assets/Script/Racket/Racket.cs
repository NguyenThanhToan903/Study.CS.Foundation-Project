using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private Transform racketCollider;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void Start()
    {
        playerInput.Combat.Catch.started += _ => Attack();
    }

    private void Attack()
    {
        animator.SetTrigger("Catching");
        racketCollider.gameObject.SetActive(true);
    }

    private void DoneAttackEvent()
    {
        racketCollider.gameObject.SetActive(false);
    }
}
