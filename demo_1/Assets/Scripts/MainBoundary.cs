﻿using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoundary : MonoBehaviour {
    /// <summary>
    /// Destroys object that passes the main boundary
    /// </summary>
    /// <param name="other"> Collider objects from scene that come into contact with the main boundary</param>
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Destroy_MainBoundary_Destroyed!!!");
        Destroy(other.gameObject);
        if (other.GetComponent<Collider>().name == "Enemy")
        {
            Enemy enemyModel = new Enemy();

        }
    }
}
