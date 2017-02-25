using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
	public float speed;
	protected const Rigidbody BULLET_RB = GetComponent<Rigidbody>();

	void Start () {
		
	}

	//TODO Handle collision with enemy
	//TODO Handle collision with barrier
	//TODO Handle moving on spawn
}
