﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static bool paused = false;

    public GameObject PauseMenu;
    public GameObject[] buttons;

	// Use this for initialization
	void Awake ()
    {
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

    public void PauseState ()
    {
        PauseMenu.SetActive(true);

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].SetActive(true);

        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
        
    }

    public void PlayState ()
    {
        PauseMenu.SetActive(false);

        for (int i = 0; i < buttons.Length; i++)
            buttons[i].SetActive(false);

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    private void ResetLevel()
    {
        int thisSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //PlayerPrefs.
        SceneManager.LoadScene(thisSceneIndex);
    }

    public void ResumeGame()
    {
        paused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
