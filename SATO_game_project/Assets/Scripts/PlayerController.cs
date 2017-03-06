using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
    public Boundary PlayerBoundary;

    public Rigidbody Player_rb;
    public GameObject redShot;
    public GameObject yellowShot;
	public Transform shotSpawn;
	public float fireRate;

	protected float _nextFire;
    protected GameObject shot;


    void Start()
    {
        Player_rb = GetComponent<Rigidbody>();
        shot = redShot;
    }

    void Update()
    {
		if (Input.GetKeyDown (KeyCode.Space) && Time.time > _nextFire)
		{
			_nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
        if (Input.GetKeyDown (KeyCode.E))
        {
            if (shot == redShot)
            {
                shot = yellowShot;
            }
            else if (shot == yellowShot)
            {
                shot = redShot;
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
}

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}