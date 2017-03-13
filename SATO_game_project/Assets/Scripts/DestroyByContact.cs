using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	protected LevelController levelController;

	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Collider>().name == "Enemy") 
		{
            Destroy(gameObject);
            levelController = GameObject.FindObjectOfType(typeof(LevelController)) as LevelController;

            levelController.IncrementScore();
            levelController.AddToHealth(-10);
            Destroy(other.gameObject);
		}
	}
}
