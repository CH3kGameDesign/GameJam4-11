using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class PlayerMoveScript : MonoBehaviour {

    [Tooltip("Control the speed of the player on the x axis")]
    [Range(1, 20)]
    public float m_playerSpeed = 10;

    [Tooltip("Scalar for innitial jump force")]
    [Range(0.0f, 1.0f)]
    public float Jump_Velocity = 1.0f;

    [Tooltip("Scalar for rate of drop")]
    [Range(0.0f, 1.0f)]
    public float Jump_Drag = 1.0f;

    private Player playerRef;
    private CharacterController m_characterController;
    private bool m_isJumping = false;
    private float m_gravityAmount = 0;

    [HideInInspector]
    public float m_gravityDir = 1.0f;


	// Use this for initialization
	void Start ()
    {
        playerRef = GetComponent<Player>();
        
        m_characterController = GetComponent<CharacterController>();
        
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 totalForce = new Vector3(0, 0, 0);

        if (playerRef.m_controllerType == PlayerInput.KEYBOARD)
        {
            string hDirection = "Horizontal" + (playerRef.m_playerID + 1);
            string jump = "Jump" + (playerRef.m_playerID + 1);

            if (Input.GetAxisRaw(hDirection) != 0)
            {
                Vector3 translation = new Vector3(m_playerSpeed * Input.GetAxisRaw(hDirection), 0, 0);

                // perform movement
                totalForce += translation * Time.deltaTime;
            }

            RaycastHit hit;

            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            if (Physics.Raycast(transform.position, Vector3.down * m_gravityDir, out hit, Mathf.Infinity, layerMask))
            {
                if (Input.GetAxisRaw(jump) > 0 && hit.distance <= 0.6f && !m_isJumping)
                {
                    m_gravityDir = -m_gravityDir;


                    //m_jumpForce = Jump_Velocity;
                    m_isJumping = true;
                }
                else if(Input.GetAxisRaw(jump) <= 0)
                {
                    m_isJumping = false;
                }
            }
        }
        else
        {
            GamePadMovement(playerRef.m_gamePadState);

            RaycastHit hit;

            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            if (Physics.Raycast(transform.position, Vector3.down * m_gravityDir, out hit, Mathf.Infinity, layerMask))
            {
                if (playerRef.m_gamePadState.Triggers.Left > 0 && hit.distance <= 0.6f && !m_isJumping)
                {
                    m_gravityDir = -m_gravityDir;
                    m_isJumping = true;
                }
                else if(playerRef.m_gamePadState.Triggers.Left <= 0)
                {
                    m_isJumping = false;
                }
            }
        }

        
        totalForce += Vector3.up * getJumpForce();

        m_characterController.Move(totalForce);
    }

    // when the player presses the jump button, this function will be called to start the jump
    private float getJumpForce()
    {
        if(m_gravityAmount <= 0.0f)
        {
            m_gravityAmount -=  2 * Jump_Drag * Time.deltaTime;

            int layerMask = 1 << 8;

            layerMask = ~layerMask;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down * m_gravityDir, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.distance < 0.6f && !hit.collider.CompareTag("Projectile"))
                    m_gravityAmount = 0;
            }

           
        }
        
        return m_gravityAmount * m_gravityDir;
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
