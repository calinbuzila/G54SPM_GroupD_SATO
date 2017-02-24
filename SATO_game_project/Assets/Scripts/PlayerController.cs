using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
    public Boundary PlayerBoundary;

    public Rigidbody Player_rb;

    // Use this for initialization
    void Start()
    {
        Player_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate used for physics
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

