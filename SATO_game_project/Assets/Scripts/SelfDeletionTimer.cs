using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDeletionTimer : MonoBehaviour {

	// Things this script are attached to will survive for five seconds presently.
	protected const int deletionTimer = 5;
	protected float timer;

	void Update () {
		timer += 1.0f * Time.deltaTime;
		if (timer >= deletionTimer)
		{
			Destroy (gameObject);
		}
	}
}
