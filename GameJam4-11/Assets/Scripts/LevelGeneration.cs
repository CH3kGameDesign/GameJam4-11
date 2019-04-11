using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    [Header("GameObjects")]
    public GameObject playerManager;
    [Header ("Prefabs")]
    public List<GameObject> obs = new List<GameObject>();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject GO = Instantiate(obs[Random.Range(0, obs.Count)], transform.GetChild(i).position, transform.GetChild(i).rotation, transform.GetChild(i));
            playerManager.GetComponent<PlayerManager>().m_playerStartPositions[i] = GO.transform.GetChild(0);
        }
        playerManager.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
