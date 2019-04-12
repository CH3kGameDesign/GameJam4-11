using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class MainMenu : MonoBehaviour
{
    public int m_levelIndex;

    private void Start()
    {
    }

    public void StartGame()
    {
        // get the state of any connected gamepads
        List<GamePadState> gamePads = GetGamePads();

        if (gamePads.Count > 1)
            SceneManager.LoadScene(m_levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
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
}
