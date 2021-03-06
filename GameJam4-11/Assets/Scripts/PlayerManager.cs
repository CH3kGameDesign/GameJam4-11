﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerManager : MonoBehaviour
{
    // singleton instance
    #region singleton

    private static PlayerManager m_instance;

    /// <summary>
    /// getter for singleton instance of PlayerManager
    /// </summary>
    public static PlayerManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    #endregion

    public GameObject m_playerPrefab;

    // all players
    [HideInInspector]
    public List<GameObject> m_players;
    
    // start positions
    public Transform[] m_playerStartPositions;

    public Material[] m_playerMaterials;

	// Use this for initialization
	void Awake ()
    {
        // initialise singleton instance
        m_instance = this;

        // initialise list of players
        m_players = new List<GameObject>();

        // get the state of any connected gamepads
        List<GamePadState> gamePads = GetGamePads();

        // if there are no gamepads connected, use keyboard
        if (gamePads.Count == 0)
        {
            m_players.Add(InstantiatePlayer(0, PlayerInput.KEYBOARD)); // player 1
            m_players.Add(InstantiatePlayer(1, PlayerInput.KEYBOARD)); // player 2
        }
        else
        {
            for (int i = 0; i < gamePads.Count; i++)
                m_players.Add(InstantiatePlayer(i, PlayerInput.GAMEPAD)); // create a player for each gamepad
        }
	}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<GamePadState> GetGamePads()
    {
        List<GamePadState> output = new List<GamePadState>();

        for (int i = 0; i <= (int)PlayerIndex.Four; i++)
        {
            // get gamepad 0, 1, 2, etc.
            GamePadState gamePad = GamePad.GetState((PlayerIndex)i);

            // if said gamepad is connected...
            if (gamePad.IsConnected)
            {
                output.Add(gamePad); //... add it to gamepad list
            }
        }

        return output;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="playerID"></param>
    /// <param name="controllerType"></param>
    /// <returns></returns>
    public GameObject InstantiatePlayer(int playerID, PlayerInput controllerType)
    {
        // determine spawn position
        Transform startTransform = m_playerStartPositions[playerID];

        // create player at spawn position
        GameObject player = Instantiate(m_playerPrefab);
        player.transform.position = startTransform.position;

        // set controller-relevant player variables
        player.GetComponent<Player>().m_playerID = playerID;
        player.GetComponent<Player>().m_controllerType = controllerType;
        player.GetComponentsInChildren<MeshRenderer>()[1].material = m_playerMaterials[playerID];
        player.GetComponent<PlayerFireScript>().m_reticleChild.GetComponent<MeshRenderer>().material = m_playerMaterials[playerID];

        return player;
    }
}
