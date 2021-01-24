using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] private Transform _standPos; //will use @ top of ladder
    [SerializeField] private Transform _climbPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LadderChecker"))
        {
            Player player = other.transform.parent.GetComponent<Player>();

            if(player != null)
            {
                player.LadderClimb(_climbPos, this);
            }
        }
    }
    /*
    public Vector3 ClimbOffset()
    {
        return _climbPos.position;
    }*/

    public Vector3 StandOffset()
    {
        return _standPos.position;
    }
}
