using UnityEngine;

public class Racket : MonoBehaviour
{
    [SerializeField]
    private GameObject activeRacket;

    [SerializeField]
    private Transform racketCollider;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }

        RacketRota();
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

    private void RacketRota()
    {
        if (Time.timeScale == 1f)
        {
            float temp = 0f;

            if (Input.GetAxis("Horizontal") < 0) temp = -180f;
            else temp = 0f;

            activeRacket.transform.rotation = Quaternion.Euler(0f, temp, 0f);
            racketCollider.transform.rotation = Quaternion.Euler(0f, temp, 0f);
        }
    }
}
