using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Transform _standPos; //will use @ top of ladder
    [SerializeField] private Transform _climbPos; //Use to get on the Ladder

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LedgeGrabChecker"))
        {
            Player player = other.transform.parent.GetComponent<Player>();

            if(player != null)
            {
                player.GrabLadder(_climbPos, this);
                player.SetAnimSpd();
            }
        }
    }

    public Vector3 StandOffset()
    {
        return _standPos.position;
    }
}
