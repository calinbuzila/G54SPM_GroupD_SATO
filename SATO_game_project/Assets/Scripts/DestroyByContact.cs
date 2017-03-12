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
            if (gameObject.CompareTag("RedBullet"))
            {
                levelController.IncrementScore();
                Destroy(other.gameObject);
            }
            else if (gameObject.CompareTag("YellowBullet"))
            {
                levelController.DecrementScore();
            } 
		}
	}
}
