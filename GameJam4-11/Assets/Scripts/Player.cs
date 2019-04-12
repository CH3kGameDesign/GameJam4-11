using UnityEngine;
using XInputDotNetPure;

public enum PlayerInput
{
    KEYBOARD,
    GAMEPAD
}

public class Player : MonoBehaviour
{
    [HideInInspector]
    public int m_playerID; // players 0-3 (1-4)

    [HideInInspector]
    public GamePadState m_gamePadState; // use this for gamePad input

    [HideInInspector]
    public PlayerInput m_controllerType; // keyboard or gamepad

    private bool m_canPause = true;
    
    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_controllerType == PlayerInput.GAMEPAD)
        {
            m_gamePadState = GamePad.GetState((PlayerIndex)m_playerID);

            if (m_gamePadState.Buttons.Start == ButtonState.Pressed && m_canPause)
            {
                MenuManager.paused = !MenuManager.paused;
                m_canPause = false;
            }
            else if (m_gamePadState.Buttons.Start == ButtonState.Released)
            {
                m_canPause = true;
            }
        }
	}
}
