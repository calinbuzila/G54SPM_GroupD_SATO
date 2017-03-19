using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShotController : MonoBehaviour {

	public int speed;
	protected Rigidbody bulletRigidBody;

	void Start ()
	{
		bulletRigidBody = GetComponent<Rigidbody> ();	
		// Note the bullet moves left, thus minus right.
		bulletRigidBody.velocity = -(transform.right * speed);
	}
}
