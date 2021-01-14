using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpHeight = 20.0f;
    [SerializeField] private float _gravity = 0.5f;
    private bool _jumping = false;
    private float _yVelocity;
    private Vector3 _velocity;
    private Animator _anim;
    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();

        if (_controller == null)
        {
            Debug.LogError("The CHARACTER CONTROLLER is NULL");
        }

        if (_anim == null)
        {
            Debug.LogError("The PLAYER ANIMATOR is NULL");
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
            if (_jumping)
            {
                _anim.SetBool("Jump", false);
                _jumping = false;
            }

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            Vector3 direction = new Vector3(0,0, horizontalInput);
            _velocity = direction * _speed;
            _anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

            if (horizontalInput != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _velocity.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumping = true;
                _anim.SetBool("Jump", true);
                _yVelocity = _jumpHeight;
                _anim.SetBool("Jump", true);
            }
        }
        else
        {            
            _yVelocity -= _gravity;
        }
        _velocity.y = _yVelocity;
        _controller.Move(_velocity * Time.deltaTime);
    }

    public void GrabLedge(Vector3 handPos)
    {
        _controller.enabled = false;
        _anim.SetBool("LedgeGrab", true);
        _anim.SetBool("Jump", false);
        _anim.SetFloat("Speed", 0.0f);
        transform.position = handPos;
    }
}
