using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerInput
{
    KEYBOARD,
    GAMEPAD
}

public class PlayerMoveScript : MonoBehaviour {

    [Tooltip("Control the speed of the player on the x axis")]
    [Range(1, 20)]
    public float PlayerSpeed = 10;

    [Tooltip("Identifier of which player is controlling this character, can be set to up to 4, however we'll probably only have 2 players")]
    [Range(1, 4)]
    public int PlayerID;

    private PlayerInput device;
    private CharacterController controller;

	// Use this for initialization
	void Start () {
        device = PlayerInput.KEYBOARD;
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 direction = new Vector3(0,0,0);
        float speed = 10.0f;

        Vector3 totalForce = new Vector3(0,0,0);

        if (device == PlayerInput.KEYBOARD)
        {
            //if (Input.GetButton("W"))
            //{
            //    InnitiateJump();
            //}
            //if (Input.GetButton("S"))
            //{
            //    direction = transform.TransformDirection(Vector3.down);
            //}
            if (Input.GetButton("a"))
            {
                direction = transform.TransformDirection(Vector3.left);
            }
            if (Input.GetButton("d"))
            {
                direction = transform.TransformDirection(Vector3.right);
            }
        }
        
        totalForce += direction * speed * Time.deltaTime;

        ApplyForce(totalForce);
	}


    // when the player presses the jump button, this function will be called to start the jump
    void InnitiateJump()
    {

    }

    //this function is where all the force will be applied
    void ApplyForce(Vector3 force)
    {
        if(!controller.isGrounded)
        {
            force += Vector3.down * 9.1f;
        }
        controller.Move(force);
    }
}
