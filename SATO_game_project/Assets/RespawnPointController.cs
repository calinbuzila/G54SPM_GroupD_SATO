using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointController : MonoBehaviour {

	protected LevelController levelController;
	public GameObject player;
	public Transform spawnPointTransform;

	void Start ()
	{
		levelController = GameObject.FindObjectOfType<LevelController> ();
	}

	void Update () 
	{
		if (levelController.GetHealth () == 0)
		{
			Instantiate (player, spawnPointTransform.position, spawnPointTransform.rotation);
		}
	}
}
