using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour
{
    //[SerializeField] private Vector3 _handOffset, _standOffset;
    [SerializeField] private Transform _handPos, _standPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            Player player = other.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.GrabLedge(_handPos, this);
            }
        }
    }

    public Vector3 StandOffset()
    {
        return _standPos.position;
    }
}
