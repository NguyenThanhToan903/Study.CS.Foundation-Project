using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 moveInput;
    [SerializeField] private Vector2 moveDir;


    private void Update()
    {
        Move();
    }
    
    private void Move()
    {
        moveInput = InputManager.Instance.input.normalized;
        transform.position += moveSpeed * Time.deltaTime * moveInput;
    }
}
