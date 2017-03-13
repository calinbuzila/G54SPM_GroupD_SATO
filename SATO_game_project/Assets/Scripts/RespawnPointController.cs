using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointController : MonoBehaviour {

	protected LevelController levelController;
	public GameObject playerObject;
	public Transform spawnPointTransform;

	void Start ()
	{
		levelController = GameObject.FindObjectOfType<LevelController> ();
	}

	public void Respawn()
	{
		Instantiate (playerObject, spawnPointTransform.position, spawnPointTransform.rotation);
	}
}
