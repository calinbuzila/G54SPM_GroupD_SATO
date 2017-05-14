using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoundary : MonoBehaviour
{
    /// <summary>
    /// Destroys object that passes the main boundary
    /// </summary>
    /// <param name="other"> Collider objects from scene that come into contact with the main boundary</param>
    void OnTriggerExit(Collider other)
    {
		// TODO TEMPORARY, REMOVE ONCE RESPAWN FOR NON-PLAYERKILLED KAMIKAZES IS RESOLVED!
		if (other.GetComponent<Collider> ().name == "Enemy")
		{
			EnemyController.IncrementPlayerKills();
		}
        Debug.Log("Entity destroyed by MainBoundary.");
        Destroy(other.gameObject);
    }
}
