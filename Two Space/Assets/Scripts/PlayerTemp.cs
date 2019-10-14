using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTemp : MonoBehaviour {

    public float WalkSpeed;
    public float JumpForce;

    //public Rigidbody2D rig;
    private Vector2 inputAxis;
    private Vector3 velocity;
    private PlayerController controller;
    private float gravity = -9.8f;

	// Use this for initialization
	void Start () {
        controller = GetComponent<PlayerController>();
        //rig = gameObject.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //// reset vertical velocity if grounded
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        inputAxis = new Vector2(Input.GetAxisRaw("P2_Horizontal"), Input.GetAxisRaw("P2_Vertical"));

        //velocity = rig.velocity;
        //controller.Move(velocity);
    }

    void FixedUpdate()
    {
        if (inputAxis.x != 0)
            velocity.x = (inputAxis.x * WalkSpeed * Time.deltaTime);
        else
            velocity.x = 0;

        Debug.Log(controller.collisions.below);

        if (inputAxis.y > 0 && controller.collisions.below)
        {
            velocity.y = JumpForce;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity);        
    }
}
