using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
    public Boundary PlayerBoundary;

    public Rigidbody Player_rb;
    public GameObject shot;
    public Transform shotSpawn;
	public float fireRate;

	protected float _nextFire;

	public ColourController colourController;

    void Start()
    {
        Player_rb = GetComponent<Rigidbody>();
		colourController = GameObject.FindObjectOfType<ColourController>();
		colourController.AssignColour (shot.GetComponent<Renderer> (), colourController.redMaterial);

    }

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > _nextFire)
		{
			_nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}

		if (Input.GetKeyDown (KeyCode.E)) {

			if (shot.GetComponent<Renderer> ().sharedMaterial == colourController.redMaterial) {
				colourController.AssignColour (shot.GetComponent<Renderer> (), colourController.yellowMaterial);

			} else if (shot.GetComponent<Renderer> ().sharedMaterial == colourController.yellowMaterial) {
				colourController.AssignColour (shot.GetComponent<Renderer> (), colourController.redMaterial);

			}
				

		}


    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movementVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Player_rb.velocity = movementVector * PlayerSpeed;
        Player_rb.position = new Vector3
        (
            Mathf.Clamp(Player_rb.position.x, PlayerBoundary.xMin, PlayerBoundary.xMax),
            0.0f,
            Mathf.Clamp(Player_rb.position.z, PlayerBoundary.zMin, PlayerBoundary.zMax)
        );
        Player_rb.freezeRotation = true;
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