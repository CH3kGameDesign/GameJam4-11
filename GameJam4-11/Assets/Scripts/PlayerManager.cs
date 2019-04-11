using System.Collections;
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

    public GameObject playerPrefab;

    // all players
    [HideInInspector]
    public List<GameObject> m_players;

    // all gamepads
    private List<GamePadState> m_gamePads;

    // start positions
    public Transform[] m_playerStartPositions;

	// Use this for initialization
	void Awake ()
    {
        // initialise singleton instance
        m_instance = this;

        // initialise list of players
        m_players = new List<GameObject>();

        // get the state of any connected gamepads
        m_gamePads = GetGamePads();

        // if there are no gamepads connected, use keyboard
        if (m_gamePads.Count == 0)
        {
            m_players.Add(InstantiatePlayer(0)); // player 1
            m_players.Add(InstantiatePlayer(1)); // player 2
        }
        else
        {
            for (int i = 0; i < m_gamePads.Count; i++)
                m_players.Add(InstantiatePlayer(i)); // create a player for each gamepad
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
    /// <param name="playerInput"></param>
    public GameObject InstantiatePlayer(int playerID)
    {
        // determine spawn position
        Transform startTransform = m_playerStartPositions[playerID];

        // create player at spawn position
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = startTransform.position;
        //player.GetComponent<Player>().m_playerID = playerID;

        return player;
    }
}
