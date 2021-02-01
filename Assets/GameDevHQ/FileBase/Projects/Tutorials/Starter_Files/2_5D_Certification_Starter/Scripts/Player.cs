using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _ladderSpd = 1.0f;
    [SerializeField] private float _jumpHeight = 20.0f;
    [SerializeField] private float _gravity = 0.5f;
    private bool _jumping = false;
    private bool _onLedge = false;
    private bool _climbing = false;
    private bool _climbNoInput = false;
    private float _yVelocity;
    private int _collectables = 0;
    private Vector3 _velocity;
    private Animator _anim;
    private CharacterController _controller;
    private LedgeGrab _activeLedge;
    private Ladder _activeLadder;
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
        if (!_climbing)
        {
            Movement();
        }
        else
        {
            LadderMovement();
        }
        

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
                
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    _anim.SetBool("Roll", true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumping = true;
                //_anim.SetBool("Jump", true);
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

    private void LadderMovement()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(0, verticalInput, 0);
        _velocity = direction * _ladderSpd;
        _anim.SetFloat("LadderSpeed", verticalInput);
        _controller.Move(_velocity * Time.deltaTime);

        if (_climbNoInput)
        {
            _anim.speed = 1.0f;
        }
        else
        {
            _anim.speed = Mathf.Abs(verticalInput);
        }

        if (_controller.isGrounded)
        {
            if (_climbing)
            {
                _anim.SetBool("ClimbingLadder", false);
                _climbing = false;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.DrawRay(hit.point, hit.normal, Color.green);
        //if (_controller.isGrounded)
        //{
            if (hit.transform.tag == "Ladder")
            {
                //float ladderJumpHeight = 0.125f;
                _climbing = true;
                //transform.position += Vector3.up; //* ladderJumpHeight;
                _anim.SetBool("ClimbingLadder", true);
            }
        //}
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

    public void Roll()
    {
        _anim.SetBool("Roll", false);
    }

    public void GrabLadder(Transform climbPos, Ladder currentLadder)
    {
        _controller.enabled = false;
        _anim.speed = 1.0f;
        _anim.SetBool("TopOfLadder", true);
        transform.position = climbPos.position;
        _activeLadder = currentLadder;
    }

    public void ClimbUpLadder()
    {
        transform.position = _activeLadder.StandOffset();
        _anim.SetBool("ClimbingLadder", false);
        _anim.SetBool("TopOfLadder", false);
        _climbing = false;
        _climbNoInput = false;
        _controller.enabled = true;
    }

    public void SetAnimSpd()
    {
        _climbNoInput = true;
    }

    public void PlayerCollectables()
    {
        _collectables++;
        _uiManager.UpdateCollectables(_collectables);
    }
}
