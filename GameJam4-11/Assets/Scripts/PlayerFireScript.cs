using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerFireScript : MonoBehaviour
{
    // reference to player
    private Player m_player;

    // reference to the reticle
    public GameObject m_projectile;
    public GameObject m_reticle;
    public Vector2 m_reticleExtents = new Vector2(-120, 120);

    // ammo information
    public int m_maxAmmo = 3;
    [HideInInspector]
    public int m_currentAmmo = 3;

	// Use this for initialization
	void Start ()
    {
        // cache the player script
        m_player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (m_player.m_controllerType)
        {
            case PlayerInput.KEYBOARD: // handle keyboard firing

                //
                
                break;

            case PlayerInput.GAMEPAD: // handle gamepad firing
;
                GamePadAim();
                GamePadFire();

                break;
        }
    }

    /// <summary>
    /// performs gamepad aiming (right joystick)
    /// </summary>
    private void GamePadAim()
    {
        GamePadState gamePad = m_player.m_gamePadState;
        float xAngle = gamePad.ThumbSticks.Right.X;
        float yAngle = gamePad.ThumbSticks.Right.Y;

        if (xAngle == 0 && yAngle == 0)
            return;

        float rightStickAngle = Mathf.Atan2(yAngle, xAngle) * Mathf.Rad2Deg - 90;
        //rightStickAngle = Mathf.Clamp(rightStickAngle, m_reticleExtents.x, m_reticleExtents.y);

        m_reticle.transform.eulerAngles = new Vector3(0, 0, rightStickAngle);
    }

    /// <summary>
    /// performs gamepad firing (right trigger)
    /// </summary>
    private void GamePadFire()
    {
        GamePadState gamePad = m_player.m_gamePadState;

        if (gamePad.Triggers.Right > 0)
        {
            // fire code here
        }
    }
}
