using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public enum PlayerInput
{
    KEYBOARD,
    GAMEPAD
}

public class PlayerMoveScript : MonoBehaviour {

    [Tooltip("Control the speed of the player on the x axis")]
    [Range(1, 20)]
    public float m_playerSpeed = 10;

    [Tooltip("Identifier of which player is controlling this character, can be set to up to 4, however we'll probably only have 2 players")]
    [Range(1, 4)]
    public int m_playerID;

    private PlayerInput device;
    private CharacterController m_characterController;
    private float m_downwardForce = 0;
    private bool m_isJumping = false;
    private float m_jumpTimer = 0;

	// Use this for initialization
	void Start () {
        device = PlayerInput.KEYBOARD;
        m_characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 totalForce = new Vector3(0, 0, 0);

        if (device == PlayerInput.KEYBOARD)
        {
            string hDirection = "Horizontal" + (m_playerID);
            string jump = "Jump" + (m_playerID);

            if (Input.GetAxisRaw(hDirection) != 0)
            {
                Vector3 translation = new Vector3(m_playerSpeed * Input.GetAxisRaw(hDirection), 0, 0);

                // perform movement
                totalForce += translation * Time.deltaTime;
            }
            if(Input.GetAxisRaw(jump) > 0 && m_characterController.isGrounded)
            {
                m_isJumping = true;
            }
        }
        else
        {
            GamePadMovement(GamePad.GetState((PlayerIndex)m_playerID));
        }


        if (m_isJumping)
        {
            totalForce += Vector3.up * getJumpForce();
            m_jumpTimer += Time.deltaTime;
        }
        //totalForce += direction * speed * Time.deltaTime;

        ApplyForce(totalForce);
        if(m_characterController.isGrounded)
        {
            m_isJumping = false;
        }
    }

    // when the player presses the jump button, this function will be called to start the jump
    float getJumpForce()
    {
        if(m_jumpTimer == 0.0f)
        {
            //return 
        }
        
        return 0.0f;
    }

    //this function is where all the force will be applied
    void ApplyForce(Vector3 force)
    {

        // the following is used to apply gravity, this may end up being replaced as gravity is boring and no one likes it
        if(!m_characterController.isGrounded && m_downwardForce != 9.1f)
        {
            m_downwardForce += 5.0f * Time.deltaTime;
            if(m_downwardForce > 9.1f) { m_downwardForce = 9.1f; }
        }
        else
        {
            m_downwardForce = 0;
        }
        force += Vector3.down * m_downwardForce;

        m_characterController.Move(force);
    }






    /// <summary>
    /// performs gamepad movement (directional pad)
    /// </summary>
    /// <param name="gamePad">the gamepad</param>
    private void GamePadMovement(GamePadState gamePad)
    {
        // left movement
        if (gamePad.DPad.Left == ButtonState.Pressed/* || gamePad.ThumbSticks.Left.X < 0*/)
            m_characterController.Move(Vector3.left * m_playerSpeed * Time.deltaTime);

        // right movement
        if (gamePad.DPad.Right == ButtonState.Pressed/* || gamePad.ThumbSticks.Left.X > 0*/)
            m_characterController.Move(Vector3.right * m_playerSpeed * Time.deltaTime);

        // jumping
        if (gamePad.DPad.Up == ButtonState.Pressed/* || gamePad.ThumbSticks.Left.Y > 0*/)
        {
            // call jump function
        }
    }
}
