using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private ActiveRacket activeRacket;

    [SerializeField]
    private Transform racketCollider;

    [SerializeField] private Vector3 input;

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
        input = InputManager.Instance.Input.normalized;
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
        if (Time.timeScale == 1f)
        {
            //Vector3 mousePos = Input.mousePosition;
            float temp = 0f;
            //Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerMovement.transform.position);

            //float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

            if (input.x < 0) temp = -180f;
            else temp = 0f;
            activeRacket.transform.rotation = Quaternion.Euler(0f, temp, 0f);
            racketCollider.transform.rotation = Quaternion.Euler(0f, temp, 0f);
        }
    }
}
