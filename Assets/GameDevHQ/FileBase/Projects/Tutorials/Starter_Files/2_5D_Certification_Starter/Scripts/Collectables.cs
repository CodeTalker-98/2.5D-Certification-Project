﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponentInChildren<Player>();
            player.PlayerCollectables();
            Destroy(this.gameObject);
        }
    }
}
