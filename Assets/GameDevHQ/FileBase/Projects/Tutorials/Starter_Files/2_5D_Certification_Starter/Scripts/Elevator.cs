using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _waitTime = 5.0f;
    [SerializeField] private float _spd = 5.0f;
    [SerializeField] private Transform _targetA, _targetB;
    private bool _goingDown = false;
    private bool _callElevator = false;
    private bool _sendElevator = false;

    private void Start()
    {
        transform.position = _targetA.position;    
    }

    private void FixedUpdate()
    {
        if (_sendElevator)
        {
            if (_goingDown)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _spd * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _spd * Time.deltaTime);
            }
            
            if(transform.position == _targetA.position || transform.position == _targetB.position)
            {
                _sendElevator = false;
            }
        }
    }
    public void MoveElevator()
    {
        _goingDown = !_goingDown;
        if (_callElevator)
        {
            StartCoroutine(ElevatorWait(_waitTime));
        }
    }

    IEnumerator ElevatorWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _sendElevator = true;
        _callElevator = false;
    }

    public void ReadyElevator()
    {
        _callElevator = true;
    }
}
