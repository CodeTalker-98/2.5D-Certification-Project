using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] private bool _enableElevator = false;
    private Elevator _elevator;
    private MeshRenderer _renderer;

    private void Start()
    {
        _elevator = GetComponent<Elevator>();
        _renderer = GameObject.Find("Elevator_Panel").GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(_enableElevator && Input.GetKeyDown(KeyCode.E))
        {            
            if(_elevator != null)
            {
                _elevator.ReadyElevator();
                _elevator.MoveElevator();
            }
            
            if(_renderer != null)
            {
                _renderer.material.color = Color.green;
            }
        }
        if (!_enableElevator)
        {
            _renderer.material.color = Color.red;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collided with Player");
            _enableElevator = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _enableElevator = false;
        }
    }
}
