using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public int m_shooterID; // who shot this bullet
    
    private bool m_isLethal = true; // bullet isn't lethal when lodged in wall

    public float m_spawnTime = 0.2f;
    private float m_lifeTime = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        m_lifeTime += Time.deltaTime;

        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            PlayerFireScript playerFireScript = other.GetComponent<PlayerFireScript>();

            if (player.m_playerID == m_shooterID && !m_isLethal)
            {
                // collision with the player who shot it
                playerFireScript.m_currentProjectiles++;
                Destroy(gameObject); // destroy projectile
            }
            else if (player.m_playerID != m_shooterID && m_isLethal)
            {
                PlayerManager.Instance.m_players.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
        else if (!other.CompareTag("Projectile"))
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            m_isLethal = false; // set bullet to non-lethal
        }
    }
}
