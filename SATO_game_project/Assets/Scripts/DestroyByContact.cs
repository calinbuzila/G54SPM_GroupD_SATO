using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Collider>().name == "Enemy") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
