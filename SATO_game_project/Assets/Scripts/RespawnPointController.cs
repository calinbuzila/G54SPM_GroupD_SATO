using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointController : MonoBehaviour {

	public GameObject playerObject;
	public Transform spawnPointTransform;

	public void Respawn()
	{
		playerObject.GetComponent<PlayerController>().enabled = true;
		Instantiate(playerObject, spawnPointTransform.position, spawnPointTransform.rotation);
	}
}
