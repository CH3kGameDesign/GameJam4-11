using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerFireScript : MonoBehaviour
{
    // reference to player
    private Player m_player;

    // reference to the project and reticle
    public GameObject m_projectile;
    public GameObject m_reticle;
    public GameObject m_reticleChild;
    public Vector2 m_reticleExtents = new Vector2(-120, 120);

    // ammo information
    public int m_maxProjectiles = 3;
    [HideInInspector]
    public int m_currentProjectiles;
    public float m_projectileSpeed = 10.0f;
    private bool m_canShoot = true;

    private Vector2 m_rightThumbStick = new Vector3(0, 1);

    // Use this for initialization
    void Start()
    {
        // cache the player script
        m_player = GetComponent<Player>();

        m_currentProjectiles = m_maxProjectiles;
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_player.m_controllerType)
        {
            case PlayerInput.KEYBOARD: // handle keyboard firing

                //
                KeyboardAim();

                if (m_currentProjectiles != 0)
                    KeyboardFire();

                break;

            case PlayerInput.GAMEPAD: // handle gamepad firing
                
                GamePadAim();

                if (m_currentProjectiles != 0)
                    GamePadFire();

                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void KeyboardAim()
    {
        float verticalAxis = Input.GetAxisRaw("Vertical" + (m_player.m_playerID + 1)) * 5;

        if (verticalAxis != 0.0f)
            m_reticle.transform.Rotate(new Vector3(0, 0, verticalAxis));
    }

    /// <summary>
    /// 
    /// </summary>
    private void KeyboardFire()
    {

    }

    /// <summary>
    /// performs gamepad aiming (right joystick)
    /// </summary>
    private void GamePadAim()
    {
        // get gamepad
        GamePadState gamePad = m_player.m_gamePadState;

        // get right thumbstick x and y axes
        float xAngle = gamePad.ThumbSticks.Right.X;
        float yAngle = gamePad.ThumbSticks.Right.Y;

        if (xAngle == 0 && yAngle == 0)
            return;

        // 
        float rightStickAngle = Mathf.Atan2(yAngle, xAngle) * Mathf.Rad2Deg - 90;
        //rightStickAngle = Mathf.Clamp(rightStickAngle, m_reticleExtents.x, m_reticleExtents.y);

        m_reticle.transform.eulerAngles = new Vector3(0, 0, rightStickAngle);

        m_rightThumbStick.x = xAngle;
        m_rightThumbStick.y = yAngle;
    }

    /// <summary>
    /// performs gamepad firing (right trigger)
    /// </summary>
    private void GamePadFire()
    {
        GamePadState gamePad = m_player.m_gamePadState;

        if (gamePad.Triggers.Right > 0.0f && m_canShoot)
        {
            m_canShoot = false;
            m_currentProjectiles--;

            // get right thumbstick x and y axes
            float xAngle = gamePad.ThumbSticks.Right.X;
            float yAngle = gamePad.ThumbSticks.Right.Y;

            // instantiate projectile
            GameObject projectile = Instantiate(m_projectile);
            projectile.GetComponent<Projectile>().m_shooterID = m_player.m_playerID;
            projectile.transform.position = m_reticleChild.transform.position;

            Vector3 projectileForce = new Vector3(m_projectileSpeed * m_rightThumbStick.x, m_projectileSpeed * m_rightThumbStick.y, 0);

            Rigidbody projectileRigidBody = projectile.GetComponent<Rigidbody>();
            projectileRigidBody.AddForce(projectileForce);
        }
        else if (gamePad.Triggers.Right == 0.0f)
        {
            m_canShoot = true;
        }
    }
}
