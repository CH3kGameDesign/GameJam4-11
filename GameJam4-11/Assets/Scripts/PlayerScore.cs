using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour {

    public static List<int> playerScore = new List<int>();

    public List<TextMeshProUGUI> scoreText = new List<TextMeshProUGUI>();

	// Use this for initialization
	void Start () {
        if (playerScore.Count == 0)
        {
            playerScore.Add(0);
            playerScore.Add(0);
            playerScore.Add(0);
            playerScore.Add(0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < PlayerManager.Instance.m_players.Count; i++)
        {
            scoreText[i].text = playerScore[i].ToString();
        }
	}
}
