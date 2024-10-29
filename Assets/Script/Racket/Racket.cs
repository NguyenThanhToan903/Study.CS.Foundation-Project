using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ActiveRacket activeRacket;
    [SerializeField] private Vector3 move;
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

    private void Update()
    {
        MouseFollowWithOffset();
        GetInput();
    }

    private void GetInput()
    {
        move = InputManager.Instance.Input.normalized;
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

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        float temp = 0f;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerMovement.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //if (mousePos.x < playerScreenPoint.x)
        //{
        //    activeRacket.transform.rotation = Quaternion.Euler(0f, -180f, angle);
        //}
        //else
        //{
        //    activeRacket.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //}
        if (move.x < 0) temp = -180f;
        else temp = 0f;
        activeRacket.transform.rotation = Quaternion.Euler(0f, temp, angle);
        racketCollider.transform.rotation = Quaternion.Euler(0f, temp, angle);

    }

}
