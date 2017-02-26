using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
	public float speed;
	protected Rigidbody _bulletRB;

	void Start ()
	{
		_bulletRB = GetComponent<Rigidbody> ();
		_bulletRB.velocity = transform.right * speed;
	}

	//TODO Handle collision with enemy
	//TODO Handle collision with barrier
	//TODO Handle moving on spawn
}
