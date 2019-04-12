using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoDisplay : MonoBehaviour
{
    private PlayerFireScript m_playerFireScript; 

    public Renderer[] m_ammoSprites;

	// Use this for initialization
	void Start ()
    {
        m_playerFireScript = GetComponent<PlayerFireScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        int numberOfProjectiles = m_playerFireScript.m_currentProjectiles;

        for (int i = 0; i < m_ammoSprites.Length; i++)
        {
            if (i < numberOfProjectiles)
                m_ammoSprites[i].enabled = true;
            else
                m_ammoSprites[i].enabled = false;
        }
	}
}
