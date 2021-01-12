using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpHeight = 20.0f;
    [SerializeField] private float _gravity = 0.5f;
    private float _yVelocity;
    private Vector3 _velocity;
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("The CHARACTER CONTROLLER is NULL");
        }
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (_controller.isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 direction = new Vector3(0,0, horizontalInput);
            _velocity = direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
            }
        }
        else
        {            
            _yVelocity -= _gravity;            
        }
        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
