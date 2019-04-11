using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public int m_shooterID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            PlayerFireScript playerFireScript = other.GetComponent<PlayerFireScript>();

            if (player.m_playerID == m_shooterID)
            {
                playerFireScript.m_currentProjectiles++;
                Destroy(gameObject);
            }
            else
            {
                PlayerManager.Instance.m_players.Remove(other.gameObject);
                Destroy(other.gameObject);
            }
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, 0);
            //transform.position -= transform.forward;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    //if (other.CompareTag("Player"))
    //    //{
    //    //    Player player = other.GetComponent<Player>();
    //    //    PlayerFireScript playerFireScript = other.GetComponent<PlayerFireScript>();

    //    //    if (player.m_playerID == m_shooterID)
    //    //    {
    //    //        playerFireScript.m_currentProjectiles++;
    //    //        Destroy(this);
    //    //    }
    //    //    else
    //    //    {
    //    //        Destroy(player.gameObject);
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //    //}
    //}
}
