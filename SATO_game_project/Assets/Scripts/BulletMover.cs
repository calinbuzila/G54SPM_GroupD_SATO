using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
	public float speed;
	protected Rigidbody bulletRigidBody;

	void Start ()
	{
		bulletRigidBody = GetComponent<Rigidbody> ();
		bulletRigidBody.velocity = transform.forward * speed;
	}
}
