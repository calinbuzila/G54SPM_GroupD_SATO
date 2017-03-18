using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	protected LevelController levelController;

    void Start()
    {
        levelController = GameObject.FindObjectOfType(typeof(LevelController)) as LevelController;
    }

    void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Collider>().name == "Enemy") 
		{
            if(other.CompareTag(gameObject.tag))
            {
                levelController.IncrementScore();
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            else
            {
                levelController.AddToHealth(-10);
                Destroy(gameObject);
            }
		}
	}
}
