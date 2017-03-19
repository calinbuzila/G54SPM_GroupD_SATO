using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
	public Boundary PlayerBoundary;

	public ColourController colourController;
    public Rigidbody playerRigidBody;
    public GameObject shot;
    public Transform shotSpawn;
	public float fireRate;

	protected float nextFire;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
		colourController = GameObject.FindObjectOfType<ColourController>();
        colourController.AssignColour(shot);
    }

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
        if (Input.GetKeyDown (KeyCode.E))
        {
            colourController.CycleToNextColour(shot);
        }
        if (Input.GetKeyDown (KeyCode.Q))
        {
            colourController.CycleToPreviousColour(shot);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movementVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
        playerRigidBody.velocity = movementVector * PlayerSpeed;
        playerRigidBody.position = new Vector3
        (
            Mathf.Clamp(playerRigidBody.position.x, PlayerBoundary.xMin, PlayerBoundary.xMax),
            0.0f,
            Mathf.Clamp(playerRigidBody.position.z, PlayerBoundary.zMin, PlayerBoundary.zMax)
        );
        playerRigidBody.freezeRotation = true;
    }

	public void KillPlayer()
	{
		Destroy (gameObject);
	}
}

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}