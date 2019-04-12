using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public static bool paused = false;

    public GameObject PauseMenu;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerManager.Instance.m_players.Count == 1)
            ResetLevel();

		if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused == true)
            PauseState();
        else
            PlayState();
    }

    private void PauseState ()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
        
    }

    private void PlayState ()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    private void ResetLevel()
    {
        int thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //PlayerPrefs.
        SceneManager.LoadScene(thisSceneIndex);
    }
}
