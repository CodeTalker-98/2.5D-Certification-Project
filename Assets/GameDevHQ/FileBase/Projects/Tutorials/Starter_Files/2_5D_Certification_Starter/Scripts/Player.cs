using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _jumpHeight = 20.0f;
    [SerializeField] private float _gravity = 0.5f;
    private bool _jumping = false;
    private bool _onLedge = false;
    private float _yVelocity;
    private int _collectables = 0;
    private Vector3 _velocity;
    private Animator _anim;
    private CharacterController _controller;
    private LedgeGrab _activeLedge;
    private UIManager _uiManager;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

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

        if (_onLedge)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetBool("ClimbUp", true);
                _onLedge = false;
            }
        }
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

    public void GrabLedge(Transform handPos, LedgeGrab currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("LedgeGrab", true);
        _anim.SetBool("Jump", false);
        _anim.SetFloat("Speed", 0.0f);
        _onLedge = true;
        transform.position = handPos.position;
        _activeLedge = currentLedge;
    }

    public void ClimbUp()
    {
        transform.position = _activeLedge.StandOffset();
        _anim.SetBool("LedgeGrab", false);
        _anim.SetBool("ClimbUp", false);
        _controller.enabled = true;
    }

    public void PlayerCollectables()
    {
        _collectables++;
        _uiManager.UpdateCollectables(_collectables);
    }
}
