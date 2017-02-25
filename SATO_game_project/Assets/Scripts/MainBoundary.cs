using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoundary : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Destroys object that passes the main boundary
    /// </summary>
    /// <param name="other"> Collider objects from scene that come into contact with the main boundary</param>
    void OnTriggerExit(Collider other)
    {
        Debug.Log("Destroy_MainBoundaryExceeded!!!");
        Destroy(other.gameObject);
    }
}
