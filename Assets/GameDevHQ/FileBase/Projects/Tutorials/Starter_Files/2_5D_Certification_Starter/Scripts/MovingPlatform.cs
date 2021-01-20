using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _spd = 5.0f;
    [SerializeField] private Transform _targetA, _targetB;
    private bool _switch = false;

    private void Start()
    {
        transform.position = _targetA.position;
    }

    private void FixedUpdate()
    {
        if(transform.position == _targetA.position)
        {
            _switch = false;
        }
        if(transform.position == _targetB.position)
        {
            _switch = true;
        }

        if (_switch)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _spd * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _spd * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
