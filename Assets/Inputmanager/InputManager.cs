using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance { get => instance; set => instance = value; }

    private Vector3 input;
    public Vector3 Input { get => input; set => input = value; }
    private void Awake()
    {
        InputManager.instance = this;
    }

    private void Start()
    {
        InputManager.instance = this;
    }

    private void FixedUpdate()
    {
        this.GetInput();
    }

    private void GetInput()
    {
        this.input.x = UnityEngine.Input.GetAxis("Horizontal");
        this.input.y = UnityEngine.Input.GetAxis("Vertical");
    }
}
