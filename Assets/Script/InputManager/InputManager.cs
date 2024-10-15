using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] public Vector3 input;

    private static InputManager instance;
    public static InputManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        InputManager.instance = this;
    }

    private void FixedUpdate()
    {
        GetInput();
    }

    protected virtual void GetInput()
    {
        this.input.x = Input.GetAxis("Horizontal");
        this.input.y = Input.GetAxis("Vertical");
    }
}
