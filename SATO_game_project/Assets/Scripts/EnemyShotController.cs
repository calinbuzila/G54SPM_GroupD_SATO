using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour {

	public int speed;
	protected Rigidbody bulletRigidBody;
	protected LevelController levelController;
	protected MainController mainController;

	void Start ()
	{
		levelController = GameObject.FindObjectOfType (typeof(LevelController)) as LevelController;
		mainController = GameObject.FindObjectOfType (typeof(MainController)) as MainController;
		bulletRigidBody = GetComponent<Rigidbody> ();
		// Note the bullet moves left, thus minus right.
		bulletRigidBody.velocity = -(transform.right * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Destroy (gameObject);
			levelController.AddToHealth (-10 * (mainController.GameDifficulty + 1));
		}
	}
}